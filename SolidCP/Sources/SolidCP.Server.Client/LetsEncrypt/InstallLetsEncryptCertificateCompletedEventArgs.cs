using SolidCP.Providers.ResultObjects;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace SolidCP.Providers.Web
{
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [GeneratedCode("wsdl", "2.0.50727.42")]
    public class InstallLetsEncryptCertificateCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        public LetsEncryptResult Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (LetsEncryptResult)this.results[0];
            }
        }

        internal InstallLetsEncryptCertificateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }
    }
}
