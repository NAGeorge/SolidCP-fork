using SolidCP.Providers.Common;
using SolidCP.Providers.ResultObjects;
using SolidCP.Providers.Utils;
using SolidCP.Server.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace SolidCP.Providers.Web
{
    internal class LetsEncrypt
    {
        private readonly ACMESharpRunspaceHelper _runspaceHelper;

        public string BaseService
        {
            get; set;
        }

        public string VaultProfileName
        {
            get; set;
        }

        public string VaultRootPath
        {
            get; set;
        }

        public LetsEncrypt(string vaultProfileName, string vaultRootPath, bool staging = false)
        {
            this.VaultProfileName = vaultProfileName;
            this.VaultRootPath = FileUtils.EvaluateSystemVariables(vaultRootPath);
            this.BaseService = "LetsEncrypt";
            if (staging)
            {
                this.BaseService = "LetsEncrypt-STAGING";
            }
            this._runspaceHelper = new ACMESharpRunspaceHelper();
            try
            {
                if (string.IsNullOrEmpty(vaultRootPath))
                {
                    throw new Exception("The root path for ACMESharp vault profiles is not set in the web server service settings in SolidCP");
                }
            }
            catch (Exception exception)
            {
                Log.WriteError("Unable to initialize ACMESharp", exception);
                throw;
            }
            try
            {
                if (!this._runspaceHelper.AreModulesInstalled(new string[] { "ACMESharp" }))
                {
                    throw new Exception("Cannot import ACMESharp Powershell Module! Install ACMESharp and make sure the application pool is 64 bit.");
                }
            }
            catch (Exception exception1)
            {
                Log.WriteError("Please install the PowerShell module for ACMESharp as an Administrator using Install from the PowerShell Gallery if you're using PS v5 or from Chocolatey if you're using PS v3 or v4. Read more at https://github.com/ebekker/ACMESharp/wiki/Quick-Start", exception1);
                throw;
            }
            Runspace runspace = this._runspaceHelper.OpenRunspace();
            try
            {
                Command command = new Command("Get-ACMEVaultProfile");
                command.Parameters.Add("ProfileName", this.VaultProfileName);
                Collection<PSObject> pSObjects = this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
                if (!pSObjects.Any<PSObject>() || pSObjects[0] == null)
                {
                    this.CreateVaultProfile(runspace, false);
                }
                else if (!(pSObjects[0].Properties["VaultParameters"].Value is IDictionary<string, object>))
                {
                    this.CreateVaultProfile(runspace, true);
                }
                command = new Command("Get-ACMEVault");
                command.Parameters.Add("VaultProfile", this.VaultProfileName);
                Collection<PSObject> pSObjects1 = this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
                if (!pSObjects1.Any<PSObject>() || pSObjects1[0] == null || !Directory.Exists(Path.Combine(this.VaultRootPath, this.VaultProfileName)))
                {
                    command = new Command("Initialize-ACMEVault");
                    command.Parameters.Add("Alias", this.VaultProfileName);
                    command.Parameters.Add("VaultProfile", this.VaultProfileName);
                    command.Parameters.Add("BaseService", this.BaseService);
                    this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
                }
                this._runspaceHelper.CloseRunspace(runspace);
            }
            catch (Exception exception3)
            {
                Exception exception2 = exception3;
                Log.WriteError(string.Concat("Unable to get or create Vault Profile with storage. Profilename: ", vaultProfileName, ", ProfileRootPath: ", vaultRootPath), exception2);
                throw;
            }
        }

        public LetsEncryptResult CreateCertificate(string hostName, string[] SANs, string siteName)
        {
            string str;
            string str1;
            string str2;
            LetsEncryptResult letsEncryptResult = new LetsEncryptResult()
            {
                IsSuccess = true
            };
            List<string> strs = new List<string>();
            try
            {
                Runspace runspace = this._runspaceHelper.OpenRunspace();
                if (this.ValidateHostName(runspace, hostName, siteName, out str))
                {
                    List<string> strs1 = new List<string>();
                    string[] sANs = SANs;
                    for (int i = 0; i < (int)sANs.Length; i++)
                    {
                        string str3 = sANs[i];
                        if (this.ValidateHostName(runspace, str3, siteName, out str2))
                        {
                            strs1.Add(str2);
                        }
                        else
                        {
                            strs.Add(str3);
                        }
                    }
                    letsEncryptResult.Value = strs.ToArray();
                    int dayOfYear = (DateTime.Today.DayOfYear + 79) / 80;
                    using (MD5 mD5 = MD5.Create())
                    {
                        str1 = string.Format("{0}-{1}-cert-{2:yyyy}{3}", new object[] { hostName, new Guid(mD5.ComputeHash(Encoding.UTF8.GetBytes(string.Join("", SANs)))), DateTime.Today, dayOfYear });
                    }
                    Collection<PSObject> pSObjects = null;
                    try
                    {
                        Command command = new Command("Get-ACMECertificate");
                        command.Parameters.Add("CertificateRef", str1);
                        command.Parameters.Add("VaultProfile", this.VaultProfileName);
                        pSObjects = this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
                    }
                    catch
                    {
                    }
                    if (pSObjects == null || !pSObjects.Any<PSObject>() || pSObjects[0] == null)
                    {
                        Command command1 = new Command("New-ACMECertificate");
                        command1.Parameters.Add("IdentifierRef", str);
                        command1.Parameters.Add("VaultProfile", this.VaultProfileName);
                        command1.Parameters.Add("Generate");
                        command1.Parameters.Add("Alias", str1);
                        if (strs1.Any<string>())
                        {
                            command1.Parameters.Add("AlternativeIdentifierRefs", strs1);
                        }
                        pSObjects = this._runspaceHelper.ExecuteShellCommand(runspace, command1, false);
                    }
                    string value = pSObjects[0].Properties["IssuerSerialNumber"].Value as string;
                    if (string.IsNullOrEmpty(value))
                    {
                        Command command2 = new Command("Submit-ACMECertificate");
                        command2.Parameters.Add("CertificateRef", str1);
                        command2.Parameters.Add("VaultProfile", this.VaultProfileName);
                        pSObjects = this._runspaceHelper.ExecuteShellCommand(runspace, command2, false);
                        value = pSObjects[0].Properties["IssuerSerialNumber"].Value as string;
                    }
                    DateTime now = DateTime.Now;
                    while (string.IsNullOrEmpty(value) || (DateTime.Now - now).Seconds > 5)
                    {
                        Thread.Sleep(750);
                        Command command3 = new Command("Update-ACMECertificate");
                        command3.Parameters.Add("CertificateRef", str1);
                        command3.Parameters.Add("VaultProfile", this.VaultProfileName);
                        pSObjects = this._runspaceHelper.ExecuteShellCommand(runspace, command3, false);
                        value = pSObjects[0].Properties["IssuerSerialNumber"].Value.ToString();
                    }
                    string str4 = string.Concat(Path.GetTempPath(), Guid.NewGuid(), ".pfx");
                    try
                    {
                        Command command4 = new Command("Get-ACMECertificate");
                        command4.Parameters.Add("CertificateRef", str1);
                        command4.Parameters.Add("VaultProfile", this.VaultProfileName);
                        command4.Parameters.Add("ExportPkcs12", str4);
                        this._runspaceHelper.ExecuteShellCommand(runspace, command4, false);
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        letsEncryptResult.IsSuccess = false;
                        if (!exception.Message.Contains("Issuer certificate hasn't been resolved"))
                        {
                            letsEncryptResult.ErrorDescription = string.Concat("Unable to export certificate: ", exception.Message);
                        }
                        else
                        {
                            Log.WriteError("Please download and install the LetsEncrypt Intermediate certificate in the Intermediate certificates store for local machine", exception);
                            letsEncryptResult.ErrorDescription = "Let's Encrypt intermediate certificate is missing.";
                        }
                    }
                    this._runspaceHelper.CloseRunspace(runspace);
                    if (File.Exists(str4))
                    {
                        letsEncryptResult.Certificate = File.ReadAllBytes(str4);
                        File.Delete(str4);
                    }
                    else
                    {
                        letsEncryptResult.IsSuccess = false;
                        letsEncryptResult.ErrorDescription = "Certificate not exported";
                    }
                }
                else
                {
                    letsEncryptResult.IsSuccess = false;
                    letsEncryptResult.ErrorDescription = "Could not validate the choosen hostname for the certificate. No certificate issued.";
                    return letsEncryptResult;
                }
            }
            catch (Exception exception3)
            {
                Exception exception2 = exception3;
                Log.WriteError(string.Concat("Unable to create certificate for hostname: ", hostName, " and site: ", siteName), exception2);
                letsEncryptResult.IsSuccess = false;
                letsEncryptResult.ErrorDescription = string.Concat("Unable to create certificate: ", exception2.Message);
            }
            return letsEncryptResult;
        }

        private void CreateVaultProfile(Runspace runspace, bool force = false)
        {
            Command command = new Command("Set-ACMEVaultProfile");
            command.Parameters.Add("ProfileName", this.VaultProfileName);
            command.Parameters.Add("Provider", "local");
            if (force)
            {
                command.Parameters.Add("Force");
            }
            command.Parameters.Add("VaultParameters", new Dictionary<string, object>()
            {
                { "RootPath", Path.Combine(this.VaultRootPath, this.VaultProfileName) },
                { "CreatePath", true }
            });
            this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
        }

        public void Registration(string contactEmail)
        {
            try
            {
                Runspace runspace = this._runspaceHelper.OpenRunspace();
                Command command = new Command("Get-ACMERegistration");
                command.Parameters.Add("VaultProfile", this.VaultProfileName);
                Collection<PSObject> pSObjects = null;
                try
                {
                    pSObjects = this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
                }
                catch (Exception exception)
                {
                }
                if (pSObjects == null || !pSObjects.Any<PSObject>() || pSObjects[0] == null)
                {
                    command = new Command("New-ACMERegistration");
                    command.Parameters.Add("VaultProfile", this.VaultProfileName);
                    command.Parameters.Add("Contacts", string.Concat("mailto:", contactEmail));
                    command.Parameters.Add("AcceptTos", null);
                    this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
                }
                this._runspaceHelper.CloseRunspace(runspace);
            }
            catch (Exception exception1)
            {
                Log.WriteError(string.Concat("Unable to check/create Lets Encrypt registration for email: ", contactEmail), exception1);
                throw;
            }
        }

        private void RemoveProfile(Runspace runspace)
        {
            Command command = new Command("Set-ACMEVaultProfile");
            command.Parameters.Add("ProfileName", this.VaultProfileName);
            command.Parameters.Add("Remove");
            command.Parameters.Add("Force");
            try
            {
                this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
            }
            catch (Exception exception)
            {
            }
            string str = Path.Combine(this.VaultRootPath, this.VaultProfileName);
            if (Directory.Exists(str))
            {
                Directory.Delete(str, true);
            }
        }

        private bool ValidateHostName(Runspace runspace, string hostName, string siteName, out string identifierRef)
        {
            bool flag;
            try
            {
                Command command = new Command("Get-ACMEIdentifier");
                command.Parameters.Add("VaultProfile", this.VaultProfileName);
                Collection<PSObject> pSObjects = null;
                try
                {
                    pSObjects = this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
                }
                catch (Exception exception)
                {
                }
                if (pSObjects == null || !pSObjects.Any<PSObject>((PSObject i) => {
                    if (i.Properties["Dns"].Value.ToString() != hostName)
                    {
                        return false;
                    }
                    return i.Properties["Status"].Value.ToString() == "valid";
                }))
                {
                    string str = hostName;
                    DateTime now = DateTime.Now;
                    identifierRef = string.Concat("SolidCP-", str, now.ToString("s"));
                    command = new Command("New-ACMEIdentifier");
                    command.Parameters.Add("VaultProfile", this.VaultProfileName);
                    command.Parameters.Add("Dns", hostName);
                    command.Parameters.Add("Alias", identifierRef);
                    this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
                    command = new Command("Complete-ACMEChallenge");
                    command.Parameters.Add("IdentifierRef", identifierRef);
                    command.Parameters.Add("VaultProfile", this.VaultProfileName);
                    command.Parameters.Add("ChallengeType", "http-01");
                    command.Parameters.Add("Handler", "iis");
                    command.Parameters.Add("HandlerParameters", new Dictionary<string, object>()
                    {
                        { "WebSiteRef", siteName }
                    });
                    this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
                    command = new Command("Submit-ACMEChallenge");
                    command.Parameters.Add("IdentifierRef", identifierRef);
                    command.Parameters.Add("VaultProfile", this.VaultProfileName);
                    command.Parameters.Add("ChallengeType", "http-01");
                    this._runspaceHelper.ExecuteShellCommand(runspace, command, false);
                    string str1 = "pending";
                    DateTime dateTime = DateTime.Now;
                    while (str1 == "pending")
                    {
                        Thread.Sleep(500);
                        command = new Command("Update-ACMEIdentifier");
                        command.Parameters.Add("IdentifierRef", identifierRef);
                        command.Parameters.Add("VaultProfile", this.VaultProfileName);
                        str1 = this._runspaceHelper.ExecuteShellCommand(runspace, command, false)[0].Properties["Status"].Value.ToString();
                        if (!(str1 == "pending") || (DateTime.Now - dateTime).Seconds <= 3)
                        {
                            continue;
                        }
                        str1 = "timeout";
                    }
                    if (str1 == "invalid")
                    {
                        throw new Exception(string.Format("LetsEncrypt is unable to validate domain {0} on site {1}", hostName, siteName));
                    }
                    if (str1 == "timeout")
                    {
                        throw new Exception(string.Format("Time out validating domain {0} on site {1}", hostName, siteName));
                    }
                    flag = true;
                }
                else
                {
                    identifierRef = pSObjects.First<PSObject>((PSObject i) => {
                        if (i.Properties["Dns"].Value.ToString() != hostName)
                        {
                            return false;
                        }
                        return i.Properties["Status"].Value.ToString() == "valid";
                    }).Properties["Alias"].Value.ToString();
                    flag = true;
                }
            }
            catch (Exception exception2)
            {
                Exception exception1 = exception2;
                identifierRef = string.Empty;
                Log.WriteError(string.Format("LetsEncrypt is unable to validate domain {0} on site {1}", hostName, siteName), exception1);
                flag = false;
            }
            return flag;
        }
    }
}
