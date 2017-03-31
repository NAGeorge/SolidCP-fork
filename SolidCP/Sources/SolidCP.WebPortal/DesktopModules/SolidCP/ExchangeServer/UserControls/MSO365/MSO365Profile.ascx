<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MSO365Profile.aspx.cs" Inherits="SolidCP.Portal.ExchangeServer.UserControls.MSO365.MSO365Profile" %>

<%@ Register Src="../CultureControl.ascx" TagName="CultureControl" TagPrefix="wsp" %>
<%@ Register Src="./Office365Address.ascx" TagName="o365Address" TagPrefix="wsp" %>
<%@ Register TagPrefix="wsp" TagName="CollapsiblePanel" Src="../../../UserControls/CollapsiblePanel.ascx" %>

<%@ Register Src="../../../SkinControls/BootstrapDropDownList.ascx" TagName="BootstrapDropDownList" TagPrefix="mspc" %>


<wsp:CollapsiblePanel ID="colo365Profile" runat="server" TargetControlID="panelo365Profile" meta:ResourceKey="colo365Profile" Text="Profile Info"/>

<asp:Panel runat="server" ID="panelo365Profile">

    <table cellpadding="2">
        <tr>
            <td class="Normal" style="width: 150px;">
                <asp:Label ID="lblCompanyName" runat="server"
                           meta:resourcekey="lblCompanyName" Text="Company Name:"/>
            </td>
            <td class="Normal">
                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="NormalTextBox" Width="400px"/>
                <asp:RequiredFieldValidator ID="valReqCompanyName" runat="server" ControlToValidate="txtCompanyName" meta:resourcekey="valReqCompanyName"/>
            </td>
        </tr>
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
                <asp:Label ID="lblEmail" runat="server"
                           meta:resourcekey="lblEmail" Text="Email:"/>
            </td>
            <td class="Normal">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="NormalTextBox" Width="400px"/>
                <asp:RequiredFieldValidator ID="valReqEmail" runat="server" ControlToValidate="txtEmail" meta:resourcekey="valReqEmail"/>
                <asp:RegularExpressionValidator ID="valRegEmail" ControlToValidate="txtEmail" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="valRegEmail"/>
            </td>
        </tr>
        
         <tr>
            <td class="Normal" style="width: 150px;">
                <asp:Label ID="lblCulture" runat="server"
                           meta:resourcekey="lblCulture" Text="Culture:"/>
            </td>
            <td class="Normal">
                <wsp:CultureControl ID="ccCulture" runat="server" SetDefaultValue="True"/>
            </td>
        </tr>
    
        <tr>
            <td class="Normal" style="width: 150px;">
                <asp:Label ID="lblLanguage" runat="server" 
                           meta:resourcekey="lblLanguage" Text="Language:"/>
            </td>
            <td class="Normal">
                <wsp:CultureControl runat="server" ID="ccLanguage" Language="True" SetDefaultValue="True"/>
            </td>
        </tr>
        <tr>
            <td class="Normal" style="width: 150px;">
                <asp:Label ID="lblProfileType" runat="server" 
                           meta:resourcekey="lblProfileType" Text="Type:"/>
            </td>
            <td class="Normal">
                <mspc:BootstrapDropDownList runat="server" ID="typeList" CssClass="InlineControl">
                   <asp:ListItem Text="organization" Value="organization">Organization</asp:ListItem>
                </mspc:BootstrapDropDownList>
            </td>
        </tr>
    
    </table>
</asp:Panel>

    <wsp:CollapsiblePanel ID="col365Address1" runat="server" TargetControlID="panel365Address1" meta:ResourceKey="col365Address1" Text="Default address"></wsp:CollapsiblePanel>

    <asp:Panel runat="server" ID="panel365Address1">
        <wsp:o365Address ID="o365Address1" runat="server"/>
    </asp:Panel>
