<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MSO365UserRolesEditor.aspx.cs" Inherits="SolidCP.Portal.ExchangeServer.UserControls.MSO365.MSO365UserRolesEditor" %>

<%@ Register TagPrefix="wsp" TagName="CollapsiblePanel" Src="../../../UserControls/CollapsiblePanel.ascx" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <wsp:CollapsiblePanel ID="colO365UserRoles" runat="server" TargetControlID="panelO365UserRoles" meta:ResourceKey="colO365UserRoles" Text="Roles" />

        <asp:Panel runat="server" ID="panelO365UserRoles">
            <table>
                <tr>
                    <td class="Normal">
                        <asp:RadioButton ID="rbtnUserRole" GroupName="O365Role" runat="server" CssClass="NormalTextBox" meta:resourcekey="rbtnUserRole" OnCheckedChanged="rbtnCustomizedAdminRole_OnCheckedChanged" AutoPostBack="True" />
                    </td>
                </tr>
                <tr>
                    <td class="Normal">
                        <asp:RadioButton ID="rbtnGlobalAdminRole" GroupName="O365Role" runat="server" CssClass="NormalTextBox" meta:resourcekey="rbtnGlobalAdminRole" OnCheckedChanged="rbtnCustomizedAdminRole_OnCheckedChanged" AutoPostBack="True" />
                    </td>
                </tr>
                <tr>
                    <td class="Normal">
                        <asp:RadioButton ID="rbtnCustomizedAdminRole" GroupName="O365Role" runat="server" CssClass="NormalTextBox" meta:resourcekey="rbtnCustomizedAdminRole" OnCheckedChanged="rbtnCustomizedAdminRole_OnCheckedChanged" AutoPostBack="True" />
                    </td>
                </tr>
                <tr id="rowCustomAdminRole" runat="server" visible="False">
                    <td class="Normal">
                        <div style="padding-left: 10px;">
                            <table>
                                <tr>
                                    <td class="Normal">
                                        <asp:CheckBox ID="chkBillingAdminRole" runat="server" CssClass="NormalTextBox" meta:resourcekey="chkBillingAdminRole" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Normal">
                                        <asp:CheckBox ID="chkExchangeAdminRole" runat="server" CssClass="NormalTextBox" meta:resourcekey="chkExchangeAdminRole" Text="Exchange administrator" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Normal">
                                        <asp:CheckBox ID="chkPasswordAdminRole" runat="server" CssClass="NormalTextBox" meta:resourcekey="chkPasswordAdminRole" Text="Password administrator" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Normal">
                                        <asp:CheckBox ID="chkSkypeAdminRole" runat="server" CssClass="NormalTextBox" meta:resourcekey="chkSkypeAdminRole" Text="Skype for Business administrator" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Normal">
                                        <asp:CheckBox ID="chkServiceAdminRole" runat="server" CssClass="NormalTextBox" meta:resourcekey="chkServiceAdminRole" Text="Service administrator" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Normal">
                                        <asp:CheckBox ID="chkSharePointAdminRole" runat="server" CssClass="NormalTextBox" meta:resourcekey="chkSharePointAdminRole" Text="SharePoint administrator" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Normal">
                                        <asp:CheckBox ID="chkUserManagementAdminRole" runat="server" CssClass="NormalTextBox" meta:resourcekey="chkUserManagementAdminRole" Text="User management administrator" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
