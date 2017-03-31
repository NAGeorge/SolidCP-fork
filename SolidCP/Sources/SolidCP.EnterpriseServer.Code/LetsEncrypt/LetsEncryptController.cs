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
using System.IO;
using System.Data;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

using SolidCP.Providers;
using SolidCP.Providers.HeliconZoo;
using SolidCP.Providers.Web;
using SolidCP.Providers.DNS;
using OS = SolidCP.Providers.OS;
using SolidCP.Providers.Common;
using SolidCP.Providers.ResultObjects;
using System.Resources;
using System.Threading;
using System.Reflection;
using SolidCP.Templates;
using SolidCP.Providers.Database;
using SolidCP.Providers.FTP;
using System.Collections;
using SolidCP.Server;
using SolidCP.Server.Client;
using Microsoft.Web.Services3;
using SolidCP.EnterpriseServer;
using SolidCP.Providers.LetsEncrypt;

namespace SolidCP.EnterpriseServer
{
    public class LetsEncryptController : IImportController, IBackupController 
    {
        private const string LOG_SOURCE_LE = "LetsEncrypt";

        public static LetsEncryptController GetLetsEncrypt(int serviceId)
        {
            LetsEncryptController le = new LetsEncryptController();

            //ServiceProviderProxy.Init(le, serviceId);  // 'le' not corrct var type - repro code here for now
// since this was put here instead of inside ServiceProviderProxy, is that causing the error???
            ServerProxyConfigurator cnfg = new ServerProxyConfigurator();

            // get service
            ServiceInfo service = ServerController.GetServiceInfo(serviceId);

            if (service == null)
                throw new Exception(String.Format("Service with ID {0} was not found", serviceId));

            // set service settings
            StringDictionary serviceSettings = ServerController.GetServiceSettings(serviceId);
            foreach (string key in serviceSettings.Keys)
                cnfg.ProviderSettings.Settings[key] = serviceSettings[key];

            // get provider
            ProviderInfo provider = ServerController.GetProvider(service.ProviderId);
            cnfg.ProviderSettings.ProviderGroupID = provider.GroupId;
            cnfg.ProviderSettings.ProviderCode = provider.ProviderName;
            cnfg.ProviderSettings.ProviderName = provider.DisplayName;
            cnfg.ProviderSettings.ProviderType = provider.ProviderType;


            return le;
        }


        #region LetsEncrypt
        public static ResultObject wscRegisterLetsEncrypt(LERequest request)
        {
            var result = new ResultObject();

            //var lep = new LetsEncryptProxy();

            //result = lep.srvRegisterLetsEncrypt(request);

			//var LetsEncryptSettings = SystemController.GetSystemSettings(SystemSettings.LETS_ENCRYPT);
            //WebSite ws = WebServerController.GetWebSite(SiteId) as WebSite;
            //WebServer server = WebServerController.GetWebServer(ws.ServiceId);
            //PackageInfo service = PackageController.GetPackage(ws.PackageId);

            var lep = new LetsEncryptProxy();
            lep.srvRegisterLetsEncrypt(request);

            return result;
        } 

        public static ResultObject wscRenewLetsEncrypt(SSLCertificate SSLCert,  int SiteId)
        {
            throw new NotImplementedException();
        }
        public static ResultObject wscRemoveLetsEncrypt(SSLCertificate SSLCert, int SiteId)
        {
            throw new NotImplementedException();
        }

        #endregion

        private static int GetLetsEncryptServiceID(int packageId)
        {
            return PackageController.GetPackageServiceId(packageId, ResourceGroups.LetsEncrypt);
        }


        // to be looked at later... the certs themselves will be covered by WebserverController... but we need to deal with the new ACME Signer and Registration too

        #region IBackupController Members

        public int BackupItem(string tempFolder, System.Xml.XmlWriter writer, ServiceProviderItem item, ResourceGroupInfo group)
        {
            throw new NotImplementedException();
        }

        public int RestoreItem(string tempFolder, XmlNode itemNode, int itemId, Type itemType, string itemName, int packageId, int serviceId, ResourceGroupInfo group)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IImportController

        public List<string> GetImportableItems(int packageId, int itemTypeId, Type itemType, ResourceGroupInfo group)
        {
            throw new NotImplementedException();
        }

        public void ImportItem(int packageId, int itemTypeId, Type itemType, ResourceGroupInfo group, string itemName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}