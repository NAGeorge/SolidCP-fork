using SolidCP.Providers.Common;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace SolidCP.EnterpriseServer
{
    public class RenewLetsEncryptCertificateCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        public ResultObject Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (ResultObject)this.results[0];
            }
        }

        internal RenewLetsEncryptCertificateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }
    }
}
