using SolidCP.Providers.EmailSecurity;
using SolidCP.Server.Utils;
using System;
using System.IO;
using System.Net;
using System.Web;
namespace SolidCP.Providers.EmailSecurity.SpamExperts
{
    public class SpamExperts : HostingServiceProviderBase, IEmailSecurity
    {
        private string DefaultEmptyPass
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        private string Password
        {
            get
            {
                return base.ProviderSettings["password"];
            }
        }

        private string Schema
        {
            get
            {
                return base.ProviderSettings["schema"];
            }
        }

        private string Url
        {
            get
            {
                return base.ProviderSettings["url"];
            }
        }

        private string User
        {
            get
            {
                return base.ProviderSettings["user"];
            }
        }

        public SpamExperts()
        {
        }

        public EmailSecurityResult AddDomain(string domain, string password, string email, string[] destinations)
        {
            EmailSecurityResult emailSecurityResult;
            Log.WriteStart("AddDomain", new object[0]);
            if (string.IsNullOrEmpty(password))
            {
                password = this.DefaultEmptyPass;
            }
            if (destinations == null || destinations.Length == 0)
            {
                emailSecurityResult = this.ExecCommand("domain/add", new string[] { "domain", domain });
            }
            else
            {
                string str = string.Concat("[\"", string.Join("\",\"", destinations), "\"]");
                emailSecurityResult = this.ExecCommand("domain/add", new string[] { "domain", domain, "destinations", str });
            }
            emailSecurityResult = this.ExecCommand("domainuser/add", new string[] { "domain", domain, "password", password, "email", email });
            Log.WriteEnd("AddDomain", new object[0]);
            return emailSecurityResult;
        }

        public EmailSecurityResult AddEmail(string name, string domain, string password)
        {
            Log.WriteStart("AddEmail", new object[0]);
            if (string.IsNullOrEmpty(password))
            {
                password = this.DefaultEmptyPass;
            }
            EmailSecurityResult emailSecurityResult = this.ExecCommand("emailusers/add", new string[] { "username", name, "password", password, "domain", domain });
            Log.WriteEnd("AddEmail", new object[0]);
            return emailSecurityResult;
        }

        private EmailSecurityResult CheckSuccess(string result)
        {
            if (result == null)
            {
                return EmailSecurityResult.None;
            }
            if (result.Length >= 10 && result.Substring(0, 10) == "EXCEPTION:")
            {
                return new EmailSecurityResult(EmailSecurityStatus.Error, result);
            }
            if (result.Contains("SUCCESS"))
            {
                return new EmailSecurityResult(EmailSecurityStatus.Success, result);
            }
            if (result.Contains("already exists"))
            {
                return new EmailSecurityResult(EmailSecurityStatus.AlreadyExists, result);
            }
            if (result.Contains("Unable to find"))
            {
                return new EmailSecurityResult(EmailSecurityStatus.NotFound, result);
            }
            if (result.Contains("No such"))
            {
                return new EmailSecurityResult(EmailSecurityStatus.NotFound, result);
            }
            return new EmailSecurityResult(EmailSecurityStatus.Error, result);
        }

        public EmailSecurityResult DeleteDomain(string domain)
        {
            Log.WriteStart("DeleteDomain", new object[0]);
            EmailSecurityResult emailSecurityResult = this.ExecCommand("domainuser/remove", new string[] { "username", domain });
            emailSecurityResult = this.ExecCommand("domain/remove", new string[] { "domain", domain });
            Log.WriteEnd("DeleteDomain", new object[0]);
            return emailSecurityResult;
        }

        public EmailSecurityResult DeleteEmail(string email)
        {
            Log.WriteStart("DeleteEmail", new object[0]);
            EmailSecurityResult emailSecurityResult = this.ExecCommand("emailusers/remove", new string[] { "username", email });
            Log.WriteEnd("DeleteEmail", new object[0]);
            return emailSecurityResult;
        }

        private EmailSecurityResult ExecCommand(string command, params string[] param)
        {
            Log.WriteStart("ExecCommand {0}", new object[] { command });
            UriBuilder uriBuilder = new UriBuilder()
            {
                Scheme = this.Schema,
                Host = this.Url
            };
            string str = string.Concat("/api/", command, "/");
            int length = (int)param.Length / 2;
            for (int i = 0; i < length; i++)
            {
                string str1 = param[i * 2];
                string str2 = param[i * 2 + 1];
                str = string.Concat(new string[] { str, str1, "/", HttpUtility.UrlEncode(str2), "/" });
                if (str1 != "password")
                {
                    Log.WriteInfo("{0}={1}", new object[] { str1, str2 });
                }
            }
            uriBuilder.Path = str;
            string empty = string.Empty;
            try
            {
                using (Stream stream = (new WebClient()
                {
                    Credentials = new NetworkCredential(this.User, this.Password)
                }).OpenRead(uriBuilder.Uri))
                {
                    empty = (new StreamReader(stream)).ReadToEnd();
                }
                if (empty == null)
                {
                    empty = "";
                }
                Log.WriteInfo("result = {0}", new object[] { empty });
            }
            catch (Exception exception)
            {
                empty = string.Concat("EXCEPTION:", exception.ToString());
                Log.WriteWarning(empty, new object[0]);
            }
            Log.WriteEnd("ExecCommand", new object[0]);
            return this.CheckSuccess(empty);
        }

        public override bool IsInstalled()
        {
            return true;
        }

        public EmailSecurityResult SetDomainDestinations(string name, string[] destinations)
        {
            if (destinations.Length == 0)
            {
                return EmailSecurityResult.None;
            }
            string str = string.Concat("[\"", string.Join("\",\"", destinations), "\"]");
            return this.ExecCommand("domain/edit", new string[] { "domain", name, "destinations", str });
        }

        public EmailSecurityResult SetDomainUserPassword(string name, string password)
        {
            Log.WriteStart("SetDomainUserPassword", new object[0]);
            if (string.IsNullOrEmpty(password))
            {
                password = this.DefaultEmptyPass;
            }
            Log.WriteEnd("SetDomainUserPassword", new object[0]);
            return this.ExecCommand("domainuser/setpassword", new string[] { "username", name, "password", password });
        }

        public EmailSecurityResult SetEmailPassword(string email, string password)
        {
            Log.WriteStart("SetEmailPassword", new object[0]);
            if (string.IsNullOrEmpty(password))
            {
                password = this.DefaultEmptyPass;
            }
            EmailSecurityResult emailSecurityResult = this.ExecCommand("emailusers/setpassword", new string[] { "username", email, "password", password });
            Log.WriteEnd("SetEmailPassword", new object[0]);
            return emailSecurityResult;
        }
    }
}
