<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MSO365UsersImport.ascx.cs" Inherits="SolidCP.Portal.ExchangeServer.UserControls.MSO365.MSO365UsersImport" %>
<%@ Register Src="../LocationSelector.ascx" TagName="LocationSelector" TagPrefix="wsp" %>
<%@ Register Src="../ServiceLevelSelector.ascx" TagName="ServiceLevelSelector" TagPrefix="mspc" %>

<div style="display: inline-block">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <CPCC:StyleButton ID="btnImportO365Users" runat="server" meta:resourcekey="btnImportO365Users"
                Text="Import O365 Users" CssClass="btn btn-default Button1" OnClick="btnImportUsers_Click" OnClientClick="return ShowProgressDialog('Loading...');" />

            <ajaxToolkit:ModalPopupExtender ID="Modal" runat="server" EnableViewState="true" TargetControlID="btnImportO365UsersFake"
                PopupControlID="O365UsersImportPanel" BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="btnModalCancel" />

            <CPCC:StyleButton ID="btnImportO365UsersFake" runat="server" Style="display: none;" />

            <asp:Panel ID="O365UsersImportPanel" runat="server" CssClass="Popup" Style="display: none">
                <table class="Popup-Header">
                    <tr>
                        <td class="Popup-HeaderLeft"></td>
                        <td class="Popup-HeaderTitle">
                            <asp:Localize ID="headerImportO365Users" runat="server" meta:resourcekey="headerImportO365Users" /></td>
                        <td class="Popup-HeaderRight"></td>
                    </tr>
                </table>
                <div class="Popup-Content">
                    <div class="Popup-Body" style="padding-top: 10px;">
                        <div runat="server" ID="locationPanel" style="line-height: 44px">
                            <asp:Localize ID="locLocation" runat="server" meta:resourcekey="locLocation" />&nbsp;
                            <wsp:LocationSelector ID="locationSelector" runat="server" />
                        </div>
                        <div style="line-height: 44px" id="ServiceLevelo365Row" runat="server">
                            <asp:Localize ID="locServiceLevel" runat="server" meta:resourcekey="locServiceLevel" />&nbsp;
                            <mspc:ServiceLevelSelector ID="serviceLevelSelector" runat="server" />&nbsp;
                            <asp:RequiredFieldValidator runat="server" ID="reqServiceLevels" meta:resourcekey="reqServiceLevels" ControlToValidate="serviceLevelSelector"
                                  InitialValue="0" Display="Dynamic" ValidationGroup="o365Import" />
                        </div>
                        <div class="Popup-Scroll">
                            <asp:GridView CssClass="table table-striped table-hover" GridLines="None" OnPreRender="GridView_PreRender" PagerStyle-CssClass="pagination-bs" ID="gvO365UsersImport" runat="server" meta:resourcekey="gvO365UsersImport" AutoGenerateColumns="False"
                                Width="600px" 
                                DataKeyNames="UserPrincipalName">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllImportUsersCheckBoxCheckboxes(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField meta:resourcekey="gvO365UsersDisplayName" HeaderText="gvO365UsersDisplayName">
                                        <HeaderStyle Wrap="false" />
                                        <ItemStyle Width="50%" Wrap="false"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnCountry" Value='<%# Eval("Country") %>'/>
                                            <asp:Literal ID="litDisplayName" runat="server" Text='<%# Eval("DisplayName") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField meta:resourcekey="gvO365UsersImportUpn" HeaderText="gvO365UsersImportUpn">
                                        <HeaderStyle Wrap="false" />
                                        <ItemStyle Width="50%" Wrap="false"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Literal ID="litUpn" runat="server" Text='<%# Eval("UserPrincipalName") %>'></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="FormFooterMiddle">
                        <CPCC:StyleButton ID="btnModalImportUsers" runat="server" CssClass="btn btn-default Button1" meta:resourcekey="btnModalImportUsers" Text="Import"
                            OnClientClick="return CloseAndShowProgressDialog('Importing...')" OnClick="btnModalImportUsers_Click" ValidationGroup="o365Import" />
                        <CPCC:StyleButton ID="btnModalCancel" runat="server" CssClass="btn btn-default Button1" meta:resourcekey="btnModalCancel" Text="Cancel"
                            OnClick="btnModalCancel_OnClick" CausesValidation="false" />
                    </div>
                </div>
            </asp:Panel>

            <script language="javascript">
                function SelectAllImportUsersCheckBoxCheckboxes(box) {
                    $("#<%= gvO365UsersImport.ClientID %> tbody :checkbox").attr("checked", $(box).attr("checked"));
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnModalImportUsers" />
            <asp:PostBackTrigger ControlID="btnImportO365Users" />
        </Triggers>
    </asp:UpdatePanel>
</div>
