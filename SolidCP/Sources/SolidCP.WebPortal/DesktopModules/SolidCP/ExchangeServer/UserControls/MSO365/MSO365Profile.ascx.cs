using SolidCP.Portal;
using SolidCP.Portal.ExchangeServer.UserControls;
using SolidCP.Portal.SkinControls;
using SolidCP.Providers.MicrosoftOnlineServices.Office365;
using System;
using System.Web.UI.WebControls;

namespace SolidCP.Portal.ExchangeServer.UserControls.MSO365
{
    public partial class MSO365Profile : SolidCPModuleBase
    {
        protected CollapsiblePanel colo365Profile;
        protected CollapsiblePanel col365Address1;
        protected Panel panel365Address1;
        protected MSO365Address o365Address1;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string ValidationGroup
        {
            set
            {
                valReqCompanyName.ValidationGroup = value;
                valReqEmail.ValidationGroup = value;
                valRegEmail.ValidationGroup = value;
                valReqFirstName.ValidationGroup = value;
                valReqLastName.ValidationGroup = value;
                o365Address1.ValidationGroup = value;
            }
        }

        public MSO365Profile()
        {
        }

        public MSO365Address GetAddress1Control()
        {
            return o365Address1;
        }

        public O365Profile GetProfile()
        {
            return new O365Profile()
            {
                CompanyName = txtCompanyName.Text,
                Email = txtEmail.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Type = typeList.SelectedValue,
                Culture = ccCulture.GetValue(),
                Language = ccLanguage.GetValue(),
                Address = o365Address1.GetAddress()
            };
        }

        public void SetCompanyName(string companyName)
        {
            txtCompanyName.Text = companyName;
        }

        public void SetEmail(string email)
        {
            txtEmail.Text = email;
        }

        public void SetFirstName(string firstName)
        {
            txtFirstName.Text = firstName;
        }

        public void SetLastName(string lastName)
        {
            txtLastName.Text = lastName;
        }

        public void SetProfile(O365Profile profile)
        {
            if (profile == null)
            {
                return;
            }
            txtCompanyName.Text = profile.CompanyName;
            txtEmail.Text = profile.Email;
            txtFirstName.Text = profile.FirstName;
            txtLastName.Text = profile.LastName;
            ccCulture.SetValue(profile.Culture);
            ccLanguage.SetValue(profile.Language);
            this.o365Address1.SetAddress(profile.Address);
        }
    }
}