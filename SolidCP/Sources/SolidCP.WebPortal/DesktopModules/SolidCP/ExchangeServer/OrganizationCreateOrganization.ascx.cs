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
using System.Collections.Generic;
using System.Linq;
using SolidCP.EnterpriseServer;
using SolidCP.Providers.HostedSolution;

namespace SolidCP.Portal.ExchangeServer
{
    public partial class OrganizationCreateOrganization : SolidCPModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DomainInfo[] domains = ES.Services.Servers.GetMyDomains(PanelSecurity.PackageId).Where(d => !Utils.IsIdnDomain(d.DomainName)).ToArray();
            Organization[] orgs = ES.Services.Organizations.GetOrganizations(PanelSecurity.PackageId, false);
            var list = new List<OrganizationDomainName>();
            SetPolicy(PanelSecurity.PackageId, UserSettings.EXCHANGE_POLICY, "OrgIdPolicy");

            foreach (Organization o in orgs)
            {
                OrganizationDomainName[] tmpList = ES.Services.Organizations.GetOrganizationDomains(o.Id);

                foreach (OrganizationDomainName name in tmpList)
                {
                    list.Add(name);
                }
            }

            foreach (DomainInfo d in domains)
            {
                if (!d.IsDomainPointer)
                {
                    bool bAdd = true;
                    foreach (OrganizationDomainName acceptedDomain in list)
                    {
                        if (d.DomainName.ToLower() == acceptedDomain.DomainName.ToLower())
                        {
                            bAdd = false;
                            break;
                        }
                    }
                    if (bAdd)
                    {
                        ddlDomains.Items.Add(d.DomainName.ToLower());
                        //Add Mail Cleaner
                        Knom.Helpers.Net.APIMailCleanerHelper.DomainAdd(d.DomainName.ToLower());
                    }
                }
            }

            if (ddlDomains.Items.Count == 0)
            {
                ddlDomains.Visible = btnCreate.Enabled = false;
            }
        }

        public void SetPolicy(int packageId, string settingsName, string key)
        {
            PackageInfo package = PackagesHelper.GetCachedPackage(packageId);

            if (package != null)
            {
                SetOrgIdPolicy(package.UserId, settingsName, key);
            }
        }

        public void SetOrgIdPolicy(int userId, string settingsName, string key)
        {
            UserInfo user = UsersHelper.GetCachedUser(userId);

            if (user != null)
            {
                UserSettings settings = ES.Services.Users.GetUserSettings(userId, settingsName);

                if (settings != null && settings["OrgIdPolicy"] != null)
                {
                    SetOrgIdPolicy(settings);
                }
            }
        }

        private void SetOrgIdPolicy(UserSettings settings)
        {
            string policyValue = settings["OrgIdPolicy"];
            string[] values = policyValue.Split(';');

            if (values.Length > 1 && Convert.ToBoolean(values[0]))
            {
                try
                {
                    int maxLength = Convert.ToInt32(values[1]);
                    txtOrganizationID.MaxLength = maxLength;
                    valRequireCorrectOrgID.ValidationExpression = string.Format("[a-zA-Z0-9.-]{{1,{0}}}", maxLength);
                }
                catch (Exception)
                {
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            CreateOrganization();
        }

        private void CreateOrganization()
        {
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                int itemId = ES.Services.Organizations.CreateOrganization(PanelSecurity.PackageId, txtOrganizationID.Text.Trim().ToLower(), txtOrganizationName.Text.Trim().ToLower(), ddlDomains.SelectedValue.Trim().ToLower());

                if (itemId < 0)
                {
                    messageBox.ShowResultMessage(itemId);
                    return;
                }

                Response.Redirect(EditUrl("SpaceID", PanelSecurity.PackageId.ToString(), "organization_home", "ItemID=" + itemId));
            }
            catch (Exception ex)
            {
                messageBox.ShowErrorMessage("ORGANIZATION_CREATE_ORG", ex);
            }
        }
    }
}
