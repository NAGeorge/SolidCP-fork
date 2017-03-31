using AjaxControlToolkit;
using SolidCP.EnterpriseServer;
using SolidCP.Portal;
using SolidCP.Portal.ExchangeServer.UserControls;
using SolidCP.Portal.UserControls;
using SolidCP.Providers.HostedSolution;
using SolidCP.Providers.ResultObjects;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SolidCP.Portal.ExchangeServer.UserControls.MSO365
{
    public partial class O365UsersImport : SolidCPModuleBase
    {
        

        public O365UsersImport()
        {
        }

        protected void btnImportUsers_Click(object sender, EventArgs e)
        {
            this.locationPanel.Visible = this.locationSelector.IsLocationAllowed;
            this.ServiceLevelo365Row.Visible = this.serviceLevelSelector.IsServiceLevelRequired;
            ExchangeAccount[] o365UsersForImport = ES.Services.MicrosoftOnline.GetO365UsersForImport(PanelRequest.ItemID);
            this.gvO365UsersImport.DataSource = o365UsersForImport;
            this.gvO365UsersImport.DataBind();
            this.Modal.Show();
        }

        protected void btnModalCancel_OnClick(object sender, EventArgs e)
        {
            this.Modal.Hide();
        }

        protected void btnModalImportUsers_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                this.Modal.Show();
                return;
            }
            SimpleMessageBox simpleMessageBox = (SimpleMessageBox)this.Parent.FindControl("messageBox");
            List<ExchangeAccount> gridViewAccounts = this.GetGridViewAccounts(AccountsList.SelectedState.Selected);
            IntResult intResult = ES.Services.MicrosoftOnline.ImportO365Users(PanelRequest.ItemID, gridViewAccounts.ToArray(), this.locationSelector.LocationIdInt, this.serviceLevelSelector.ServiceLevelIdInt);
            if (intResult.Value < 0)
            {
                simpleMessageBox.ShowResultMessage(intResult.Value);
                this.RefershPage();
                return;
            }
            simpleMessageBox.ShowMessage(intResult, "MOS_IMPORT_O365_USERS", null);
            this.RefershPage();
        }

        private List<ExchangeAccount> GetGridViewAccounts(AccountsList.SelectedState state)
        {
            List<ExchangeAccount> exchangeAccounts = new List<ExchangeAccount>();
            for (int i = 0; i < this.gvO365UsersImport.Rows.Count; i++)
            {
                GridViewRow item = this.gvO365UsersImport.Rows[i];
                CheckBox checkBox = (CheckBox)item.FindControl("chkSelect");
                if (checkBox != null)
                {
                    ExchangeAccount exchangeAccount = new ExchangeAccount()
                    {
                        UserPrincipalName = (string)this.gvO365UsersImport.DataKeys[i][0],
                        DisplayName = ((Literal)item.FindControl("litDisplayName")).Text,
                        Country = ((HiddenField)item.FindControl("hdnCountry")).Value
                    };
                    if (state == AccountsList.SelectedState.All || state == AccountsList.SelectedState.Selected && checkBox.Checked || state == AccountsList.SelectedState.Unselected && !checkBox.Checked)
                    {
                        exchangeAccounts.Add(exchangeAccount);
                    }
                }
            }
            return exchangeAccounts;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void RefershPage()
        {
            ((GridView)this.Parent.FindControl("gvUsers")).DataBind();
        }
    }
}