using Microsoft.Web.Services3;
using SolidCP.Providers;
using SolidCP.Providers.EmailSecurity;
using System;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace SolidCP.Server
{
    [Policy("ServerPolicy")]
    [ToolboxItem(false)]
    [WebService(Namespace = "http://smbsaas/solidcp/server/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class EmailSecurity : HostingServiceProviderWebService, IEmailSecurity
    {
        private IEmailSecurity EmailSecurityProvider
        {
            get
            {
                return (IEmailSecurity)base.Provider;
            }
        }

        public EmailSecurity()
        {
        }

        [SoapHeader("settings")]
        [WebMethod]
        public EmailSecurityResult AddDomain(string domain, string password, string email, string[] destinations)
        {
            return this.EmailSecurityProvider.AddDomain(domain, password, email, destinations);
        }

        [SoapHeader("settings")]
        [WebMethod]
        public EmailSecurityResult AddEmail(string name, string domain, string password)
        {
            return this.EmailSecurityProvider.AddEmail(name, domain, password);
        }

        [SoapHeader("settings")]
        [WebMethod]
        public EmailSecurityResult DeleteDomain(string domain)
        {
            return this.EmailSecurityProvider.DeleteDomain(domain);
        }

        [SoapHeader("settings")]
        [WebMethod]
        public EmailSecurityResult DeleteEmail(string email)
        {
            return this.EmailSecurityProvider.DeleteEmail(email);
        }

        [SoapHeader("settings")]
        [WebMethod]
        public EmailSecurityResult SetDomainDestinations(string name, string[] destinations)
        {
            return this.EmailSecurityProvider.SetDomainDestinations(name, destinations);
        }

        [SoapHeader("settings")]
        [WebMethod]
        public EmailSecurityResult SetDomainUserPassword(string name, string password)
        {
            return this.EmailSecurityProvider.SetDomainUserPassword(name, password);
        }

        [SoapHeader("settings")]
        [WebMethod]
        public EmailSecurityResult SetEmailPassword(string email, string password)
        {
            return this.EmailSecurityProvider.SetEmailPassword(email, password);
        }
    }
}