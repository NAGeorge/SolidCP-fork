using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SolidCP.Portal.ExchangeServer.UserControls.MSO365
{
    public partial class MSO365UserRolesEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public MSO365UserRolesEditor()
        {
        }
        public O365UserRoles GetUserRole()
        {
            if (this.rbtnUserRole.Checked)
            {
                return O365UserRoles.User;
            }
            if (this.rbtnGlobalAdminRole.Checked)
            {
                return O365UserRoles.GlobalAdministrator;
            }
            O365UserRoles o365UserRole = O365UserRoles.User;
            if (this.chkBillingAdminRole.Checked)
            {
                o365UserRole = o365UserRole.Add<O365UserRoles>(O365UserRoles.BillingAdministrator);
            }
            if (this.chkExchangeAdminRole.Checked)
            {
                o365UserRole = o365UserRole.Add<O365UserRoles>(O365UserRoles.ExchangeAdministrator);
            }
            if (this.chkPasswordAdminRole.Checked)
            {
                o365UserRole = o365UserRole.Add<O365UserRoles>(O365UserRoles.PasswordAdministrator);
            }
            if (this.chkServiceAdminRole.Checked)
            {
                o365UserRole = o365UserRole.Add<O365UserRoles>(O365UserRoles.ServiceAdministrator);
            }
            if (this.chkSharePointAdminRole.Checked)
            {
                o365UserRole = o365UserRole.Add<O365UserRoles>(O365UserRoles.SharePointAdministrator);
            }
            if (this.chkSkypeAdminRole.Checked)
            {
                o365UserRole = o365UserRole.Add<O365UserRoles>(O365UserRoles.SkypeForBusinessAdministrator);
            }
            if (this.chkUserManagementAdminRole.Checked)
            {
                o365UserRole = o365UserRole.Add<O365UserRoles>(O365UserRoles.UserManagementAdministrator);
            }
            o365UserRole = o365UserRole.Remove<O365UserRoles>(O365UserRoles.User);
            return o365UserRole;
        }
        protected void rbtnCustomizedAdminRole_OnCheckedChanged(object sender, EventArgs e)
        {
            this.rowCustomAdminRole.Visible = this.rbtnCustomizedAdminRole.Checked;
        }

        public void SetUserRole(O365UserRoles roles)
        {
            if (roles.HasFlag(O365UserRoles.User))
            {
                this.rbtnUserRole.Checked = true;
                return;
            }
            if (roles.HasFlag(O365UserRoles.GlobalAdministrator))
            {
                this.rbtnGlobalAdminRole.Checked = true;
                return;
            }
            this.rbtnCustomizedAdminRole.Checked = true;
            if (roles.HasFlag(O365UserRoles.BillingAdministrator))
            {
                this.chkBillingAdminRole.Checked = true;
            }
            if (roles.HasFlag(O365UserRoles.ExchangeAdministrator))
            {
                this.chkExchangeAdminRole.Checked = true;
            }
            if (roles.HasFlag(O365UserRoles.PasswordAdministrator))
            {
                this.chkPasswordAdminRole.Checked = true;
            }
            if (roles.HasFlag(O365UserRoles.ServiceAdministrator))
            {
                this.chkServiceAdminRole.Checked = true;
            }
            if (roles.HasFlag(O365UserRoles.SharePointAdministrator))
            {
                this.chkSharePointAdminRole.Checked = true;
            }
            if (roles.HasFlag(O365UserRoles.SkypeForBusinessAdministrator))
            {
                this.chkSkypeAdminRole.Checked = true;
            }
            if (roles.HasFlag(O365UserRoles.UserManagementAdministrator))
            {
                this.chkUserManagementAdminRole.Checked = true;
            }
            if (!this.rbtnCustomizedAdminRole.Checked && !this.rbtnGlobalAdminRole.Checked && !this.rbtnUserRole.Checked)
            {
                this.rbtnUserRole.Checked = true;
            }
            this.rbtnCustomizedAdminRole_OnCheckedChanged(null, null);
        }


    }
}