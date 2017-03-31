using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Configuration;

using SolidCP.Providers.EmailSecurity;
using SolidCP.Providers;

namespace SolidCP.EnterpriseServer
{
    public enum ExecResult
    {
        Success = 0,
        Error = 1,
        AlreadyExists = 2,
        NotFound = 3,
        None = 4
    }

    public class SEClass
    {
        
        public static string DefaultEmptyPass = "SolidCP123@#";

        private static ExecResult CheckSuccess(string result)
        {
           
            if (result == null) return ExecResult.None;

            if (result.Length >= 10)
            {
                if (result.Substring(0, 10) == "EXCEPTION:") return ExecResult.Error;
                
            }
            if (result.Contains("SUCCESS")) return ExecResult.Success;
            if (result.Contains("already exists")) return ExecResult.AlreadyExists;
            if (result.Contains("Unable to find")) return ExecResult.NotFound;
            if (result.Contains("No such")) return ExecResult.NotFound;

            return ExecResult.Error;


        }

        private static string ExecCommand( string command, params string[] param)
        {
            string commandInfo = "exec command " + command;

            UriBuilder uri = new UriBuilder();
            uri.Scheme = ConfigurationManager.AppSettings["SpamExpertsSchema"];
            uri.Host = ConfigurationManager.AppSettings["SpamExpertsUrl"];
            //uri.UserName = SEConfig.Instance.user;
            //uri.Password = SEConfig.Instance.password;

            SELog.Write(uri.ToString());

            string com = "/api/" + command + "/";
            int paramCount = param.Length / 2;
            for (int i = 0; i < paramCount; i++)
            {
                string name = param[i * 2];
                string val = param[i * 2 + 1];

                com += name + "/" + HttpUtility.UrlEncode(val) + "/";

                if ((name != "password") || (Convert.ToBoolean(ConfigurationManager.AppSettings["SpamExpertsExtendedLog"])))
                {
                    commandInfo += " " + name + "='" + val + "'";
                }
            }
            SELog.Write(com.ToString());
            uri.Path = com;

            

            string result = string.Empty;
            try
            {
                System.Net.WebClient Client = new WebClient();

                Client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SpamExpertsUser"], ConfigurationManager.AppSettings["SpamExpertsPassword"]);

                using (Stream strm = Client.OpenRead(uri.Uri))
                {
                    StreamReader sr = new StreamReader(strm);
                    result = sr.ReadToEnd();
                }

                if (result == null) result = "";
                SELog.Write(result.ToString());
                SELog.Write(com.ToString() + result.ToString());
            }
            catch (Exception exc)
            {
                result = "EXCEPTION:" + exc.ToString();
                SELog.WriteError(exc);
            }


            // send error message
            //if (CheckSuccess(result) == ExecResult.Error)
            //{
            //    SendMail(SEConfig.Instance.ErrorMailSubject,
            //            String.Format(SEConfig.Instance.ErrorMailBody, commandInfo, result)
            //        );
            //}
            SELog.Write(result.ToString());
            return result;
        }

        static public ExecResult AddDomain(string domain, string password, string email, string[] destinations, out string result)
        {
            SELog.Write("##############################################");
            SELog.Write(" ");
            SELog.Write("AddDomain");

            if (String.IsNullOrEmpty(password))
                password = DefaultEmptyPass;

            if ((destinations == null) || (destinations.Length == 0))
            {
                result = ExecCommand("domain/add", "domain", domain);
            }
            else
            {
                string list = "[\"" + String.Join("\",\"", destinations) + "\"]";
                result = ExecCommand("domain/add", "domain", domain, "destinations", list);
            }

            result = ExecCommand("domainuser/add", "domain", domain, "password", password, "email", email);
            SELog.Write(result.ToString());
            SELog.Write(" ");
            SELog.Write("##############################################");
            return CheckSuccess(result);
        }
        static public ExecResult AddDomain(string domain, string password, string email, string[] destinations)
        {
            SELog.Write("##############################################");
            SELog.Write(" ");
            SELog.Write("AddDomain");
            string result;
            return AddDomain(domain, password, email, destinations, out result);
        }

        static public ExecResult DeleteDomain(string domain, out string result)
        {
            SELog.Write("##############################################");
            SELog.Write(" ");
            SELog.Write("DeleteDomain");

            result = ExecCommand("domainuser/remove", "username", domain);
            result = ExecCommand("domain/remove", "domain", domain);
            SELog.Write(result.ToString());
            SELog.Write(" ");
            SELog.Write("##############################################");
            return CheckSuccess(result);
        }
        static public ExecResult DeleteDomain(string name)
        {
            string result;
            return DeleteDomain(name, out result);
        }

        static public ExecResult AddEmail(string name, string domain, string password, out string result)
        {
            SELog.Write("##############################################");
            SELog.Write(" ");
            SELog.Write("AddEmail");

            if (String.IsNullOrEmpty(password))
                password = DefaultEmptyPass;

            result = ExecCommand("emailusers/add", "username", name, "password", password, "domain", domain);
            SELog.Write(result.ToString());
            SELog.Write(" ");
            SELog.Write("##############################################");
            return CheckSuccess(result);
        }

        static public ExecResult AddEmail(string name, string domain, string password)
        {
            string result;
            return AddEmail(name, domain, password, out result);
        }

        static public ExecResult DeleteEmail(string email, out string result)
        {
            SELog.Write("##############################################");
            SELog.Write(" ");
            SELog.Write("DeleteEmail");

            result = ExecCommand("emailusers/remove", "username", email);
            SELog.Write(result.ToString());
            SELog.Write(" ");
            SELog.Write("##############################################");
            return CheckSuccess(result);
        }

        static public ExecResult DeleteEmail(string email)
        {
            string result;
            return DeleteEmail(email, out result);
        }

        static public ExecResult SetEmailPassword(string email, string password, out string result)
        {
            SELog.Write("##############################################");
            SELog.Write(" ");
            SELog.Write("SetEmailPassword");

            if (String.IsNullOrEmpty(password))
                password = DefaultEmptyPass;

            result = ExecCommand("emailusers/setpassword", "username", email, "password", password);
            SELog.Write(result.ToString());
            SELog.Write(" ");
            SELog.Write("##############################################");
            return CheckSuccess(result);
        }

        static public ExecResult SetEmailPassword(string email, string password)
        {
            string result;
            return SetEmailPassword(email, password, out result);
        }
        static public ExecResult SetDomainUserPassword(string name, string password)
        {
            SELog.Write("##############################################");
            SELog.Write(" ");
            SELog.Write("SetDomainUserPassword");

            if (String.IsNullOrEmpty(password))
                password = DefaultEmptyPass;

            string result = ExecCommand("domainuser/setpassword", "username", name, "password", password);
            SELog.Write(result.ToString());
            SELog.Write(" ");
            SELog.Write("##############################################");
            return CheckSuccess(result);
        }

        static public ExecResult SetDomainDestinations(string name, string[] destinations, out string result)
        {
            result = "";
            if (destinations.Length == 0) return ExecResult.None;

            string list = "[\"" + String.Join("\",\"", destinations) + "\"]";
            result = ExecCommand("domain/edit", "domain", name, "destinations", list);
            SELog.Write(result.ToString());
            SELog.Write(" ");
            SELog.Write("##############################################");
            return CheckSuccess(result);
        }

        static public ExecResult SetDomainDestinations(string name, string[] destinations)
        {
            string result;
            return SetDomainDestinations(name, destinations, out result);
        }


        //static private String SendMail(String subject, String Message)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(SEConfig.Instance.MailServer))
        //            return "";

        //        SmtpClient client = new SmtpClient(SEConfig.Instance.MailServer, 25);
        //        client.Send(SEConfig.Instance.MailFrom, SEConfig.Instance.MailTo, subject, Message);
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        SELog.WriteError(ex);
        //        return ex.Message;
        //    }

        //}

    }
}