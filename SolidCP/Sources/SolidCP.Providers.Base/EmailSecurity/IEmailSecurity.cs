using System;

namespace SolidCP.Providers.EmailSecurity
{
    public interface IEmailSecurity
    {
        EmailSecurityResult AddDomain(string domain, string password, string email, string[] destinations);

        EmailSecurityResult AddEmail(string name, string domain, string password);

        EmailSecurityResult DeleteDomain(string domain);

        EmailSecurityResult DeleteEmail(string email);

        EmailSecurityResult SetDomainDestinations(string name, string[] destinations);

        EmailSecurityResult SetDomainUserPassword(string name, string password);

        EmailSecurityResult SetEmailPassword(string email, string password);
    }
}
