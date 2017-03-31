﻿using OpenSSL.Core;
using OpenSSL.Crypto;
using OpenSSL.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ACMESharp.PKI.Providers
{
    public class OpenSslLib64Provider : OpenSslLibBaseProvider
    {
        public OpenSslLib64Provider(IReadOnlyDictionary<string, string> initParams)
            : base(initParams)
        { }
    }
}
