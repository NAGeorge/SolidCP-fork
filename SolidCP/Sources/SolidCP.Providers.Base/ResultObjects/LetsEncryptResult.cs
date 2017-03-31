using System;
using System.Runtime.CompilerServices;

namespace SolidCP.Providers.ResultObjects
{
    public class LetsEncryptResult : StringArrayResultObject
    {
        public byte[] Certificate
        {
            get;
            set;
        }

        public string ErrorDescription
        {
            get;
            set;
        }

        public LetsEncryptResult()
        {
        }
    }
}
