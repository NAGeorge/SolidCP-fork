using SolidCP.Providers.ResultObjects;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace SolidCP.EnterpriseServer
{
    public class InstallLetsEncryptCertificateCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        public StringArrayResultObject Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (StringArrayResultObject)this.results[0];
            }
        }

        internal InstallLetsEncryptCertificateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }
    }
}
