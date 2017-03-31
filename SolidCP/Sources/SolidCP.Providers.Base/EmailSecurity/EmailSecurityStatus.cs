using System;

namespace SolidCP.Providers.EmailSecurity
{
    [Serializable]
    public enum EmailSecurityStatus
    {
        Success,
        Error,
        AlreadyExists,
        NotFound,
        None
    }
}
