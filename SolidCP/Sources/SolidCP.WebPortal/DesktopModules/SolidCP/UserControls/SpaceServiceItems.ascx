<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpaceServiceItems.ascx.cs" Inherits="SolidCP.Portal.UserControls.SpaceServiceItems" %>
<%@ Register Src="Quota.ascx" TagName="Quota" TagPrefix="scp" %>
<%@ Register Src="ServerDetails.ascx" TagName="ServerDetails" TagPrefix="scp" %>
<%@ Register Src="SearchBox.ascx" TagName="SearchBox" TagPrefix="scp" %>
<%@ Register Src="WebsiteActions.ascx" TagName="WebsiteActions" TagPrefix="scp" %>
<%@ Register Src="MailAccountActions.ascx" TagName="MailAccountActions" TagPrefix="scp" %>
<%@ Register Src="../SkinControls/BootstrapDropDownList.ascx" TagName="BootstrapDropDownList" TagPrefix="scp" %>


<script type="text/javascript">
    function checkAll(selectAllCheckbox) {
        //get all checkbox and select it
        $('td :checkbox').prop("checked", selectAllCheckbox.checked);
    }
    function unCheckSelectAll(selectCheckbox) {
        //if any item is unchecked, uncheck header checkbox as also
        if (!selectCheckbox.checked)
            $('th :checkbox').prop("checked", false);
    }
    function GetAsyncImage(img) {
        img.onload = null;

        var method = $(img).data("method");
        var itemId = $(img).data("itemid");

        $.ajax({
            type: "GET",
            cache: false,
            url: "Handlers/AsyncLoadHandler.ashx?method=" + method + "&" + "itemId=" + itemId,
            datatype: "image",
            success: function (data) {
                $(img).attr("src", data);
            }
        });
    }
    function GetBootstrapAsyncImage(img) {
        img.onload = null;

        var method = $(img).data("method");
        var itemId = $(img).data("itemid");

        $.ajax({
            type: "GET",
            cache: false,
            url: "Handlers/AsyncLoadHandler.ashx?method=" + method + "&" + "itemId=" + itemId,
            datatype: "image",
            success: function (data) {
                $(img).after(data);
                $(img).remove();
            }
        });
    }
    $(document).ready(function () {
        $(".AsyncImage").each(function () { GetAsyncImage(this) });
        $(".BootstrapAsyncImage").each(function () { GetBootstrapAsyncImage(this) });
       
    });
</script>

    <div class="buttons-in-panel-header">
        <asp:Button ID="btnAddItem" runat="server" Text="btnAddItem" CssClass="btn btn-primary" OnClick="btnAddItem_Click" />
        &nbsp;<asp:CheckBox ID="chkRecursive" runat="server" Text="Show Nested Space Items"
            meta:resourcekey="chkRecursive" AutoPostBack="true" Checked="True" CssClass="Normal" />
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-4">
                    <scp:WebsiteActions ID="websiteActions" runat="server" GridViewID="gvItems" CheckboxesName="chkSelectedIds" Visible="False" />
                    <%-- Mail Accounts --%>
                    <scp:MailAccountActions ID="mailActions" runat="server" GridViewID="gvItems" CheckboxesName="chkSelectedIds" Visible="False" />
            </div>
            <div class="col-md-8 text-right form-inline">
                    <scp:SearchBox ID="searchBox" runat="server" />
            </div>
    </div>
    </div>
<asp:Literal ID="litGroupName" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="litTypeName" runat="server" Visible="false"></asp:Literal>

<asp:UpdatePanel ID="ItemsPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
	<ContentTemplate>

<asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" AllowSorting="True"
    DataSourceID="odsItemsPaged" EmptyDataText="gvItems" CssSelectorClass="NormalGridView" DataKeyNames="ItemID"
    AllowPaging="True" OnRowCommand="gvItems_RowCommand" OnRowDataBound="gvItems_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:CheckBox ID="selectAll" Runat="server" onclick="checkAll(this);" CssClass="HeaderCheckbox"></asp:CheckBox>
            </HeaderTemplate>
			<ItemTemplate>							        
				<asp:CheckBox runat="server" ID="chkSelectedIds" onclick="unCheckSelectAll(this);" CssClass="GridCheckbox"></asp:CheckBox>
			</ItemTemplate>
		</asp:TemplateField>

        <asp:TemplateField SortExpression="ItemName" HeaderText="gvItemsName">
            <ItemStyle Width="35%"></ItemStyle>
            <ItemTemplate>
	            <asp:hyperlink id="lnkEdit1" runat="server" CssClass="Medium"
	                NavigateUrl='<%# GetItemEditUrl(Eval("PackageID"), Eval("ItemID")) %>'>
		            <%# Eval("ItemName")%>
	            </asp:hyperlink>
            </ItemTemplate>
        </asp:TemplateField>

                <asp:TemplateField HeaderText="SSL" Visible="false" HeaderStyle-HorizontalAlign="Center">

                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Image ID="IsSSLEnabled" meta:resourcekey="IsSSLEnable" runat="server" ImageUrl='<%# GetThemedImage("loading.gif") %>' ImageAlign="AbsMiddle"
                            data-method="IsSSLEnabled" data-itemid='<%# Eval("ItemID") %>'
                            CssClass="BootstrapAsyncImage"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SSL Exp" Visible="false" HeaderStyle-HorizontalAlign="Center">

                    <ItemStyle Width="200px" Wrap="false" HorizontalAlign="center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Image ID="GetSSLExpDate" meta:resourcekey="GetSSLExpDate" runat="server" ImageUrl='<%# GetThemedImage("loading.gif") %>' ImageAlign="AbsMiddle"
                            data-method="GetSSLExpDate" data-itemid='<%# Eval("ItemID") %>' CssClass="BootstrapAsyncImage"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Redirect" Visible="false" HeaderStyle-HorizontalAlign="Center">

                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Image ID="IsRedirection" meta:resourcekey="IsRedirection" runat="server" ImageUrl='<%# GetThemedImage("loading.gif") %>' ImageAlign="AbsMiddle"
                            data-method="IsRedirection" data-itemid='<%# Eval("ItemID") %>'
                            CssClass="BootstrapAsyncImage"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Infected" Visible="false" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Image ID="IsSiteInfected" meta:resourcekey="IsSiteInfected" runat="server" ImageUrl='<%# GetThemedImage("loading.gif") %>' ImageAlign="AbsMiddle"
                            data-method="IsSiteInfected" data-itemid='<%# Eval("ItemID") %>'
                            CssClass="BootstrapAsyncImage"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Site Status" Visible="false" HeaderStyle-HorizontalAlign="Center">

                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Image ID="WebSiteStatus" meta:resourcekey="WebSiteStatus" runat="server" ImageUrl='<%# GetThemedImage("loading.gif") %>' ImageAlign="AbsMiddle"
                            data-method="WebSiteStatus" data-itemid='<%# Eval("ItemID") %>'
                            CssClass="BootstrapAsyncImage"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Pool Status" Visible="false" HeaderStyle-HorizontalAlign="Center">

                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Image ID="AppPoolStatus" meta:resourcekey="AppPoolStatus" runat="server" ImageUrl='<%# GetThemedImage("loading.gif") %>' ImageAlign="AbsMiddle"
                            data-method="AppPoolStatus" data-itemid='<%# Eval("ItemID") %>'
                            CssClass="BootstrapAsyncImage"/>
                    </ItemTemplate>
                </asp:TemplateField>


        <asp:TemplateField HeaderText="gvItemsView">
			<ItemStyle Wrap="false"></ItemStyle>
            <ItemTemplate>
	            <asp:hyperlink id="lnkView" runat="server" CssClass="Medium" Target="_blank"
	                NavigateUrl='<%# GetUrlNormalized(Eval("PackageID"), Eval("ItemID"))%>'>
	            </asp:hyperlink>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField SortExpression="PackageName" HeaderText="gvItemsSpace">
            <ItemStyle Wrap="False"></ItemStyle>
            <ItemTemplate>
	            <asp:hyperlink id="lnkEdit2" runat="server"
	                NavigateUrl='<%# GetSpaceHomePageUrl((int)Eval("PackageID")) %>'>
		            <%# Eval("PackageName") %>
	            </asp:hyperlink>
            </ItemTemplate>
        </asp:TemplateField>

		<asp:TemplateField SortExpression="Username" HeaderText="gvItemsUser">
		    <ItemStyle Wrap="False"></ItemStyle>
			<ItemTemplate>
				<asp:hyperlink id="lnkEdit3" runat="server"
				    NavigateUrl='<%# GetUserHomePageUrl((int)Eval("UserID")) %>'>
					<%# Eval("Username") %>
				</asp:hyperlink>
			</ItemTemplate>
            <HeaderStyle Wrap="False" />
		</asp:TemplateField>

        <asp:TemplateField SortExpression="ServerName" HeaderText="gvItemsServer">
            <ItemStyle Wrap="False"></ItemStyle>
			<ItemTemplate>
				<asp:hyperlink id="lnkEdit4" runat="server"
				    NavigateUrl='<%# GetItemsPageUrl("ServerID", Eval("ServerID").ToString()) %>'>
					<%# Eval("ServerName") %>
				</asp:hyperlink>
			</ItemTemplate>
        </asp:TemplateField>


        <asp:TemplateField>
			<ItemTemplate>
				<CPCC:StyleButton ID="cmdDetach" runat="server" 
 					CommandName="Detach" CommandArgument='<%# Eval("ItemID") %>'
					CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Remove this item?');">
                    <i class="fa fa-chain-broken">&nbsp;</i>&nbsp;<asp:Localize runat="server" meta:resourcekey="cmdDetachText"/>
                </CPCC:StyleButton>
			</ItemTemplate>
        </asp:TemplateField>
    </Columns>
	<PagerSettings Mode="NumericFirstLast" />
</asp:GridView>
<asp:ObjectDataSource ID="odsItemsPaged" runat="server" EnablePaging="True" SelectCountMethod="GetServiceItemsPagedCount"
    SelectMethod="GetServiceItemsPaged" SortParameterName="sortColumn"
        TypeName="SolidCP.Portal.ServiceItemsHelper" OnSelected="odsItemsPaged_Selected">
    <SelectParameters>
        <asp:QueryStringParameter Name="packageId" QueryStringField="SpaceID" DefaultValue="-1" />
        <asp:ControlParameter Name="groupName" ControlID="litGroupName" PropertyName="Text" />
        <asp:ControlParameter Name="typeName" ControlID="litTypeName" PropertyName="Text" />
        <asp:QueryStringParameter Name="serverId" QueryStringField="ServerID" DefaultValue="0" Type="Int32" />
        <asp:ControlParameter Name="recursive" ControlID="chkRecursive" PropertyName="Checked" DefaultValue="False" />
        <asp:ControlParameter Name="filterColumn" ControlID="searchBox" PropertyName="FilterColumn" />
        <asp:ControlParameter Name="filterValue" ControlID="searchBox" PropertyName="FilterValue" />
    </SelectParameters>
</asp:ObjectDataSource>

	</ContentTemplate>
</asp:UpdatePanel>
<div class="Right" style="float: right; margin-bottom: 5px;">
    <asp:Label ID="lblItemsPerPage" runat="server" meta:resourcekey="lblItemsPerPage" Text="Page size:" CssClass="Normal"></asp:Label>
    <scp:BootstrapDropDownList ID="ddlItemsPerPage" runat="server" CssClass="InlineControl NormalTextBox" 
        AutoPostBack="True" onselectedindexchanged="ddlItemsPerPage_SelectedIndexChanged">
        <asp:ListItem Value="10">10</asp:ListItem>
        <asp:ListItem Value="20">20</asp:ListItem>
        <asp:ListItem Value="50">50</asp:ListItem>
        <asp:ListItem Value="100">100</asp:ListItem>
        <asp:ListItem Value="150">150</asp:ListItem>
        <asp:ListItem Value="200">200</asp:ListItem>
    </scp:BootstrapDropDownList>
</div>
<asp:Panel id="QuotasPanel" runat="server" CssClass="GridFooter">
	<asp:Label ID="lblQuotaName" runat="server" Text="Items" CssClass="NormalBold"></asp:Label>&nbsp;
	<scp:Quota ID="itemsQuota" runat="server" QuotaName="Group.Items" />
</asp:Panel>