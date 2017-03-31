using System;
using System.Runtime.CompilerServices;

namespace SolidCP.Providers.EmailSecurity
{
    [Serializable]
    public class EmailSecurityResult
    {
        public static EmailSecurityResult None
        {
            get
            {
                return new EmailSecurityResult(EmailSecurityStatus.None, null);
            }
        }

        private string Result
        {
            get;
            set;
        }

        private EmailSecurityStatus Status
        {
            get;
            set;
        }

        public EmailSecurityResult()
        {
            this.Status = EmailSecurityStatus.None;
            this.Result = null;
        }

        public EmailSecurityResult(EmailSecurityStatus status, string result)
        {
            this.Status = status;
            this.Result = result;
        }
    }
}
