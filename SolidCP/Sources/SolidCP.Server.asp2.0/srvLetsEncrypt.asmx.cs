// Copyright (c) 2016, SolidCP
// SolidCP is distributed under the Creative Commons Share-alike license
// 
// SolidCP is a fork of WebsitePanel:
// Copyright (c) 2015, Outercurve Foundation.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
// - Redistributions of source code must  retain  the  above copyright notice, this
//   list of conditions and the following disclaimer.
//
// - Redistributions in binary form  must  reproduce the  above  copyright  notice,
//   this list of conditions  and  the  following  disclaimer in  the documentation
//   and/or other materials provided with the distribution.
//
// - Neither  the  name  of  the  Outercurve Foundation  nor   the   names  of  its
//   contributors may be used to endorse or  promote  products  derived  from  this
//   software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,  BUT  NOT  LIMITED TO, THE IMPLIED
// WARRANTIES  OF  MERCHANTABILITY   AND  FITNESS  FOR  A  PARTICULAR  PURPOSE  ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL,  SPECIAL,  EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO,  PROCUREMENT  OF  SUBSTITUTE  GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)  HOWEVER  CAUSED AND ON
// ANY  THEORY  OF  LIABILITY,  WHETHER  IN  CONTRACT,  STRICT  LIABILITY,  OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE)  ARISING  IN  ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Microsoft.Web.Services3;

using SolidCP.Providers;
using SolidCP.Providers.Web;
using SolidCP.Server.Utils;
using SolidCP.Providers.ResultObjects;
using SolidCP.Providers.WebAppGallery;
using SolidCP.Providers.Common;
using Microsoft.Web.Administration;
using Microsoft.Web.Management.Server;
using SolidCP.Providers.HostedSolution;
using System.Diagnostics;
using SolidCP.Server;


//using ACMESharp;
//using ACMESharp.ACME;
//using ACMESharp.JOSE;

namespace SolidCP.Providers.LetsEncrypt
{
    /// <summary>
    /// Summary description for WebServer
    /// </summary>
    [WebService(Namespace = "http://smbsaas/solidcp/server/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [Policy("ServerPolicy")]
    [ToolboxItem(false)]
    public class srvLetsEncrypt : HostingServiceProviderWebService//, ILetsEncrypt
    {
       
        private void Eventlog(string Log, int EventNum, EventLogEntryType EventType)
        {
            string EventLogSource = "SolidCP.LetsEncypt";

            if (!EventLog.SourceExists(EventLogSource) ) { EventLog.CreateEventSource(EventLogSource, "SolidCP"); }

            EventLog.WriteEntry(EventLogSource, Log, EventType, EventNum);
        }
        public ILetsEncrypt LEProvider
        {
            get { return (ILetsEncrypt)Provider; }
        }

        #region LetsEncrypt
        [WebMethod, SoapHeader("settings")]
        public LERegistrationResult srvRegisterLetsEncrypt(LERequest request )
        {
            LERegistrationResult LEResult = new LERegistrationResult();
            try
            {
                Log.WriteStart("'{0}' LetsEncrpt Register", ProviderSettings.ProviderName);  

                string PFXPassword = new Guid().ToString().Replace("-", "");
                LEResult = LEProvider.GenerateLECert(request.URLs, request.contacts, request.siteinfo,  request.settings, true); 

                Log.WriteEnd("'{0}' LetsEncrpt Register", ProviderSettings.ProviderName);
            }
            catch (Exception ex)
            {
                Log.WriteError(String.Format("'{0}' LetsEncrpt Register", ProviderSettings.ProviderName), ex);
                throw;
            }

            return LEResult;
        }

        [WebMethod, SoapHeader("settings")]
        public LERegistrationResult srvRenewLetsEncrypt(SSLCertificate SSLCert, int SiteId, string[] Urls, LetsEncryptSettings settings, string[] contacts, WebSite website)
        {

            LERegistrationResult result = new LERegistrationResult();

            try
            {
                Log.WriteStart("'{0}' LetsEncrpt Register", ProviderSettings.ProviderName);
                result = LEProvider.RenewLECert(Urls, contacts, website, settings);
                
                Log.WriteEnd("'{0}' LetsEncrpt Register", ProviderSettings.ProviderName);

            }
            catch (Exception ex)
            {
                Log.WriteError(String.Format("'{0}' LetsEncrpt Register", ProviderSettings.ProviderName), ex);
                throw;
            }

            return result;
        }

        [WebMethod, SoapHeader("settings")]
        public bool srvRemoveLetsEncrypt( WebSite site)
        {

            bool result;

            try
            {
                Log.WriteStart("LetsEncrpt Remove SSL '{0}'", site.Name);
                // WebServer ser

                result = LEProvider.RemoveLECert(site);
                Log.WriteEnd("LetsEncrpt Remove SSL '{0}'", ProviderSettings.ProviderName);

            }
            catch (Exception ex)
            {
                Log.WriteError(String.Format("'{0}' LetsEncrpt Register", ProviderSettings.ProviderName), ex);
                throw;
            }

            return result;
        }

        #endregion

    }
}
