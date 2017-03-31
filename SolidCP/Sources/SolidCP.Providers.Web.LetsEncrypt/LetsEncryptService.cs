// Copyright (c) 2016, SolidCP
// SolidCP is distributed under the Creative Commons Share-alike license
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
// - Redistributions of source code must  retain  the  above copyright notice, this
//   list of conditions and the following disclaimer.
//
// - Redistributions in binary form  must  reproduce the  above  copyright  notice,
//   this list of conditions  and  the  following  disclaimer in  the documentation
//   and/or other materials provided with the distribution.
//
// - Neither  the  name  of SolidCP  nor   the   names  of  its
//   contributors may be used to endorse or  promote  products  derived  from  this
//   software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,  BUT  NOT  LIMITED TO, THE IMPLIED
// WARRANTIES  OF  MERCHANTABILITY   AND  FITNESS  FOR  A  PARTICULAR  PURPOSE  ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL,  SPECIAL,  EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO,  PROCUREMENT  OF  SUBSTITUTE  GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)  HOWEVER  CAUSED AND ON
// ANY  THEORY  OF  LIABILITY,  WHETHER  IN  CONTRACT,  STRICT  LIABILITY,  OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE)  ARISING  IN  ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using Microsoft.Web.Administration;
using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

using SolidCP.Providers.Common;
using SolidCP.Providers;
using SolidCP.Providers.Web;
using SolidCP.Providers.Web.Iis;
using SolidCP.Providers.Web.Iis.Common;
using SolidCP.Server;
using SolidCP.Providers.LetsEncrypt;
using ACMESharp;
using ACMESharp.JOSE;
using ACMESharp.PKI;
using ACMESharp.ACME;
using ACMESharp.HTTP;
using System.Reflection;

namespace SolidCP.Providers.LetsEncrypt
{
    public class LetsEncryptService : ConfigurationModuleService
    {
        

        public RS256Signer GetValidSigner(string signer)
        {
            RS256Signer _signer = new RS256Signer();
            _signer.Init();
            if (signer.Length > 0)  {
                using (MemoryStream memory = new MemoryStream(Encoding.ASCII.GetBytes(signer)))  {
                    _signer.Load(memory);
                }
            }
            return _signer;
        }

        public void GetValidRegistration(ref AcmeClient client, string registrant, string[] contacts)
        {
            AcmeRegistration _registration = new AcmeRegistration();
            
            if (registrant.Length > 0)  {
                using (MemoryStream memory = new MemoryStream(Encoding.ASCII.GetBytes(registrant)))  {
                    client.Registration = AcmeRegistration.Load(memory);
                }
            } else {
                //register the new contact
                client.Register(contacts);
                // update acceptance of TOS and Renewal
                client.UpdateRegistration(true, true);
            }

        }

        public void CreateAuthorizationFile(string answerfile, string filecontent)
        {
            string directory = Path.GetDirectoryName(answerfile);
            Directory.CreateDirectory(directory);
            File.WriteAllText(answerfile, filecontent);
        }

        public void WarmupSite(string url)
        {
            var request = WebRequest.Create(url);

            try{
                using (var response = request.GetResponse()) { }
            }
            catch (AmbiguousMatchException) {
                //
            }
        }

        public void BeforeAuthorize(string answerPath)
        {
            // need to override the webconfig on the autorization directory to be able to process
            // extentionless files properly

            var directory = Path.GetDirectoryName(answerPath);
            var webConfigPath = Path.Combine(directory, "web.config");

            string webconfig = @"<?xml version=""1.0"" encoding=""UTF - 8""?>
 <configuration>  
   <system.webServer>  
     <validation validateIntegratedModeConfiguration = ""false"" />   
     <staticContent>   
       <mimeMap fileExtension = ""."" mimeType = ""text/json"" />      
     </staticContent>      
   </system.webServer>      
   <system.web>      
     <authorization>
       <allow users = ""*"" />       
     </authorization>       
   </system.web>
 </configuration>";
            File.WriteAllText(webConfigPath, webconfig);
            
        }
        
        public bool ProcessChallenge(ref AuthorizationState authzState, AuthorizeChallenge challenge, HttpChallenge httpChallenge, LETarget target, AcmeClient client)
        {
            string filePath = httpChallenge.FilePath;
            if (filePath.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                filePath = filePath.Substring(1);
            string answerPath = Environment.ExpandEnvironmentVariables(Path.Combine(target.WebRootPath, filePath));

            CreateAuthorizationFile(answerPath, httpChallenge.FileContent);

            BeforeAuthorize(answerPath);

            var answerUri = new Uri(httpChallenge.FileUrl);

            authzState.Challenges = new AuthorizeChallenge[] { challenge };
            client.SubmitChallengeAnswer(authzState, AcmeProtocol.CHALLENGE_TYPE_HTTP, true);

            // have to loop to wait for server to stop being pending.
            int ctr = 0;
            while ((authzState.Status == "pending") && (ctr < 30))
            {
                Thread.Sleep(4000); // this has to be here to give ACME server a chance to think
                var newAuthzState = client.RefreshIdentifierAuthorization(authzState);
                if (newAuthzState.Status != "pending")
                    authzState = newAuthzState;
            }


            return authzState.Status!="pending";
        }

        public string GetIssuerCertificate(CertificateRequest certificate, CertificateProvider certprovider, string certificatePath, string BaseUri)
        {
            var linksEnum = certificate.Links;
            if (linksEnum != null)
            {
                var links = new LinkCollection(linksEnum);
                var upLink = links.GetFirstOrDefault("up");
                if (upLink != null)
                {
                    string temporaryFileName = Path.GetTempFileName();
                    try
                    {
                        using (WebClient web = new WebClient()){
                            var uri = new Uri(new Uri(BaseUri), upLink.Uri);
                            web.DownloadFile(uri, temporaryFileName);
                            web.DownloadData(uri);
                        }

                        X509Certificate2 cacert = new X509Certificate2(temporaryFileName);
                        string sernum = cacert.GetSerialNumberString();

                        string cacertDerFile = Path.Combine(certificatePath, string.Format("ca-{0}-crt.der",sernum));
                        string cacertPemFile = Path.Combine(certificatePath, string.Format("ca-{0}-crt.pem",sernum));

                        if (!File.Exists(cacertDerFile)) {
                            File.Copy(temporaryFileName, cacertDerFile, true);
                        }

                        if (!File.Exists(cacertPemFile)) {
                            using (FileStream source = new FileStream(cacertDerFile, FileMode.Open), target = new FileStream(cacertPemFile, FileMode.Create))  {
                                var caCrt = certprovider.ImportCertificate(EncodingFormat.DER, source);
                                certprovider.ExportCertificate(caCrt, EncodingFormat.PEM, target);
                            }
                        }
                        return cacertPemFile;
                    }
                    finally {
                        if (File.Exists(temporaryFileName))
                            File.Delete(temporaryFileName);
                    }
                }
            }

            return null;
        }

        public bool ObtainCerts(string _certificatePath, string dnsIdentifier, CertificateProvider certprovider, PrivateKey rsaKeys, Csr csr, CertificateRequest certRequ, string BaseUri, String PFXPassword, ref LERegistrationResult RegResults, LetsEncryptSettings settings)
        {
            bool IsSuccess = true;

            byte[] keyGenFile; // = Path.Combine(_certificatePath, string.Format("{0}-gen-key.json",dnsIdentifier));
            byte[] keyPemFile; // = Path.Combine(_certificatePath, string.Format("{0}-key.pem", dnsIdentifier));
            byte[] csrGenFile; // = Path.Combine(_certificatePath, string.Format("{0}-gen-csr.json", dnsIdentifier));
            byte[] csrPemFile; // = Path.Combine(_certificatePath, string.Format("{0}-csr.pem", dnsIdentifier));
            byte[] crtDerFile; // = Path.Combine(_certificatePath, string.Format("{0}-crt.der", dnsIdentifier));
            byte[] crtPemFile; // = Path.Combine(_certificatePath, string.Format("{0}-crt.pem", dnsIdentifier));
            byte[] chainPemFile;// = new byte[](); // = Path.Combine(_certificatePath, string.Format("{0}-chain.pem", dnsIdentifier));
            //byte[] crtPfxFile; // = Path.Combine(_certificatePath, string.Format("{0}-all.pfx", dnsIdentifier));

            using (var fs = new MemoryStream()) {
                certprovider.SavePrivateKey(rsaKeys, fs);
                keyGenFile = fs.ToArray();
            }
            using (var fs = new MemoryStream()) {
                certprovider.ExportPrivateKey(rsaKeys, EncodingFormat.PEM, fs);
                keyPemFile = fs.ToArray();
            }
            using (var fs = new MemoryStream()) {
                certprovider.SaveCsr(csr, fs);
                csrGenFile = fs.ToArray();
            }
            using (var fs = new MemoryStream()) {
                certprovider.ExportCsr(csr, EncodingFormat.PEM, fs);
                csrPemFile = fs.ToArray();
            }
            using (var file = new MemoryStream()) {
                certRequ.SaveCertificate(file);
                crtDerFile = file.ToArray();
            }

            Crt crt;
            using (var source = new MemoryStream()) {
                crt = certprovider.ImportCertificate(EncodingFormat.DER, source);
                crtDerFile = source.ToArray();
            }
            using(var target = new MemoryStream()) { 
                certprovider.ExportCertificate(crt, EncodingFormat.PEM, target);
                crtPemFile = target.ToArray();
            }

            // To generate a PKCS#12 (.PFX) file, we need the issuer's public certificate
            byte[] isuPemFile = Encoding.ASCII.GetBytes(GetIssuerCertificate(certRequ, certprovider, _certificatePath, BaseUri));

            //append the crt to the issuer's pem to come up with the chain file
            chainPemFile = Encoding.ASCII.GetBytes(isuPemFile.ToString() + crtPemFile.ToString());


            MemoryStream pfxtarget = new MemoryStream(); // = Path.Combine(_certificatePath, string.Format("{0}.pfx", dnsIdentifier));
            MemoryStream isusource = new MemoryStream(isuPemFile, false);
            try {
                    var isuCrt = certprovider.ImportCertificate(EncodingFormat.PEM, isusource);
                    certprovider.ExportArchive(rsaKeys, new[] { crt, isuCrt }, ArchiveFormat.PKCS12, pfxtarget, PFXPassword);
            }
                catch (AmbiguousMatchException) {
                    IsSuccess = false;
            }

            if (IsSuccess) // all the processing results
            {
                RegResults.keyGenFile = keyGenFile.ToString();
                RegResults.keyPemFile = keyPemFile.ToString();
                RegResults.csrGenFile = csrGenFile.ToString();
                RegResults.csrPemFile = csrPemFile.ToString();
                RegResults.crtDerFile = crtDerFile.ToString();
                RegResults.crtPemFile = crtPemFile.ToString();
                RegResults.crtPfxFile = pfxtarget.ToString();
                if (settings.AutoRenew) {
                    RegResults.expiryDate = DateTime.UtcNow.AddDays(settings.RegTerm);
                }
            }

            return IsSuccess;
        }

        public bool AuthorizeTarget(LETarget SiteTarget, AcmeClient _client, int bitlength, string[] SANnames, string _certificatePath, string BaseUri, string PFXPassword, ref LERegistrationResult regResult, LetsEncryptSettings settings)
        {
            List<string> dnsIdentifiers = new List<string>();
            Dictionary<string,string> pfxs = new Dictionary<string, string>();
            bool IsSuccess = false;
            // only supporting SAN at this time
            if (SiteTarget.AlternativeNames != null) {
                dnsIdentifiers.AddRange(SiteTarget.AlternativeNames);
            }
            List<AuthorizationState> authStatus = new List<AuthorizationState>();
            foreach (string dnsIdentifier in dnsIdentifiers) {
                AuthorizationState authzState = _client.AuthorizeIdentifier(dnsIdentifier);
                AuthorizeChallenge challenge = _client.DecodeChallenge(authzState, AcmeProtocol.CHALLENGE_TYPE_HTTP);
                HttpChallenge httpChallenge = challenge.Challenge as HttpChallenge;
                bool success = ProcessChallenge(ref authzState, challenge, httpChallenge, SiteTarget, _client);
                if (success)
                {
                    CertificateProvider certprovider = CertificateProvider.GetProvider();
                    RsaPrivateKeyParams rsaPkp = new RsaPrivateKeyParams();
                    rsaPkp.NumBits = bitlength;
                    PrivateKey rsaKeys = certprovider.GeneratePrivateKey(rsaPkp);
                    CsrDetails csrDetails = new CsrDetails();
                    csrDetails.CommonName = dnsIdentifier;
                    csrDetails.AlternativeNames = SANnames;
                    CsrParams csrParams = new CsrParams();
                    csrParams.Details = csrDetails;
                    Csr csr = certprovider.GenerateCsr(csrParams, rsaKeys, Crt.MessageDigest.SHA256);

                    byte[] derRaw;
                    using (var bs = new MemoryStream()) {
                        certprovider.ExportCsr(csr, EncodingFormat.DER, bs);
                        derRaw = bs.ToArray();
                    }
                    var derB64U = JwsHelper.Base64UrlEncode(derRaw);
                    var certRequ = _client.RequestCertificate(derB64U);
//start

                    if (certRequ.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        if (ObtainCerts(_certificatePath, dnsIdentifier, certprovider, rsaKeys, csr, certRequ, BaseUri, PFXPassword, ref regResult, settings) ) {
                            IsSuccess = true;
                        }
                    }

                    certprovider.Dispose();
                }
            }
            return IsSuccess;
        }

        public LERegistrationResult GenerateLECert( string[] URLs, string[] contacts, WebSite site, LetsEncryptSettings settings, bool isNewCert)
        {  
            LERegistrationResult LEresult = new LERegistrationResult();
            LEresult.IsSuccess = false;

            RS256Signer Signer = GetValidSigner(settings.signer);

            AcmeClient Client = new AcmeClient(new Uri(settings.CAUrl), new AcmeServerDirectory(), Signer);
            Client.Init();

            GetValidRegistration(ref Client, settings.registration, contacts);

            LETarget SSLTarget = new LETarget();
            SSLTarget.Host = URLs[0];
            SSLTarget.WebRootPath = site.ContentPath;
         
            LEresult.IsSuccess = AuthorizeTarget(SSLTarget, Client, settings.bitlength, URLs, site.DataPath, settings.CAUrl, settings.PFXPassword, ref LEresult, settings);
           
            return LEresult;
        }

        public LERegistrationResult RenewLECert(string[] URLs, string[] contacts, WebSite site, LetsEncryptSettings settings)
        {
            LERegistrationResult LEresult = new LERegistrationResult();
            LEresult = GenerateLECert(URLs, contacts, site, settings, false);
            return LEresult;
        }

        public bool RemoveLECert(WebSite website)
        {
            ResultObject result = new ResultObject();
            // this is where to so anything specific with LetsEncrypt
            // just return success for now until we know any different
            return true;
        }


        #region support routines


        #endregion
    }
}
