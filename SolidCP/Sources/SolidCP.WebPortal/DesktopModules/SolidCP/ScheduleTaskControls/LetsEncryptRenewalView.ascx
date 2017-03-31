<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LetsEncryptRenewalView.ascx.cs" Inherits="SolidCP.Portal.ScheduleTaskControls.LetsEncryptRenewalView" %>

<table cellspacing="0" cellpadding="4" width="100%">
    <tr>
        <td class="SubHead" nowrap>
            <asp:Label ID="lblDayBefore" runat="server" meta:resourcekey="lblDayBefore" Text="Renew before expirydate (days):"></asp:Label>
        </td>
        <td class="Normal" width="100%">
            <asp:TextBox ID="txtDaysBefore" runat="server" Width="95%" CssClass="NormalTextBox" MaxLength="1000"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="SubHead" nowrap>
            <asp:Label ID="lblNotifyOwner" runat="server" meta:resourcekey="lblNotifyOwner" AssociatedControlID="cbNotifyOwner" Text="Notify certificate owner of renewal result:"></asp:Label>
        </td>
        <td>
            <asp:CheckBox runat="server" ID="cbNotifyOwner" /><br/>
        </td>
    </tr>
    <tr>
        <td class="SubHead" nowrap>
            <asp:Label ID="lblNotifyEmail" runat="server" meta:resourcekey="lblNotifyEmail" Text="Send admin summary of renewal results to:"></asp:Label>
        </td>
        <td class="Normal" width="100%">
            <asp:TextBox ID="txtNotifyEmail" runat="server" Width="95%" CssClass="NormalTextBox" MaxLength="1000"></asp:TextBox>
         </td>
    </tr>
</table>