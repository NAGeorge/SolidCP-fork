<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RDSCreateCollection.ascx.cs" Inherits="SolidCP.Portal.RDS.RDSCreateCollection" %>
<%@ Register Src="../UserControls/SimpleMessageBox.ascx" TagName="SimpleMessageBox" TagPrefix="scp" %>
<%@ Register Src="../UserControls/EnableAsyncTasksSupport.ascx" TagName="EnableAsyncTasksSupport" TagPrefix="scp" %>
<%@ Register Src="UserControls/RDSCollectionServers.ascx" TagName="CollectionServers" TagPrefix="scp"%>
<%@ Register TagPrefix="scp" TagName="CollapsiblePanel" Src="../UserControls/CollapsiblePanel.ascx" %>
<script type="text/javascript" src="/JavaScript/jquery.min.js?v=1.4.4"></script>

<scp:EnableAsyncTasksSupport id="asyncTasks" runat="server"/>


				<div class="panel-heading">
					<asp:Image ID="imgAddRDSServer" SkinID="EnterpriseRDSCollections48" runat="server" />
					<asp:Localize ID="locTitle" runat="server" meta:resourcekey="locTitle" Text="Create New RDS Collection"></asp:Localize>
				</div>
				<div class="panel-body form-horizontal">
				    <scp:SimpleMessageBox id="messageBox" runat="server" />

					<table>
					    <tr>
						    <td class="FormLabel150" style="width: 100px;"><asp:Localize ID="locCollectionName" runat="server" meta:resourcekey="locCollectionName" Text="Collection Name"></asp:Localize></td>
						    <td>
                                <asp:TextBox ID="txtCollectionName" runat="server" CssClass="TextBox300" />
                                <asp:RequiredFieldValidator ID="valCollectionName" runat="server" ErrorMessage="*" ControlToValidate="txtCollectionName" ValidationGroup="SaveRDSCollection"></asp:RequiredFieldValidator>
						    </td>                            
					    </tr>                        
					</table>                                                                              

                    <fieldset id="RDSServersPanel" runat="server">
                        <legend><asp:Localize ID="locRDSServersSection" runat="server" meta:resourcekey="locRDSServersSection" Text="RDS Servers"></asp:Localize></legend>
                        <div style="padding: 10px;">
                            <scp:CollectionServers id="servers" runat="server" />
                        </div>  
                    </fieldset>
                      

				</div>
				    <div class="panel-footer text-right">
					    <CPCC:StyleButton id="btnSave" CssClass="btn btn-success" runat="server" OnClick="btnSave_Click" OnClientClick="ShowProgressDialog('Adding collection...');" ValidationGroup="SaveRDSCollection"> <i class="fa fa-floppy-o">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="btnSaveText"/> </CPCC:StyleButton>
				    </div>