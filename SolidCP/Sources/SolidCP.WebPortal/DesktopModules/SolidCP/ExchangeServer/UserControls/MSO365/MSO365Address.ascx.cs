using SolidCP.Portal;
using SolidCP.Portal.ExchangeServer.UserControls;
using SolidCP.Providers.MicrosoftOnlineServices.Office365;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SolidCP.Portal.ExchangeServer.UserControls.MSO365
{
    public partial class MSO365Address : SolidCPModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            country.SelectedIndexChangedCallback = new Action(CountrySelectedIndexChanged);
        }

        public string ValidationGroup
        {
            set
            {
                valReqFirstName.ValidationGroup = value;
                valReqLastName.ValidationGroup = value;
                valReqAddressLine1.ValidationGroup = value;
                valRangeAddressLine1.ValidationGroup = value;
                valReqCity.ValidationGroup = value;
                valRangeCity.ValidationGroup = value;
                valReqPostalCode.ValidationGroup = value;
                valRangePostalCode.ValidationGroup = value;
                valReqPhoneNumber.ValidationGroup = value;
                valRegPhoneNumber.ValidationGroup = value;
            }
        }

        public MSO365Address()
        {
        }

        public void CountrySelectedIndexChanged()
        {
            if (base.IsPostBack)
            {
                stateSelector.RefreshTxtState();
            }
            stateSelector.CountryCode = country.Country;
            stateSelector.RefreshItems();
            stateSelector.RefreshVisibleElements();
            stateSelector.Required = (stateSelector.IsDropDownListIsActive ? false : country.IsStateRequired);
            valReqPostalCode.Enabled = country.IsPostalCodeRequired;
            valRangePostalCode.ValidationExpression = country.PostalCodePattern;
        }

        public O365Address GetAddress()
        {
            return new O365Address()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                AddressLine1 = txtAddressLine1.Text,
                AddressLine2 = txtAddressLine2.Text,
                City = txtCity.Text,
                Region = stateSelector.State,
                PostalCode = txtPostalCode.Text,
                Country = country.Country,
                PhoneNumber = txtPhoneNumber.Text
            };
        }

        public void SetAddress(O365Address address)
        {
            txtFirstName.Text = address.FirstName;
            txtLastName.Text = address.LastName;
            txtAddressLine1.Text = address.AddressLine1;
            txtAddressLine2.Text = address.AddressLine2;
            txtCity.Text = address.City;
            country.Country = address.Country;
            stateSelector.State = address.Region;
            stateSelector.EditModeValue = address.Region;
            txtPostalCode.Text = address.PostalCode;
            txtPhoneNumber.Text = address.PhoneNumber;
        }

        public void SetAddressLine1(string address)
        {
            txtAddressLine1.Text = address;
        }

        public void SetCity(string city)
        {
            txtCity.Text = city;
        }

        public void SetCountry(string country)
        {
            CountrySelector countrySelector = country;
            string str = country;
            string str1 = str;
            country.EditModeValue = str;
            countrySelector.Country = str1;
        }

        public void SetFirstName(string name)
        {
            txtFirstName.Text = name;
        }

        public void SetLastName(string name)
        {
            txtLastName.Text = name;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            txtPhoneNumber.Text = phoneNumber;
        }

        public void SetPostalCode(string zip)
        {
            txtPostalCode.Text = zip;
        }

        public void SetState(string state)
        {
            StateSelector stateSelector = stateSelector;
            string str = state;
            string str1 = str;
            stateSelector.EditModeValue = str;
            stateSelector.State = str1;
        }
    }
}