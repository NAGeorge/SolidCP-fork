using SolidCP.EnterpriseServer.MailTemplates;
using SolidCP.Providers.Common;
using SolidCP.Providers.ResultObjects;
using SolidCP.Providers.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SolidCP.EnterpriseServer
{
    class LetsEncryptRenewalTask : SchedulerTask
    {
        private const string DaysBefore = "DAYS_BEFORE";

        private const string NotifyOwner = "NOTIFY_OWNER";

        private const string NotifyEmail = "NOTIFY_EMAIL";

        public LetsEncryptRenewalTask()
        {
        }

        public override void DoWork()
        {
            int num;
            BackgroundTask topTask = TaskManager.TopTask;
            if (!string.IsNullOrEmpty((string)topTask.GetParamValue("DAYS_BEFORE")))
            {
                int.TryParse((string)topTask.GetParamValue("DAYS_BEFORE"), out num);
                bool flag = Convert.ToBoolean(topTask.GetParamValue("NOTIFY_OWNER"));
                string str = Convert.ToString(topTask.GetParamValue("NOTIFY_EMAIL"));
                List<LetsEncryptRenewalTask.CertificateAndStatus> certificateAndStatuses = new List<LetsEncryptRenewalTask.CertificateAndStatus>();
                foreach (SSLCertificate sSLCertificate in ObjectUtils.CreateListFromDataReader<SSLCertificate>(DataProvider.GetAllSslCertificatesToRenew(num, true)))
                {
                    UserInfoInternal user = UserController.GetUser(sSLCertificate.UserID);
                    TaskManager.Write(string.Concat("Attempting renewal of certificate for: ", sSLCertificate.Hostname), new string[0]);
                    StringArrayResultObject stringArrayResultObject = WebServerController.RenewLetsEncryptCertificate(sSLCertificate);
                    string str1 = stringArrayResultObject.ErrorCodes.FirstOrDefault<string>();
                    LetsEncryptRenewalTask.CertificateAndStatus certificateAndStatu = new LetsEncryptRenewalTask.CertificateAndStatus()
                    {
                        Certificate = sSLCertificate,
                        ErrorDescription = str1,
                        Username = user.Username,
                        Success = stringArrayResultObject.IsSuccess
                    };
                    certificateAndStatuses.Add(certificateAndStatu);
                    if (!stringArrayResultObject.IsSuccess)
                    {
                        TaskManager.WriteWarning(string.Concat("Failed to renew certificate for: ", sSLCertificate.Hostname, ": ", str1), new string[0]);
                    }
                    else
                    {
                        TaskManager.Write(string.Concat("Successfully renewed certificate for: ", sSLCertificate.Hostname), new string[0]);
                    }
                    if (flag)
                    {
                        this.SendMailMessageToOwner(user, sSLCertificate, stringArrayResultObject.IsSuccess, str1);
                    }
                    Thread.Sleep(100);
                }
                if ((string.IsNullOrEmpty(str) ? false : certificateAndStatuses.Any<LetsEncryptRenewalTask.CertificateAndStatus>()))
                {
                    this.SendMailMessageToAdmin(UserController.GetUser(topTask.EffectiveUserId), certificateAndStatuses, str);
                }
            }
            else
            {
                TaskManager.WriteError("Unable to renew certificates. The 'Days before Expiry' parameter is empty.", new string[0]);
            }
        }

        private void SendMailMessageToAdmin(UserInfo user, IEnumerable<LetsEncryptRenewalTask.CertificateAndStatus> certificateAndStatuses, string email)
        {
            UserSettings userSettings = UserController.GetUserSettings(user.UserId, "LetsEncryptRenewalAdminNotificationLetter");
            string item = userSettings["From"];
            string str = userSettings["CC"];
            string item1 = userSettings["Subject"];
            string str1 = email;
            string str2 = (user.HtmlMail ? userSettings["HtmlBody"] : userSettings["TextBody"]);
            bool isHtml = user.HtmlMail;
            MailPriority mailPriority = MailPriority.Normal;
            if (!string.IsNullOrEmpty(userSettings["Priority"]))
            {
                mailPriority = (MailPriority)Enum.Parse(typeof(MailPriority), userSettings["Priority"], true);
            }
            TemplateHashtable templateHashtable = new TemplateHashtable(user);
            templateHashtable["Certificates"] = certificateAndStatuses;
            str2 = PackageController.EvaluateTemplate(str2, templateHashtable);
            MailHelper.SendMessage(item, str1, str, item1, str2, mailPriority, isHtml );
        }

        private void SendMailMessageToOwner(UserInfo user, SSLCertificate certificate, bool success, string errorDescription)
        {
            UserSettings userSettings = UserController.GetUserSettings(user.UserId, "LetsEncryptRenewalNotificationLetter");
            string item = userSettings["From"];
            string str = userSettings["CC"];
            string item1 = userSettings["Subject"];
            string email = user.Email;
            string str1 = (user.HtmlMail ? userSettings["HtmlBody"] : userSettings["TextBody"]);
            bool htmlMail = user.HtmlMail;
            MailPriority mailPriority = MailPriority.Normal;
            if (!string.IsNullOrEmpty(userSettings["Priority"]))
            {
                mailPriority = (MailPriority)Enum.Parse(typeof(MailPriority), userSettings["Priority"], true);
            }
            TemplateHashtable templateHashtable = new TemplateHashtable(user);
            templateHashtable["Certificate"] = certificate;
            templateHashtable["Success"] = success;
            templateHashtable["ErrorDescription"] = errorDescription;
            str1 = PackageController.EvaluateTemplate(str1, templateHashtable);
            MailHelper.SendMessage(item, email, str, item1, str1, mailPriority, htmlMail);
        }

        private struct CertificateAndStatus
        {
            public SSLCertificate Certificate
            {
                get;
                set;
            }

            public string ErrorDescription
            {
                get;
                set;
            }

            public bool Success
            {
                get;
                set;
            }

            public string Username
            {
                get;
                set;
            }
        }
    }
}
