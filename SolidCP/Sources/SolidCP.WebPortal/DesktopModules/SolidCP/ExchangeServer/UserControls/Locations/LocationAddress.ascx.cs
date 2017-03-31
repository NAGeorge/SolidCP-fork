using SolidCP.Portal.ExchangeServer.UserControls;
using System;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SolidCP.Portal.ExchangeServer.UserControls.Locations
{
    public partial class LocationAddress : UserControl
    {
        public string Address2
        {
            get
            {
                return this.txtAddress2.Text;
            }
            set
            {
                this.txtAddress2.Text = value;
            }
        }

        public string Address2Caption
        {
            get
            {
                return this.locAddress2.Text;
            }
            set
            {
                this.locAddress2.Text = value;
            }
        }

        public bool Address2Visible
        {
            get
            {
                return this.divAddress2.Visible;
            }
            set
            {
                this.divAddress2.Visible = value;
            }
        }

        public string AddressCaption
        {
            get
            {
                return this.locAddress.Text;
            }
            set
            {
                this.locAddress.Text = value;
            }
        }

        public string City
        {
            get
            {
                return this.txtCity.Text;
            }
            set
            {
                this.txtCity.Text = value;
            }
        }

        public string CityCaption
        {
            get
            {
                return this.lblCity.Text;
            }
            set
            {
                this.lblCity.Text = value;
            }
        }

        public string Country
        {
            get
            {
                return this.country.Country;
            }
            set
            {
                this.country.EditModeValue = value;
                this.country.Country = value;
            }
        }

        public string CountryCaption
        {
            get
            {
                return this.lblCountry.Text;
            }
            set
            {
                this.lblCountry.Text = value;
            }
        }

        public string Fax
        {
            get
            {
                return this.txtFax.Text;
            }
            set
            {
                this.txtFax.Text = value;
            }
        }

        public bool FaxVisible
        {
            get
            {
                return this.divFax.Visible;
            }
            set
            {
                this.divFax.Visible = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return this.txtPhoneNumber.Text;
            }
            set
            {
                this.txtPhoneNumber.Text = value;
            }
        }

        public bool PhoneVisible
        {
            get
            {
                return this.divPhone.Visible;
            }
            set
            {
                this.divPhone.Visible = value;
            }
        }

        public string PostalCode
        {
            get
            {
                return this.txtPostalCode.Text;
            }
            set
            {
                this.txtPostalCode.Text = value;
            }
        }

        public string PostalCodeCaption
        {
            get
            {
                return this.lblPostalCode.Text;
            }
            set
            {
                this.lblPostalCode.Text = value;
            }
        }

        public bool PostalCodeRequired { get; set; } = true;

        public string RegionCaption
        {
            get
            {
                return this.lblRegion.Text;
            }
            set
            {
                this.lblRegion.Text = value;
            }
        }

        public string SecondaryPhone
        {
            get
            {
                return this.txtSecondaryPhone.Text;
            }
            set
            {
                this.txtSecondaryPhone.Text = value;
            }
        }

        public bool SecondaryPhoneVisible
        {
            get
            {
                return this.divSecPhone.Visible;
            }
            set
            {
                this.divSecPhone.Visible = value;
            }
        }

        public string State
        {
            get
            {
                return this.stateSelector.State;
            }
            set
            {
                this.stateSelector.EditModeValue = value;
                this.stateSelector.State = value;
            }
        }

        public string Street
        {
            get
            {
                return this.txtAddress.Text;
            }
            set
            {
                this.txtAddress.Text = value;
            }
        }

        public bool ValidationEnabled
        {
            get
            {
                return this.valRangePostalCode.Enabled;
            }
            set
            {
                this.valRangePostalCode.Enabled = value;
            }
        }

        public string ValidationGroup
        {
            set
            {
                this.stateSelector.ValidationGroup = value;
                this.valReqPostalCode.ValidationGroup = value;
                this.valRangePostalCode.ValidationGroup = value;
            }
        }

        public LocationAddress()
        {
        }

        public void CountrySelectedIndexChanged()
        {
            bool flag;
            if (base.IsPostBack)
            {
                this.stateSelector.RefreshTxtState();
            }
            this.stateSelector.CountryCode = this.country.Country;
            this.stateSelector.RefreshItems();
            this.stateSelector.RefreshVisibleElements();
            StateSelector stateSelector = this.stateSelector;
            if (!this.ValidationEnabled)
            {
                flag = false;
            }
            else
            {
                flag = (this.stateSelector.IsDropDownListIsActive ? false : this.country.IsStateRequired);
            }
            stateSelector.Required = flag;
            this.valReqPostalCode.Enabled = (!this.ValidationEnabled ? false : false);
            this.valRangePostalCode.ValidationExpression = this.country.PostalCodePattern;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.country.SelectedIndexChangedCallback = new Action(this.CountrySelectedIndexChanged);
        }
    }
}