<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpamExperts_Settings.ascx.cs" Inherits="SolidCP.Portal.ProviderControls.SpamExperts_Settings" %>

<%@ Register Src="../SkinControls/BootstrapDropDownList.ascx" TagName="BootstrapDropDownList" TagPrefix="scp" %>


<div class="row">
    <div class="form-horizontal">

        <div class="form-group">
            <asp:Label CssClass="col-xs-2 control-label" ID="locSchema" runat="server" meta:resourcekey="locSchema" Text="Schema:"></asp:Label>
            <div class="col-xs-10">
                <scp:BootstrapDropDownList ID="ddlSchema" runat="server" CssClass="InlineControl NormalTextBox">
                    <asp:ListItem Value="http" Text="http"></asp:ListItem>
                    <asp:ListItem Value="shttp" Text="shttp"></asp:ListItem>
                </scp:BootstrapDropDownList>
            </div>
        </div>
        <div class="clearfix"></div>

        <div class="form-group">
            <asp:Label CssClass="col-xs-2 control-label" ID="locUrl" runat="server" meta:resourcekey="locUrl" Text="Url:"></asp:Label>
            <div class="col-xs-9 col-sm-8 col-md-6 col-lg-5">
                <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                <asp:RequiredFieldValidator ID="valRequireUrl" runat="server" meta:resourcekey="valUrl" ControlToValidate="txtUrl"
                    ErrorMessage="*" ValidationGroup="Edit" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="clearfix"></div>

        <div class="form-group">
            <asp:Label CssClass="col-xs-2 control-label" ID="locUser" runat="server" meta:resourcekey="locUser" Text="User:"></asp:Label>
            <div class="col-xs-9 col-sm-8 col-md-6 col-lg-5">
                <asp:TextBox ID="txtAdminUser" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                <asp:RequiredFieldValidator ID="valRequireUser" runat="server" meta:resourcekey="valUser" ControlToValidate="txtAdminUser"
                    ErrorMessage="*" ValidationGroup="Edit" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="clearfix"></div>

        <div class="form-group">
            <asp:Label CssClass="col-xs-2 control-label" ID="locPassword" runat="server" meta:resourcekey="locPassword" Text="Password:"></asp:Label>
            <div class="col-xs-9 col-sm-8 col-md-6 col-lg-5">
                <asp:TextBox ID="txtAdminPassword" runat="server" CssClass="form-control" TextMode="Password" autocomplete="off"></asp:TextBox>
            </div>
        </div>
        <div class="clearfix"></div>

        <div class="form-group">
            <asp:Label CssClass="col-xs-2 control-label" ID="locRouteFromSE" runat="server" meta:resourcekey="locRouteFromSE" 
                Text="Delivery routes:"></asp:Label>
            <div class="col-xs-9 col-sm-8 col-md-6 col-lg-5">

                <asp:UpdatePanel ID="RouteFromSEUpdatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <div class="row">
                            <asp:GridView CssClass="table table-hover" GridLines="None"  OnPreRender="GridView_PreRender"
                                id="gvSEDestinations" runat="server"  EnableViewState="true" AutoGenerateColumns="false"
	                            Width="100%" EmptyDataText="" OnRowCommand="gvSEDestinations_RowCommand" >
	                        <Columns>
		                        <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:Label CssClass="locDestinations" runat="server" meta:resourcekey="locDestinations" Text="Destinations"/>
                                            </div>
                                        </div>
                                    </HeaderTemplate>
			                        <ItemTemplate>
                                        <div class="row">
                                            <div class="col-xs-11">
        				                        <asp:Label id="lblSEDestination" runat="server" EnableViewState="true"><%# (Container.DataItem.ToString()) %></asp:Label>
                                            </div>
                                            <div class="col-xs-1">
				                                <asp:ImageButton id="imgDelRouteFromSE" runat="server" Text="Delete" SkinID="ExchangeDelete"
					                                CommandName="DeleteItem" CommandArgument='<%# (Container.DataItem.ToString()) %>' 
					                                meta:resourcekey="cmdDelete" OnClientClick="return confirm('Are you sure you want to delete selected route?')" />
                                            </div>
                                        </div>
                                    </ItemTemplate>
		                        </asp:TemplateField>
	                        </Columns>
	                        </asp:GridView>
                        </div>
                        <div class="row">
                            <asp:TextBox CssClass="form-control InlineControl" ID="tbSEDestinations" runat="server"></asp:TextBox>
                            <asp:Button CssClass="btn btn-default Button1" ID="bntAddSEDestination" runat="server" Text="Add" meta:resourcekey="bntAddSEDestination" OnClick="bntAddSEDestination_Click" CausesValidation="false"/>
                        </div>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

        </div>
        <div class="clearfix"></div>


    </div>
</div>