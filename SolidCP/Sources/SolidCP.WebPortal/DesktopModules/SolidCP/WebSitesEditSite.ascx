<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebSitesEditSite.ascx.cs"
    Inherits="SolidCP.Portal.WebSitesEditSite" %>
<%@ Import Namespace="SolidCP.Portal" %>
<%@ Register Src="WebSitesExtensionsControl.ascx" TagName="WebSitesExtensionsControl" TagPrefix="uc6" %>
<%@ Register Src="WebSitesCustomErrorsControl.ascx" TagName="WebSitesCustomErrorsControl" TagPrefix="uc4" %>
<%@ Register Src="WebSitesMimeTypesControl.ascx" TagName="WebSitesMimeTypesControl" TagPrefix="uc5" %>
<%@ Register Src="WebSitesHomeFolderControl.ascx" TagName="WebSitesHomeFolderControl" TagPrefix="uc1" %>
<%@ Register Src="WebSitesCustomHeadersControl.ascx" TagName="WebSitesCustomHeadersControl" TagPrefix="uc6" %>
<%@ Register Src="WebSitesSecuredFoldersControl.ascx" TagName="WebSitesSecuredFoldersControl" TagPrefix="scp" %>
<%@ Register Src="WebSitesHeliconApeControl.ascx" TagName="WebSitesHeliconApeControl" TagPrefix="scp" %>
<%@ Register Src="WebSitesHeliconZooControl.ascx" TagName="WebSitesHeliconZooControl" TagPrefix="scp" %>
<%@ Register Src="UserControls/PasswordControl.ascx" TagName="PasswordControl" TagPrefix="scp" %>
<%@ Register Src="UserControls/UsernameControl.ascx" TagName="UsernameControl" TagPrefix="scp" %>
<%@ Register Src="UserControls/EnableAsyncTasksSupport.ascx" TagName="EnableAsyncTasksSupport" TagPrefix="scp" %>
<%@ Register Src="UserControls/SimpleMessageBox.ascx" TagName="SimpleMessageBox" TagPrefix="scp" %>
<%@ Register Src="UserControls/PopupHeader.ascx" TagName="PopupHeader" TagPrefix="scp" %>
<%@ Register TagPrefix="scp" Namespace="SolidCP.Portal" %>
<%@ Register Src="WebsitesSSL.ascx" TagName="WebsitesSSL" TagPrefix="uc2" %>
<%@ Register Src="UserControls/ItemButtonPanel.ascx" TagName="ItemButtonPanel" TagPrefix="scp" %>

<style type="text/css">
    .style1 {
        width: 51px;
    }
</style>
<scp:EnableAsyncTasksSupport ID="asyncTasks" runat="server" />
<script type="text/javascript">

    function confirmationSITE() {
        if (!confirm("Are you sure you want to delete Web site?")) return false; else ShowProgressDialog('Deleting Web site...');
    }

    function confirmationFPSE() {
        if (!confirm("Are you sure you want to delete Frontpage account?")) return false; else ShowProgressDialog('Uninstalling Frontpage...');
    }

    function confirmationWMSVC() {
        if (!confirm("Are you sure you want to disable Remote Management?")) return false; else ShowProgressDialog('Disabling Remote Management...');
    }

    function confirmationWebDeployPublishing() {
        if (!confirm("Are you sure you want to disable Web Publishing?")) return false; else ShowProgressDialog('Disabling Web Publishing...');
    }
</script>
<asp:Panel ID="WDeployBuildPublishingProfileWizardPanel" runat="server" CssClass="PopupContainer" DefaultButton="PubProfileWizardOkButton" Style="display: none;">
    <div class="widget">
        <div class="widget-header clearfix">
            <h3><i class="fa fa-list"></i>
                <scp:PopupHeader runat="server" meta:resourcekey="WDeployBuildPublishingProfileWizard" />
            </h3>
        </div>
        <div class="widget-content Popup">
            <asp:UpdatePanel runat="server" ID="WDeployPubProfilePanel" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="MyDatabaseList" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <div class="panel-body form-horizontal">
                        <asp:PlaceHolder runat="server" ID="ChooseDatabasePanel">
                            <fieldset>
                                <legend>
                                    <asp:Localize ID="Localize1" runat="server" Text="Database Name" /></legend>
                                <div class="FormFieldDescription">
                                    <asp:Localize ID="Localize2" runat="server">Please choose database name...</asp:Localize>
                                </div>
                                <div class="FormField">
                                    <asp:DropDownList ID="MyDatabaseList" runat="server" DataTextField="Name" DataValueField="Id"
                                        AutoPostBack="true" Width="100%" OnSelectedIndexChanged="MyDatabaseList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </fieldset>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="ChooseDatabaseUserPanel">
                            <fieldset>
                                <legend>
                                    <asp:Localize ID="Localize3" runat="server" Text="Database User" /></legend>
                                <div class="FormFieldDescription">
                                    <asp:Localize ID="Localize4" runat="server">Please choose database user name...</asp:Localize>
                                </div>
                                <div class="FormField">
                                    <asp:DropDownList ID="MyDatabaseUserList" runat="server" DataTextField="Name" DataValueField="Id" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </fieldset>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="ChooseFtpAccountPanel">
                            <fieldset>
                                <legend>
                                    <asp:Localize ID="Localize5" runat="server" Text="FTP Account" /></legend>
                                <div class="FormFieldDescription">
                                    <asp:Localize ID="Localize6" runat="server">Please choose FTP account...</asp:Localize>
                                </div>
                                <div class="FormField">
                                    <asp:DropDownList ID="MyFtpAccountList" runat="server" DataTextField="Name" DataValueField="Id" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </fieldset>
                        </asp:PlaceHolder>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="popup-buttons text-right">
            <CPCC:StyleButton ID="PubProfileWizardCancelButton" CssClass="btn btn-warning" runat="server" ValidationGroup="WDeployBuildPublishingProfileWizard" CausesValidation="False">
               
<i class="fa fa-times">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="PubProfileWizardCancelButtonText" />
            </CPCC:StyleButton>
            &nbsp;
           
            <CPCC:StyleButton ID="PubProfileWizardOkButton" CssClass="btn btn-success" runat="server" OnClick="PubProfileWizardOkButton_Click" ValidationGroup="WDeployBuildPublishingProfileWizard">
               
<i class="fa fa-check">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="PubProfileWizardOkButtonText" />
            </CPCC:StyleButton>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="WDeployRebuildPublishingProfileWizardModal" runat="server"
    TargetControlID="WDeployRebuildPubProfileLinkButton" PopupControlID="WDeployBuildPublishingProfileWizardPanel"
    BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="PubProfileWizardCancelButton" />
<div class="panel-body form-horizontal">
    <scp:SimpleMessageBox ID="messageBox" runat="server" EnableViewState="false" />
    <div class="row">
        <div class="col-md-8">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:HyperLink ID="lnkSiteName" runat="server" NavigateUrl="#" CssClass="panel-title" Target="_blank">domain.com</asp:HyperLink>
                        </div>
                        <div class="col-md-4">
                            <asp:Panel ID="sharedIP" runat="server">
                                <asp:Localize ID="locSharedIPAddress" runat="server" meta:resourcekey="locSharedIPAddress" Text="IP address: Shared" />
                                <asp:Label ID="lblSharedIP" runat="server" />
                                &nbsp;&nbsp;&nbsp;
                               
                               

                                <CPCC:StyleButton ID="cmdSwitchToDedicatedIP" meta:resourcekey="cmdSwitchToDedicatedIP" runat="server" Text="Switch to dedicated IP" OnClick="cmdSwitchToDedicatedIP_Click"></CPCC:StyleButton>
                            </asp:Panel>
                            <asp:Panel ID="dedicatedIP" runat="server">
                                <asp:Localize ID="locDedicatedIPAddress" runat="server" meta:resourcekey="locDedicatedIPAddress" Text="IP address:" />
                                <asp:Literal ID="litIPAddress" runat="server"></asp:Literal>
                                &nbsp;&nbsp;&nbsp;
                               
                               

                                <CPCC:StyleButton ID="cmdSwitchToSharedIP" meta:resourcekey="cmdSwitchToSharedIP" runat="server" Text="Switch to shared IP" OnClick="cmdSwitchToSharedIP_Click"></CPCC:StyleButton>
                            </asp:Panel>
                            <asp:Panel ID="switchToDedicatedIP" runat="server" Visible="false">
                                <asp:Localize ID="locSelectIPAddress" runat="server" meta:resourcekey="locSelectIPAddress" Text="Select IP address:" />
                                <asp:DropDownList ID="ddlIpAddresses" runat="server" CssClass="NormalTextBox"></asp:DropDownList>
                                &nbsp;
                               
                               

                                <CPCC:StyleButton ID="cmdApplyDedicatedIP" meta:resourcekey="cmdApplyDedicatedIP" runat="server" Text="Apply" OnClick="cmdApplyDedicatedIP_Click"></CPCC:StyleButton>
                                &nbsp;
                               
                               

                                <CPCC:StyleButton ID="cmdCancelDedicatedIP" meta:resourcekey="cmdCancelDedicatedIP" runat="server" Text="Cancel" OnClick="cmdCancelDedicatedIP_Click"></CPCC:StyleButton>
                            </asp:Panel>
                        </div>
                        <div class="col-md-4 text-right">

                            <CPCC:StyleButton ID="btnAddPointer" runat="server" CssClass="btn btn-primary" OnClick="btnAddPointer_Click">
                               
<i class="fa fa-plus">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="btnAddPointer" />
                            </CPCC:StyleButton>
                        </div>

                    </div>


                </div>

                <asp:GridView ID="gvPointers" runat="server" EnableViewState="True" AutoGenerateColumns="false"
                    ShowHeader="false" CssSelectorClass="NormalGridView" EmptyDataText="gvPointers"
                    DataKeyNames="DomainID" OnRowDeleting="gvPointers_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="gvPointersName">
                            <ItemStyle Wrap="false" Width="100%"></ItemStyle>
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkPointer" runat="server" NavigateUrl='<%# "http://" + (string)Eval("DomainName") %>'
                                    Target="_blank"><%# Eval("DomainName") %></asp:HyperLink>

                                <CPCC:StyleButton runat="server" ID="cmdDeletePointer" CommandName='delete' CommandArgument='<%# Eval("DomainId") %>' Visible='<%# !(bool)Eval("IsInstantAlias") %>' CssClass="btn btn-default pull-right btn-xs" OnClientClick="return confirm('Remove pointer?');">
                                   
<i class="fa fa-trash">&nbsp;</i>&nbsp;Remove&nbsp;
                               
                                </CPCC:StyleButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-md-4">
            <div class="alert alert-info">
                <table cellpadding="7" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblWebsiteStatus" runat="server" meta:resourcekey="lblWebsiteStatus" Text="Website Status"></asp:Label>
                        </td>
                        <td class="MediumBold">
                            <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                        </td>
                        <td class="text-right margin-bottom-xs">
                            <CPCC:StyleButton ID="cmdStart" CssClass="btn btn-sm btn-success btn-circle" runat="server" CommandName="Started" OnClick="cmdChangeState_Click">
                                &nbsp;<i class="fa fa-play"></i>&nbsp;
                           
                            </CPCC:StyleButton>
                            <CPCC:StyleButton ID="cmdPause" CssClass="btn btn-sm btn-warning btn-circle" runat="server" CommandName="Paused" OnClick="cmdChangeState_Click">
                                &nbsp;<i class="fa fa-sm fa-pause"></i>&nbsp;
                           
                            </CPCC:StyleButton>
                            <CPCC:StyleButton ID="cmdContinue" CssClass="btn btn-sm btn-success btn-circle" runat="server" CommandName="Continuing" OnClick="cmdChangeState_Click">
                                &nbsp;<i class="fa fa-sm fa-forward"></i>&nbsp;
                           
                            </CPCC:StyleButton>
                            <CPCC:StyleButton ID="cmdStop" CssClass="btn btn-sm btn-danger btn-circle" runat="server" CommandName="Stopped" OnClick="cmdChangeState_Click">
                                &nbsp;<i class="fa fa-sm fa-stop"></i>&nbsp;
                           
                            </CPCC:StyleButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="padding-top-sm">
                            <asp:Label ID="lblAppPoolStatus" runat="server" meta:resourcekey="lblAppPoolStatus" Text="App Pool Status"></asp:Label>
                        </td>
                        <td class="MediumBold padding-top-sm">
                            <asp:Literal ID="litAppPoolStatus" runat="server"></asp:Literal>
                        </td>
                        <td class="padding-top-sm">
                            <asp:Panel runat="server" ID="AppPoolRestartPanel" CssClass="pull-right">
                                <CPCC:StyleButton ID="cmdAppPoolStart" CssClass="btn btn-sm btn-success btn-circle" runat="server" CommandName="Started" OnClick="cmdAppPoolChangeState_Click">
                                    &nbsp;<i class="fa fa-play"></i>&nbsp;
                               
                                </CPCC:StyleButton>
                                <CPCC:StyleButton ID="cmdAppPoolRecycle" CssClass="btn btn-sm btn-warning btn-circle" runat="server" CommandName="Recycle" OnClick="cmdAppPoolChangeState_Click">
                                    &nbsp;<i class="fa fa-refresh"></i>&nbsp;
                               
                                </CPCC:StyleButton>
                                <CPCC:StyleButton ID="cmdAppPoolStop" CssClass="btn btn-sm btn-danger btn-circle" runat="server" CommandName="Stopped" OnClick="cmdAppPoolChangeState_Click">
                                    &nbsp;<i class="fa fa-sm fa-stop"></i>&nbsp;
                               
                                </CPCC:StyleButton>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <ul class="nav nav-tabs">
        <asp:DataList ID="dlTabs" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="dlTabs_SelectedIndexChanged"
            RepeatLayout="Flow" DataKeyField="ViewId">
            <ItemStyle Wrap="False" />
            <ItemTemplate>
                <li role="presentation">
                    <asp:LinkButton ID="cmdSelectTab" runat="server" CommandName="select" CssClass="Tab">
                                    <%# Eval("Name") %>
                    </asp:LinkButton>
                </li>
            </ItemTemplate>
            <SelectedItemStyle Wrap="False" />
            <SelectedItemTemplate>
                <li role="presentation" class="active">
                    <asp:LinkButton ID="cmdSelectTab" runat="server" CommandName="select" CssClass="ActiveTab">
                                    <%# Eval("Name") %>
                    </asp:LinkButton>
                </li>
            </SelectedItemTemplate>

        </asp:DataList>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.nav-tabs li').unwrap();
            $('.nav-tabs li').unwrap();
        });
    </script>
    <div class="panel panel-default tab-content">

        <div class="panel-body form-horizontal">
            <asp:MultiView ID="tabs" runat="server" ActiveViewIndex="0">
                <asp:View ID="tabHomeFolder" runat="server">
                    <uc1:WebSitesHomeFolderControl ID="webSitesHomeFolderControl" runat="server" />
                </asp:View>
                <asp:View ID="tabVirtualDirs" runat="server">
                    <div class="row">
                        <div id="VirtualDirectoriesCol" runat="server" class="col-sm-12 col-md-6">
                            <div class="container">
                                <h3 class="text-center ">
                                    <label><asp:Localize ID="HeaderVirtualDirectories" runat="server" meta:resourcekey="HeaderVirtualDirectories"></asp:Localize></label>
                                </h3>
                                <p>
                                    <asp:Localize ID="VirtualDirectoriesInfo" runat="server" meta:resourcekey="VirtualDirectoriesInfo"></asp:Localize>
                                </p>
                                <div class="row">
                                    <CPCC:StyleButton ID="btnAddVirtualDirectory" runat="server"
                                        CssClass="btn btn-primary btn-block" CausesValidation="false" OnClick="btnAddVirtualDirectory_Click">
                                            <i class="fa fa-folder-open-o">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="btnAddVirtualDirectory" />
                                    </CPCC:StyleButton>

                                </div>
                                <hr />
                                <div class="grid">
                                    <asp:GridView ID="gvVirtualDirectories" runat="server" EnableViewState="True" AutoGenerateColumns="false"
                                        ShowHeader="true" HeaderStyle-CssClass="header" CssSelectorClass="NormalGridView" CssClass="table table-hover table-striped" EmptyDataText="gvVirtualDirectories">
                                        <Columns>
                                            <asp:TemplateField HeaderText="gvVirtualDirectoriesName">
                                                <ItemStyle Width="35%" CssClass="NormalBold"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkEditVDir" runat="server" NavigateUrl='<%# EditUrl("ItemID", PanelRequest.ItemID.ToString(), "edit_vdir", "VirtDir=" + Eval("Name"), "SpaceID=" + PanelSecurity.PackageId) %>'>
									        <%# Eval("Name") %>
                                                </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ContentPath" HeaderText="gvVirtualDirectoriesPath">
                                                <ItemStyle Width="65%" />
                                            </asp:BoundField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 hidden-sm-down"></div>
                        <div id="VirtualApplicationsCol" runat="server" class="col-sm-12 col-md-6">
                            <div class="container">
                                <h3 class="text-center ">
                                    <label>

                                        <asp:Localize ID="HeaderVirtualApplications" runat="server" meta:resourcekey="HeaderVirtualApplications"></asp:Localize></label>
                                </h3>
                                <p>
                                    <asp:Localize ID="VirtualApplicationsInfo" runat="server" meta:resourcekey="VirtualAppInfo"></asp:Localize>
                                </p>

                                <div class="row">
                                    <CPCC:StyleButton ID="btnAddAppVirtualDirectory" runat="server" CssClass="btn btn-primary btn-block" CausesValidation="false" OnClick="btnAddAppVirtualDirectory_Click">
                                       
<i class="fa fa-code-fork">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="btnAddAppVirtualDirectory" />
                                    </CPCC:StyleButton>
                                </div>
                                <hr />
                                <div class="grid">
                                    <asp:GridView ID="gvAppVirtualDirectories" runat="server" EnableViewState="True" AutoGenerateColumns="false"
                                        ShowHeader="true" CssSelectorClass="NormalGridView" CssClass="table table-hover table-striped" EmptyDataText="gvAppVirtualDirectories">
                                        <Columns>
                                            <asp:TemplateField HeaderText="gvAppVirtualDirectoriesName">
                                                <ItemStyle Width="35%" CssClass="NormalBold"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkEditVDir" runat="server" NavigateUrl='<%# EditUrl("ItemID", PanelRequest.ItemID.ToString(), "edit_vapps", "VirtDir=" + Eval("Name"), "SpaceID=" + PanelSecurity.PackageId) %>'>
									        <%# Eval("Name") %>
                                                </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ContentPath" HeaderText="gvAppVirtualDirectoriesPath">
                                                <ItemStyle Width="65%" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="tabSecuredFolders" runat="server">
                    <scp:WebSitesSecuredFoldersControl ID="webSitesSecuredFoldersControl" runat="server" />
                </asp:View>
                <asp:View ID="tabHeliconApe" runat="server">
                    <scp:WebSitesHeliconApeControl ID="webSitesHeliconApeControl" runat="server" />
                </asp:View>
                <asp:View ID="tabHeliconZoo" runat="server">
                    <scp:WebSitesHeliconZooControl ID="webSitesHeliconZooControl" runat="server" />
                </asp:View>
                <asp:View ID="tabFrontPage" runat="server">
                    <asp:Panel ID="pnlFrontPage" runat="server" Style="padding: 20;">
                        <table id="tblSharePoint" runat="server">
                            <tr>
                                <td class="NormalBold">
                                    <asp:Label ID="lblSharePoint" runat="server" meta:resourcekey="lblSharePoint" Text="This web site has SharePoint Service installed and thus can't"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Literal ID="litFrontPageUnavailable" runat="server"></asp:Literal>
                        <table id="tblFrontPage" cellspacing="0" cellpadding="2" width="100%" runat="server">
                            <tr>
                                <td class="SubHead" style="width: 150px;" height="30">
                                    <asp:Label ID="lblFPStatus" runat="server" meta:resourcekey="lblFPStatus" Text="FrontPage status:"></asp:Label>
                                </td>
                                <td class="NormalBold">
                                    <asp:Literal ID="litFrontPageStatus" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead">
                                    <asp:Label ID="lblFPAccount" runat="server" meta:resourcekey="lblFPAccount" Text="FrontPage User Account:"></asp:Label>
                                </td>
                                <td class="Normal">
                                    <scp:UsernameControl ID="frontPageUsername" runat="server" ValidationGroup="FrontPage" />
                                </td>
                            </tr>
                            <tr>
                                <td class="SubHead" valign="top">
                                    <asp:Label ID="lblFPPassword" runat="server" meta:resourcekey="lblFPPassword" Text="Password:"></asp:Label>
                                </td>
                                <td>
                                    <scp:PasswordControl ID="frontPagePassword" runat="server" ValidationGroup="FrontPage" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="Normal">
                                    <CPCC:StyleButton ID="btnUninstallFrontPage" CssClass="btn btn-danger" runat="server" CausesValidation="false" OnClick="btnUninstallFrontPage_Click" OnClientClick="confirmationFPSE()">
                                       
<i class="fa fa-trash-o">&nbsp;</i>&nbsp;<asp:Label runat="server" meta:resourcekey="btnUninstallFrontPageText" />
                                    </CPCC:StyleButton>
                                    &nbsp;
                                           
                                    <CPCC:StyleButton ID="btnChangeFrontPagePassword" CssClass="btn btn-warning" runat="server" ValidationGroup="FrontPage" OnClick="btnChangeFrontPagePassword_Click">
                                               
<i class="fa fa-key">&nbsp;</i>&nbsp;<asp:Label runat="server" meta:resourcekey="btnChangeFrontPagePasswordText" />
                                            </CPCC:StyleButton>
                                    &nbsp;
                                           
                                    <CPCC:StyleButton ID="btnInstallFrontPage" CssClass="btn btn-success" runat="server" ValidationGroup="FrontPage" OnClick="btnInstallFrontPage_Click" OnClientClick="ShowProgressDialog('Installing Frontpage...');">
                                               
<i class="fa fa-check">&nbsp;</i>&nbsp;<asp:Label runat="server" meta:resourcekey="btnInstallFrontPageText" />
                                            </CPCC:StyleButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h4 class="text-center ">
                                        <label><asp:Localize ID="Localize14" runat="server" meta:resourcekey="HeaderVirtualDirectoriesFP"></asp:Localize></label>
                                    </h4>
                                    <p>
                                        <asp:Localize ID="VirtualDirectoriesFPInfo" runat="server" meta:resourcekey="VirtualDirectoriesFPInfo"></asp:Localize>
                                    </p>
                                    <h4>
                                        <label><asp:Localize ID="HeaderVirtualApplicationsFP" runat="server" meta:resourcekey="HeaderVirtualApplicationsFP"></asp:Localize></label>
                                    </h4>
                                    <p>
                                        <asp:Localize ID="VirtualFPAppInfo" runat="server" meta:resourcekey="VirtualFPAppInfo"></asp:Localize>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:View>
                <asp:View ID="tabExtensions" runat="server">
                    <uc6:WebSitesExtensionsControl ID="webSitesExtensionsControl" runat="server" />
                </asp:View>
                <asp:View ID="tabErrors" runat="server">
                    <uc4:WebSitesCustomErrorsControl ID="webSitesCustomErrorsControl" runat="server" />
                </asp:View>
                <asp:View ID="tabHeaders" runat="server">
                    <uc6:WebSitesCustomHeadersControl ID="webSitesCustomHeadersControl" runat="server" />
                </asp:View>
                <asp:View ID="tabWebDeployPublishing" runat="server">
                    <div style="padding: 20;">
                        <asp:PlaceHolder runat="server" ID="PanelWDeploySitePublishingDisabled" Visible="false">
                            <div class="NormalBold">
                                <asp:Localize runat="server" meta:resourcekey="WDeploySitePublishingDisabled" />
                            </div>
                            <br />
                            <div class="Normal">
                                <asp:Localize runat="server" meta:resourcekey="WDeploySitePublishingEnablementHint" />
                            </div>
                            <br />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="PanelWDeployManagePublishingProfile" Visible="false">
                            <div class="NormalBold">
                                <asp:Localize runat="server" meta:resourcekey="WDeploySitePublishingEnabled" />
                            </div>
                            <br />
                            <div class="Normal">
                                <asp:Localize runat="server" meta:resourcekey="WDeployPublishingProfileUsageNotes" />
                            </div>
                            <br />
                            <p>
                                <CPCC:StyleButton runat="server" ID="WDeployDownloadPubProfileLink" CssClass="btn btn-success" Text="Download Publishing Profile for this web site"
                                    CommandName="DownloadProfile" OnCommand="WDeployDownloadPubProfileLink_Command" />
                                &nbsp;&nbsp;<CPCC:StyleButton
                                    runat="server" ID="WDeployRebuildPubProfileLinkButton" Text="Re-build Publishing Profile for this web site" />
                            </p>
                            <br />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="PanelWDeployPublishingCredentials" Visible="false">
                            <table cellpadding="4" border="0">
                                <tr>
                                    <td class="Normal">
                                        <asp:Localize runat="server" meta:resourcekey="WDeployPublishingAccountLocalize" />
                                    </td>
                                    <td class="NormalBold">
                                        <asp:TextBox runat="server" ID="WDeployPublishingAccountTextBox" CssClass="NormalTextBox"
                                            ValidationGroup="WDeployPublishingGroup" MaxLength="20" />
                                        <asp:Literal runat="server" ID="WDeployPublishingAccountLiteral" Visible="false" />
                                        <asp:RequiredFieldValidator ID="WDeployPublishingAccountRequiredFieldValidator" runat="server"
                                            ErrorMessage="*" ValidationGroup="WDeployPublishingGroup" ControlToValidate="WDeployPublishingAccountTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Normal">
                                        <asp:Localize runat="server" meta:resourcekey="WDeployPublishingPasswordLocalize" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="WDeployPublishingPasswordTextBox" TextMode="Password"
                                            CssClass="NormalTextBox" ValidationGroup="WDeployPublishingGroup" />
                                        <asp:RequiredFieldValidator ID="WDeployPublishingPasswordRequiredFieldValidator"
                                            runat="server" ErrorMessage="*" ValidationGroup="WDeployPublishingGroup" ControlToValidate="WDeployPublishingPasswordTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Normal">
                                        <asp:Localize runat="server" meta:resourcekey="WDeployPublishingConfirmPasswordLocalize" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="WDeployPublishingConfirmPasswordTextBox" TextMode="Password"
                                            CssClass="NormalTextBox" ValidationGroup="WDeployPublishingGroup" />
                                        <asp:RequiredFieldValidator ID="WDeployPublishingConfirmPasswordRequiredFieldValidator"
                                            runat="server" ErrorMessage="*" ValidationGroup="WDeployPublishingGroup" ControlToValidate="WDeployPublishingConfirmPasswordTextBox" />
                                        <asp:CompareValidator ID="WDeployPublishingConfirmPasswordTextBoxCompareValidator"
                                            runat="server" meta:resourcekey="cvPasswordComparer" ControlToValidate="WDeployPublishingConfirmPasswordTextBox"
                                            ControlToCompare="WDeployPublishingPasswordTextBox" Display="Dynamic" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div>
                                <CPCC:StyleButton ID="WDeployDisablePublishingButton" CssClass="btn btn-danger" runat="server" OnClick="WDeployDisablePublishingButton_Click" OnClientClick="return confirmationWebDeployPublishing();" ValidationGroup="WDeployPublishingGroup" CausesValidation="false">
                                   
<i class="fa fa-times">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="WDeployDisablePublishingButtonText" />
                                </CPCC:StyleButton>
                                &nbsp;
                                       
                                <CPCC:StyleButton ID="WDeployChangePublishingPasswButton" CssClass="btn btn-warning" runat="server" OnClick="WDeployChangePublishingPasswButton_Click" ValidationGroup="WDeployPublishingGroup">
                                           
<i class="fa fa-key">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="WDeployChangePublishingPasswButtonText" />
                                        </CPCC:StyleButton>
                                &nbsp;
									
                                <CPCC:StyleButton ID="WDeployEnabePublishingButton" CssClass="btn btn-success" runat="server" OnClick="WDeployEnabePublishingButton_Click" ValidationGroup="WDeployPublishingGroup">
                                           
<i class="fa fa-check">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="WDeployEnabePublishingButtonText" />
                                        </CPCC:StyleButton>
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="PanelWDeployNotInstalled" Visible="false">Web Deploy
									Remote Agent is not installed... </asp:PlaceHolder>
                    </div>
                </asp:View>
                <asp:View ID="tabMimes" runat="server">
                    <uc5:WebSitesMimeTypesControl ID="webSitesMimeTypesControl" runat="server" />
                </asp:View>
                <asp:View ID="tabCF" runat="server">
                    <asp:Literal ID="litCFUnavailable" runat="server"></asp:Literal>
                    <br />
                    <br />
                    <table id="tableCF" runat="server">
                        <tr id="rowCF" runat="server">
                            <td class="style1">
                                <asp:CheckBox ID="chkCfExt" runat="server" Text="" meta:resourcekey="chkEnabled" />
                            </td>
                            <td class="Normal">
                                <asp:Label ID="lblCF" runat="server" meta:resourcekey="lblCF" Text="Enable ColdFusion Extensions."></asp:Label>
                            </td>
                        </tr>
                        <tr id="rowVirtDir" runat="server">
                            <td class="style1">
                                <asp:CheckBox ID="chkVirtDir" runat="server" Text="" meta:resourcekey="chkEnabled" />
                            </td>
                            <td class="Normal">
                                <asp:Label ID="lblVirtDir" runat="server" meta:resourcekey="lblVirtDir" Text="Create Virtual Directories for scripts and Flash remoting."></asp:Label><br />
                                <h4 class="text-center ">
                                    <label><asp:Localize ID="Localize13" runat="server" meta:resourcekey="HeaderVirtualDirectoriesCF"></asp:Localize></label>
                                </h4>
                                <p>
                                    <asp:Localize ID="VirtualDirectoriesCFInfo" runat="server" meta:resourcekey="VirtualDirectoriesCFInfo"></asp:Localize>
                                </p>
                                <h4>
                                    <label><asp:Localize ID="HeaderVirtualApplicationsCF" runat="server" meta:resourcekey="HeaderVirtualApplicationsCF"></asp:Localize></label>
                                </h4>
                                <p>
                                    <asp:Localize ID="VirtualCFAppInfo" runat="server" meta:resourcekey="VirtualCFAppInfo"></asp:Localize>
                                </p>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="tabWebManagement" runat="server">
                    <div style="padding: 20px;">
                        <asp:PlaceHolder runat="server" ID="pnlWmSvcSiteDisabled" Visible="false">
                            <div class="NormalBold">
                                <asp:Localize runat="server" meta:resourcekey="lclWmSvcSiteDisabled" />
                            </div>
                            <br />
                            <div class="Normal">
                                <asp:Localize runat="server" meta:resourcekey="lclEnablementHint" />
                            </div>
                            <br />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="pnlWmSvcSiteEnabled" Visible="false">
                            <div class="NormalBold">
                                <asp:Localize runat="server" meta:resourcekey="lclWmSvcSiteEnabled" />
                            </div>
                            <br />
                            <div class="Normal">
                                <asp:Localize runat="server" ID="lclWmSvcConnectionHint" meta:resourcekey="lclWmSvcConnectionHint" />
                            </div>
                            <br />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="pnlWmcSvcManagement">
                            <table cellpadding="4" border="0">
                                <tr>
                                    <td class="Normal">
                                        <asp:Localize runat="server" meta:resourcekey="lclWmSvcAccountName" />
                                    </td>
                                    <td class="NormalBold">
                                        <asp:TextBox runat="server" ID="txtWmSvcAccountName" CssClass="NormalTextBox" ValidationGroup="WmSvcGroup"
                                            MaxLength="20" />
                                        <asp:Literal runat="server" ID="litWmSvcAccountName" Visible="false" />
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ValidationGroup="WmSvcGroup"
                                            ControlToValidate="txtWmSvcAccountName" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Normal">
                                        <asp:Localize runat="server" meta:resourcekey="lclWmSvcAccountPassword" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtWmSvcAccountPassword" TextMode="Password" CssClass="NormalTextBox"
                                            ValidationGroup="WmSvcGroup" />
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ValidationGroup="WmSvcGroup"
                                            ControlToValidate="txtWmSvcAccountPassword" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Normal">
                                        <asp:Localize runat="server" meta:resourcekey="lclWmSvcAccountPasswordC" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtWmSvcAccountPasswordC" TextMode="Password" CssClass="NormalTextBox"
                                            ValidationGroup="WmSvcGroup" />
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ValidationGroup="WmSvcGroup"
                                            ControlToValidate="txtWmSvcAccountPasswordC" />
                                        <asp:CompareValidator runat="server" meta:resourcekey="cvPasswordComparer" ControlToValidate="txtWmSvcAccountPasswordC"
                                            ControlToCompare="txtWmSvcAccountPassword" Display="Dynamic" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div>
                                <CPCC:StyleButton ID="btnWmSvcSiteDisable" CssClass="btn btn-danger" runat="server" OnClick="btnWmSvcSiteDisable_Click" OnClientClick="return confirmationWMSVC();" ValidationGroup="WmSvcGroup" CausesValidation="false">
                                   
<i class="fa fa-times">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="btnWmSvcSiteDisableText" />
                                </CPCC:StyleButton>
                                &nbsp;
                                       
                                <CPCC:StyleButton ID="btnWmSvcChangePassw" CssClass="btn btn-warning" runat="server" OnClick="btnWmSvcChangePassw_Click" ValidationGroup="WmSvcGroup">
                                           
<i class="fa fa-key">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="btnWmSvcChangePasswText" />
                                        </CPCC:StyleButton>
                                &nbsp;
                                       
                                <CPCC:StyleButton ID="btnWmSvcSiteEnable" CssClass="btn btn-success" runat="server" OnClick="btnWmSvcSiteEnable_Click" ValidationGroup="WmSvcGroup">
                                           
<i class="fa fa-check">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="btnWmSvcSiteEnableText" />
                                        </CPCC:StyleButton>
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="pnlNotInstalled" Visible="false">
                            <div class="NormalBold">
                                <asp:Localize runat="server" meta:resourcekey="lclWmSvcNotInstalled" />
                            </div>
                        </asp:PlaceHolder>
                    </div>
                </asp:View>
                <asp:View ID="SSL" runat="server">
                    <uc2:WebsitesSSL ID="WebsitesSSLControl" runat="server" />
                </asp:View>
            </asp:MultiView>
        </div>


    </div>


</div>
<div class="panel-footer">
    <div class="row">
        <div class="col-md-6">
            <CPCC:StyleButton ID="btnDelete" CssClass="btn btn-danger" runat="server" CausesValidation="false" OnClientClick="return confirm('Delete this web site?');" OnClick="btnDelete_Click">
               
<i class="fa fa-trash-o">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="btnDeleteText" />
            </CPCC:StyleButton>
            <asp:CheckBox ID="chkDeleteWebsiteDirectory" runat="server" Text="Delete Website Directory?" AutoPostBack="false" meta:resourcekey="chkDeleteWebsiteDirectory" />

        </div>
        <div class="col-md-6 text-right">
            <scp:ItemButtonPanel id="buttonPanel" runat="server" ValidationGroup="EditMailbox"
                OnSaveClick="btnSave_Click" OnSaveExitClick="btnSaveExit_Click" />
        </div>
    </div>
</div>
