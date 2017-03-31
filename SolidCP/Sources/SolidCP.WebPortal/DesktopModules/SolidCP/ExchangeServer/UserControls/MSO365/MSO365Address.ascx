<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MSO365Address.aspx.cs" Inherits="SolidCP.Portal.ExchangeServer.UserControls.MSO365.MSO365Address" %>
<%@ Register Src="../CountrySelector.ascx" TagName="CountrySelector" TagPrefix="wsp" %>
<%@ Register Src="../StateSelector.ascx" TagName="StateSelector" TagPrefix="wsp" %>

<table cellpadding="2">
    <tr>
        <td class="Normal" style="width: 150px;">
            <asp:Label ID="lblFirstName" runat="server"
                       meta:resourcekey="lblFirstName" Text="First Name:"/>
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="NormalTextBox" Width="400px"/>
            <asp:RequiredFieldValidator ID="valReqFirstName" runat="server" ControlToValidate="txtFirstName" meta:resourcekey="valReqFirstName"/>
        </td>
    </tr>
    <tr>
        <td class="Normal" style="width: 150px;">
            <asp:Label ID="lblLastName" runat="server"
                       meta:resourcekey="lblLastName" Text="Last Name:"/>
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtLastName" runat="server" CssClass="NormalTextBox" Width="400px"/>
            <asp:RequiredFieldValidator ID="valReqLastName" runat="server" ControlToValidate="txtLastName" meta:resourcekey="valReqLastName"/>
        </td>
    </tr>
    
     <tr>
        <td class="Normal" style="width: 150px;">
            <asp:Label ID="lblAddressLine1" runat="server"
                       meta:resourcekey="lblAddressLine1" Text="Address Line 1:"/>
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="NormalTextBox" Width="400px"/>
            <asp:RequiredFieldValidator ID="valReqAddressLine1" runat="server" ControlToValidate="txtAddressLine1" meta:resourcekey="valReqAddressLine1"/>
            <asp:RegularExpressionValidator ID="valRangeAddressLine1" ControlToValidate="txtAddressLine1" runat="server" ValidationExpression="^[\s\S]{1,255}$" meta:resourcekey="valRangeAddressLine1"/>
        </td>
    </tr>
    
    <tr>
        <td class="Normal" style="width: 150px;">
            <asp:Label ID="lblAddressLine2" runat="server"
                       meta:resourcekey="lblAddressLine2" Text="Address Line 2:"/>
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="NormalTextBox" Width="400px"/>
            <asp:RegularExpressionValidator ID="valRangeAddressLine2" ControlToValidate="txtAddressLine2" runat="server" ValidationExpression="^[\s\S]{0,255}$" meta:resourcekey="valRangeAddressLine2"/>
        </td>
    </tr>
    
    <tr>
        <td class="Normal" style="width: 150px;">
            <asp:Label ID="lblPhoneNumber" runat="server"
                       meta:resourcekey="lblPhoneNumber" Text="Phone Number:"/>
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="NormalTextBox" Width="400px"/>
            <asp:RequiredFieldValidator ID="valReqPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber" meta:resourcekey="valReqPhoneNumber"/>
            <asp:RegularExpressionValidator ID="valRegPhoneNumber" ControlToValidate="txtPhoneNumber" runat="server" ValidationExpression="^(1[ \-\/\.]?)?(\((\d{3})\)|(\d{3}))[ \-\/\.]?(\d{3})[ \-\/\.]?(\d{4})$" meta:resourcekey="valRegPhoneNumber"/>
        </td>
    </tr>

     <tr>
        <td class="Normal" style="width: 150px;">
            <asp:Label ID="lblCity" runat="server"
                       meta:resourcekey="lblCity" Text="City:"/>
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtCity" runat="server" CssClass="NormalTextBox" Width="400px"/>
            <asp:RequiredFieldValidator ID="valReqCity" runat="server" ControlToValidate="txtCity" meta:resourcekey="valReqCity"/>
            <asp:RegularExpressionValidator ID="valRangeCity" ControlToValidate="txtCity" runat="server" ValidationExpression="^[\s\S]{1,128}$" meta:resourcekey="valRangeCity"/>
        </td>
    </tr>
        
    <tr>
        <td class="Normal" style="width: 150px;">
            <asp:Label ID="lblCountry" runat="server"
                       meta:resourcekey="lblCountry" Text="Country:"/>
        </td>
        <td class="Normal">
            <wsp:CountrySelector id="country" runat="server" ExcludeDefault="True" SetDefaultValue="True"/>
        </td>
    </tr>

      <tr>
        <td class="Normal" style="width: 150px;">
            <asp:Label ID="lblRegion" runat="server"
                       meta:resourcekey="lblRegion" Text="Region (State):"/>
        </td>
        <td class="Normal">
            <wsp:StateSelector id="stateSelector" runat="server"/>
<%--             <asp:TextBox ID="txtRegion" runat="server" CssClass="NormalTextBox" Width="400px"/>
           <asp:RequiredFieldValidator ID="valReqRegion" runat="server" ControlToValidate="txtRegion" meta:resourcekey="valReqRegion"/>
            <asp:RegularExpressionValidator ID="valRangeRegion" ControlToValidate="txtRegion" runat="server" ValidationExpression="^[\s\S]{0,128}$" meta:resourcekey="valRangeRegion"/>--%>
        </td>
    </tr>
    <tr>
        <td class="Normal" style="width: 150px;">
            <asp:Label ID="lblPostalCode" runat="server"
                       meta:resourcekey="lblPostalCode" Text="Postal Code:" />
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="NormalTextBox" Width="400px" />
            <asp:RequiredFieldValidator ID="valReqPostalCode" runat="server" ControlToValidate="txtPostalCode" meta:resourcekey="valReqPostalCode"/>
            <asp:RegularExpressionValidator ID="valRangePostalCode" ControlToValidate="txtPostalCode" ErrorMessage="Incorrect format" Text="Incorrect format" runat="server" ValidationExpression=""/>
        </td>
    </tr>

</table>
