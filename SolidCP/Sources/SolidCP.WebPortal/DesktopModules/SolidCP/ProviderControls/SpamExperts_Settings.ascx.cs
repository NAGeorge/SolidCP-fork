using SolidCP.Portal;
using SolidCP.Portal.SkinControls;
using System;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SolidCP.Portal.ProviderControls
{
    public partial class SpamExperts_Settings : SolidCPControlBase, IHostingServiceProviderSettings
    {
        private string emptyPassrows = "E0EBC312-A386-4598-8355-0E8AEA7604FB";

        private string SEDestinations = "sedestinations";

        public SpamExperts_Settings()
        {
        }

        public void BindSettings(StringDictionary settings)
        {
            Utils.SelectListItem(this.ddlSchema, settings["schema"]);
            this.txtUrl.Text = settings["url"];
            this.txtAdminUser.Text = settings["user"];
            this.txtAdminPassword.Text = this.emptyPassrows;
            List<string> strs = new List<string>();
            string item = settings[this.SEDestinations];
            if (item != null)
            {
                strs.AddRange(item.Split(new char[] { ',' }));
            }
            this.ViewState[this.SEDestinations] = strs;
            this.gvSEDestinations.DataSource = strs;
            this.gvSEDestinations.DataBind();
        }

        protected void bntAddSEDestination_Click(object sender, EventArgs e)
        {
            List<string> item = this.ViewState[this.SEDestinations] as List<string> ?? new List<string>();
            item.Add(this.tbSEDestinations.Text);
            this.ViewState[this.SEDestinations] = item;
            this.gvSEDestinations.DataSource = item;
            this.gvSEDestinations.DataBind();
        }

        protected void gvSEDestinations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                try
                {
                    string str = e.CommandArgument.ToString();
                    List<string> item = this.ViewState[this.SEDestinations] as List<string>;
                    if (item != null)
                    {
                        int num = item.FindIndex((string x) => x == str);
                        if (num >= 0)
                        {
                            item.RemoveAt(num);
                        }
                        this.ViewState[this.SEDestinations] = item;
                        this.gvSEDestinations.DataSource = item;
                        this.gvSEDestinations.DataBind();
                    }
                }
                catch
                {
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void SaveSettings(StringDictionary settings)
        {
            settings["schema"] = this.ddlSchema.SelectedValue;
            settings["url"] = this.txtUrl.Text;
            settings["user"] = this.txtAdminUser.Text;
            if (!string.IsNullOrEmpty(this.txtAdminPassword.Text) && this.txtAdminPassword.Text != this.emptyPassrows)
            {
                settings["password"] = this.txtAdminPassword.Text;
            }
            List<string> item = this.ViewState[this.SEDestinations] as List<string>;
            settings[this.SEDestinations] = string.Join(",", item.ToArray());
        }
    }
}