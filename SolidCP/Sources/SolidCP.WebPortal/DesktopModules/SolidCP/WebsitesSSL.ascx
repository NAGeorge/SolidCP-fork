<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebsitesSSL.ascx.cs" Inherits="SolidCP.Portal.WebsitesSSL" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="UserControls/SimpleMessageBox.ascx" TagName="SimpleMessageBox" TagPrefix="scp" %>
<%@ Register Assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="System.Web.UI" TagPrefix="cc1" %>
<%@ Register Src="SkinControls/BootstrapDropDownList.ascx" TagName="BootstrapDropDownList" TagPrefix="bsddl" %>

<asp:UpdatePanel ID="MessageBoxUpdatePanel" runat="server" UpdateMode="Always">
	<contenttemplate>
		<scp:SimpleMessageBox id="messageBox" runat="server"></scp:SimpleMessageBox>
        <asp:Panel runat="server" CssClass="MessageBox Yellow" ID="pnlHostNamesNotValidated">
            <asp:Localize runat="server" meta:resourcekey="hostnamesNotValidated"/>
            <asp:Literal runat="server" ID="ltlHostNamesNotValidated"></asp:Literal>
        </asp:Panel>
	</contenttemplate>
</asp:UpdatePanel>
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
	<ajaxToolkit:TabPanel ID="tabInstalled" runat="server" Visible="false" Enabled="false" CssClass="nav nav-tabs">
		<ContentTemplate>
			<div class="Normal">
				<h2><asp:Localize runat="server" meta:resourcekey="headerInstalledCertificate"/></h2>
				<table>
					<tr>
						<td class="SubHead" style="width: 200px;">
							<asp:Localize runat="server" meta:resourcekey="sslDomain" /></td>
						<td class="Normal">
							<asp:Literal ID="lblInstalledDomain" runat="server" /></td>
					</tr>
                    <tr>
						<td class="SubHead" style="width: 200px;">
							<asp:Localize runat="server" meta:resourcekey="sslSAN" /></td>
						<td class="Normal">
							<asp:Literal ID="lblInstalledSAN" runat="server" /></td>
					</tr>
					<tr>
						<td class="SubHead">
							<asp:Localize runat="server" meta:resourcekey="sslExpiry" /></td>
						<td class="Normal">
							<asp:Label ID="lblInstalledExpiration" CssClass="Normal" runat="server" /></td>
					</tr>
					<tr>
						<td class="SubHead">
							<asp:Localize runat="server" meta:resourcekey="sslBitLength" /></td>
						<td class="Normal">
							<asp:Literal ID="lblInstalledBits" runat="server" /></td>
					</tr>
					<tr id="trOrganization" runat="server">
						<td class="SubHead">
							<asp:Localize runat="server" meta:resourcekey="sslOrganization" /></td>
						<td class="Normal">
							<asp:Literal ID="lblInstalledOrganization" runat="server" /></td>
					</tr>
					<tr id="trOrganizationUnit" runat="server">
						<td class="SubHead">
							<asp:Localize runat="server" meta:resourcekey="sslOrganizationUnit" /></td>
						<td class="Normal">
							<asp:Literal ID="lblInstalledOU" runat="server" /></td>
					</tr>
					<tr id="trCountry" runat="server">
						<td class="SubHead">
							<asp:Localize ID="Localize1" runat="server" meta:resourcekey="sslCountry" /></td>
						<td class="Normal">
							<asp:Literal ID="lblInstalledCountry" runat="server" /></td>
					</tr>
					<tr id="trState" runat="server">
						<td class="SubHead">
							<asp:Localize ID="Localize2" runat="server" meta:resourcekey="sslState" /></td>
						<td class="Normal">
							<asp:Literal ID="lblinstalledState" runat="server" /></td>
					</tr>
					<tr id="trCity" runat="server">
						<td class="SubHead">
							<asp:Localize runat="server" meta:resourcekey="sslCity" /></td>
						<td class="Normal">
							<asp:Literal ID="lblInstalledCity" runat="server" /></td>
					</tr>
				</table>
			</div>
			<br />
			<asp:Button ID="btnRenew" runat="server" UseSubmitBehavior="true" meta:resourcekey="btnRenew"
				CssClass="Button1" Text="Renew" OnClick="btnRenew_Click" />
			<asp:Button ID="btnExportModal" runat="server" meta:resourcekey="btnExportModal"
				CssClass="Button1" Text="Export" />
			<asp:Button ID="btnDelete" runat="server" Text="Delete" meta:resourcekey="btnDelete"
				CssClass="Button1" OnClick="btnDelete_Click" />
			<asp:Panel ID="pnlPFXPassword" CssClass="Popup" Style="display: none" runat="server">
				<table class="Popup-Header" cellpadding="0" cellspacing="0">
					<tr>
						<td class="Popup-HeaderLeft">&nbsp;</td>
						<td class="Popup-HeaderTitle"><asp:Localize runat="server" meta:resourcekey="headerPFXPassword" /></td>
						<td class="Popup-HeaderRight">&nbsp;</td>
					</tr>
				</table>
				<div class="Popup-Content">
					<div class="Popup-Body" style="padding-top: 5px; padding-bottom: 5px;">
						<div class="FormFieldDescription">
							<asp:Localize runat="server" meta:resourcekey="PfxPassword" /></div>
						<div class="FormField">
							<asp:TextBox ID="txtPFXPass" ValidationGroup="pfxExport" runat="server" TextMode="Password" />
							<asp:RequiredFieldValidator ID="valtxtPFXPass" runat="server" Display="Dynamic" ValidationGroup="pfxExport"
								ControlToValidate="txtPFXPass" meta:resourcekey="valtxtPFXPass" /></div>
						<div class="FormFieldDescription">
							<asp:Localize runat="server" meta:resourcekey="PfxPasswordConfirmation" /></div>
						<div class="FormField">
							<asp:TextBox ID="txtPFXPassConfirm" ValidationGroup="pfxExport" runat="server" TextMode="Password" />
							<asp:CompareValidator ID="valtxtPFXPassConfirm" runat="server" ValidationGroup="pfxExport" 
								ControlToCompare="txtPFXPass" ControlToValidate="txtPFXPassConfirm" meta:resourcekey="valtxtPFXPassConfirm" /></div>
					</div>
					<div class="FormFooter">
						<asp:Button ID="btnExport" meta:resourcekey="btnExport" ValidationGroup="pfxExport"
							runat="server" OnClick="btnExport_Click" CssClass="Button1" UseSubmitBehavior="false"
							Text="Export" />
						<asp:Button ID="btnPFXExportCancel" meta:resourcekey="btnPFXExportCancel" runat="server"
							Text="Cancel" CssClass="Button1" />
					</div>
				</div>
			</asp:Panel>
			<ajaxToolkit:ModalPopupExtender ID="modalPfxPass" runat="server" TargetControlID="btnExportModal"
				PopupControlID="pnlPFXPassword" OkControlID="btnExport" BackgroundCssClass="modalBackground"
				DropShadow="false" CancelControlID="btnPFXExportCancel" />
		</ContentTemplate>
	</ajaxToolkit:TabPanel>
	<ajaxToolkit:TabPanel ID="tabCSR" runat="server" meta:resourcekey="tabNewCertificate" CssClass="nav nav-tabs">
		<ContentTemplate>
			<asp:Panel ID="SSLNotInstalled" runat="server" Visible="true">
				<div class="Normal">
					<h2>
						<asp:Literal ID="SSLNotInstalledHeading" runat="server" meta:resourcekey="SSLNotInstalledHeading" /></h2>
					<p class="Normal">
						<asp:Literal ID="SSLNotInstalledDescription" runat="server" meta:resourcekey="SSLNotInstalledDescription" /></p>
				</div>
				<div class="form-group text-center">
					<asp:Button ID="btnShowpnlCSR" runat="server" meta:resourcekey="btnShowpnlCSR" CssClass="btn btn-primary"
						Text="Generate CSR" OnClick="btnShowpnlCSR_click" />
					<asp:Button ID="btnShowUpload" meta:resourcekey="btnShowUpload" CssClass="btn btn-primary"
						runat="server" OnClick="btnShowUpload_click" Text="Upload Certificate" />
				</div>
                <div class="form-group text-center">
                    <asp:Button ID="btnShowLetsEncrypt" runat="server" meta:resourcekey="btnShowLetsEncrypt" CssClass="btn btn-primary"
						Text="Install a free certificate from Let's Encrypt" OnClick="btnShowLetsEncrypt_OnClick" />
				</div>
			</asp:Panel>

            <asp:Panel ID="pnlLetsEncrypt" runat="server" Visible="false">
                <script type="text/javascript">
                    function checkTOS(source, args)
                    {                
                        var elem = document.getElementById('<%= ckbAgreeTOS.ClientID %>');
                        args.IsValid = elem.checked;
                    }
                </script>
    
                <div class="Normal">
				    <h2><asp:Literal ID="LetsEncryptHeading" runat="server" meta:resourcekey="LetsEncryptHeading" /></h2>
					<p class="Normal">
						<asp:Literal ID="LetsEncryptDescription" runat="server" meta:resourcekey="LetsEncryptDescription" />
                        </p>
                    <p class="Normal">
                        <asp:HyperLink runat="server" ID="lnkLetsEncryptTOS" meta:resourcekey="lnkLetsEncryptTOS" Target="_blank" NavigateUrl="https://letsencrypt.org/documents/LE-SA-v1.0.1-July-27-2015.pdf"></asp:HyperLink>
                        <asp:CheckBox runat="server" ID="ckbAgreeTOS" meta:resourcekey="ckbAgreeTOS" />
                        <asp:CustomValidator ID="AgreeTOSRequired" runat="server" Display="Dynamic" ErrorMessage="*" ClientValidationFunction="checkTOS" OnServerValidate="AgreeTOSRequired_OnServerValidate"></asp:CustomValidator>
					</p>
				</div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                <dir class="row">
                             <dir class="col-sm-4">
                                <div class="form-group">
                                    <CPCC:H5Label runat="server" for="ddlHostName" class="control-label"><asp:Localize ID="SelectHostName" runat="server" meta:resourcekey="SelectHostName" /></CPCC:H5Label>
                                    <asp:DropDownList ID="ddlHostName" Runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlHostName_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                            </dir>
                            <dir class="col-sm-4">
                                <div class="form-group">
                                    <CPCC:H5Label runat="server" for="ddlHostName" class="control-label"><asp:Localize ID="SelectHostNames" runat="server" meta:resourcekey="SelectHostNames" /></CPCC:H5Label>
                                    <asp:ListBox id="lbSANs" Runat="server" SelectionMode="Multiple" Height="100" CssClass="list-group"></asp:ListBox>
                                    </div>
                            </dir>
                        </dir>
                        </ContentTemplate>
                </asp:UpdatePanel>
				<div style="width: 300px; margin: 2px auto;">
   					<CPCC:StyleButton ID="btnLetsEncrypt" meta:resourcekey="btnLetsEncrypt" CssClass="btn btn-primary" runat="server" OnClick="btnLetsEncrypt_click" />
				</div>
			</asp:Panel>

			<asp:Panel ID="SSLImport" runat="server" Visible="false">
				<div>
					<h2>
						<asp:Localize ID="SSLImportHeading" runat="server" meta:resourcekey="SSLImportHeading" /></h2>
					<p class="Normal">
						<asp:Localize ID="SSLImportDescription" runat="server" meta:resourcekey="SSLImportDescription" /></p>
					<asp:Button ID="btnImport" meta:resourcekey="btnImport" CssClass="Button1" runat="server" OnClick="btnImport_click" />
			        <asp:Button ID="btnDeleteAll" runat="server" Text="Delete" meta:resourcekey="btnDelete"
				        CssClass="Button1" OnClick="btnDeleteAll_Click" />
				</div>
			</asp:Panel>
			<asp:Panel ID="pnlCSR" runat="server" Visible="false">
				<h2>
					<asp:Localize runat="server" meta:resourcekey="GenerateCSR" /></h2>
				<table style="width: 100%;">
	                <tr>
						<td class="SubHead">
							<asp:Localize ID="SelectCertType" runat="server" meta:resourcekey="SelectCertType" /></td>
		                <td class="NormalBold" ><asp:DropDownList id="ddlbSiteCertificate" GroupName="Content" Runat="server" Checked="True"></asp:DropDownList></td>
					<tr>
						<td class="SubHead">
							<asp:Localize ID="sslBitLength" runat="server" meta:resourcekey="sslBitLength" /></td>
						<td class="Normal">
							<asp:DropDownList ID="lstBits" runat="server">
								<asp:ListItem>1024</asp:ListItem>
								<asp:ListItem Selected="True">2048</asp:ListItem>
								<asp:ListItem>4096</asp:ListItem>
							</asp:DropDownList></td>
					</tr>
					<tr>
						<td class="SubHead">
							<asp:Localize ID="sslOrganization" runat="server" meta:resourcekey="sslOrganization" /></td>
						<td class="Normal">
							<asp:TextBox ID="txtCompany" runat="server" /><asp:RequiredFieldValidator ID="SSLCompanyReq" Display="Dynamic" ValidationGroup="SSL" runat="server"
								ControlToValidate="txtCompany" ErrorMessage="*" /></td>
					</tr>
					<tr>
						<td class="SubHead">
							<asp:Localize ID="sslOrganizationUnit" runat="server" meta:resourcekey="sslOrganizationUnit" /></td>
						<td class="Normal">
							<asp:TextBox ID="txtOU" runat="server" /></td>
					</tr>
					<tr>
						<td class="SubHead">
							<asp:Localize ID="sslCountry" runat="server" meta:resourcekey="sslCountry" /></td>
						<td class="Normal">
							<asp:dropdownlist runat="server" id="lstCountries" cssclass="NormalTextBox" AutoPostBack="true" 
								OnSelectedIndexChanged="lstCountries_SelectedIndexChanged" width="200px" /></td>
					</tr>
					<tr>
						<td class="SubHead">
							<asp:Localize ID="sslState" runat="server" meta:resourcekey="sslState" /></td>
						<td class="Normal">
							<asp:TextBox id="txtState" runat="server" CssClass="NormalTextBox" Width="200px"></asp:TextBox>
							<asp:DropDownList ID="ddlStates" Runat="server" DataTextField="Text" DataValueField="Value" CssClass="NormalTextBox"
								Width="200px" Visible="false" />
							<asp:RequiredFieldValidator ID="SSLSSLStateReq" ValidationGroup="SSL" runat="server"
								ControlToValidate="txtState" Display="Dynamic" ErrorMessage="*" /></td>
					</tr>
					<tr>
						<td class="SubHead">
							<asp:Localize ID="sslCity" runat="server" meta:resourcekey="sslCity" /></td>
						<td class="Normal">
							<asp:TextBox ID="txtCity" runat="server" />
							<asp:RequiredFieldValidator ID="SSLCityReq" ValidationGroup="SSL" runat="server"
								ControlToValidate="txtCity" ErrorMessage="*" /></td>
					</tr>
				</table>
				<br />
				<asp:Button ID="btnCSR" meta:resourcekey="btnCSR" runat="server" CssClass="Button1"
					Text="Generate CSR" ValidationGroup="SSL" OnClick="btnCSR_Click" />
				<asp:Button ID="btnRenCSR" meta:resourcekey="btnRenCSR" runat="server" CssClass="Button1"
					Text="Generate CSR" ValidationGroup="SSL" OnClick="btnRenCSR_Click" Visible="false" />
			</asp:Panel>
			<asp:Panel ID="pnlShowUpload" runat="server" Visible="false">
				<div class="FormBody">
					<div class="FormField">
						<asp:FileUpload ID="upPFX" runat="server"></asp:FileUpload></div>
					<div class="FormFieldDescription">
						<asp:Localize runat="server" meta:resourcekey="lblPFXInstallPassword" /></div>
					<div class="FormField">
						<asp:TextBox ID="txtPFXInstallPassword" runat="server" TextMode="Password" CssClass="NormalTextBox" />
						<asp:RequiredFieldValidator runat="server" ControlToValidate="txtPFXInstallPassword" 
							Display="Dynamic" ValidationGroup="InstallPfxGrp" ErrorMessage="*" /></div>
					<br />
					<asp:Button CssClass="Button1" ID="btnInstallPFX" runat="server" Text="Install" OnClick="btnInstallPFX_Click"
						meta:resourcekey="btnInstallPFX" ValidationGroup="InstallPfxGrp" />
				</div>
			</asp:Panel>
			<asp:Panel ID="pnlInstallCertificate" runat="server" Visible="false">
				<div class="Normal">
					<h2>
						<asp:Localize ID="InstallCSRHeading" runat="server" meta:resourcekey="InstallCSRHeading" /></h2>
					<p>
						<asp:Localize ID="InstallCSRDescription" runat="server" meta:resourcekey="InstallCSRDescription" /></p>
					<asp:Localize ID="sslCSR" runat="server" meta:resourcekey="sslCSR" />:<br />
					<asp:TextBox ID="txtCSR" runat="server" Style="text-align: left; font-family: Courier New"
						Rows="25" TextMode="MultiLine" ReadOnly="True" Columns="65" Wrap="false" onfocus="this.select();"></asp:TextBox>
					<br />
					<br />
					<asp:Button ID="btnRegenCSR" CssClass="Button1" runat="server" meta:resourcekey="btnRegenCSR"
						Text="Generate New CSR" OnClick="btnRegenCSR_Click" OnClientClick="return confirm('Are you Sure? This will delete the current request.');" />
					<br />
					<p>
						<asp:Localize ID="InstallCSRCertificate" runat="server" meta:resourcekey="InstallCSRCertificate" />
					</p>
					<asp:Localize ID="sslCertificate" runat="server" meta:resourcekey="sslCertificate" />:<br />
					<asp:TextBox ID="txtCertificate" runat="server" Rows="30" Columns="65" Wrap="false"
						Style="text-align: left; font-family: Courier New" TextMode="MultiLine" ReadOnly="False"></asp:TextBox>
					<br />
					<br />
					<asp:Button ID="btnInstallCertificate" meta:resourcekey="btnInstallCertificate" runat="server"
						CssClass="Button1" Text="Install" OnClick="btnInstallCertificate_Click" />
					<asp:Button ID="btnCancelRequest" runat="server" OnClientClick="return confirm('Are you Sure? This will delete the current request.');"
						CssClass="Button1" Text="Cancel request" OnClick="btnCancelRequest_Click" />
				</div>
			</asp:Panel>
		</ContentTemplate>
	</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>