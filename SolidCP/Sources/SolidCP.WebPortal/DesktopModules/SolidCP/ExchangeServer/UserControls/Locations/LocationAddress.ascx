<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocationAddress.aspx.cs" Inherits="SolidCP.Portal.ExchangeServer.UserControls.Locations.LocationAddress" %>

<%@ Register Src="../CountrySelector.ascx" TagName="CountrySelector" TagPrefix="wsp" %>
<%@ Register Src="../StateSelector.ascx" TagName="StateSelector" TagPrefix="wsp" %>

<div class="form-horizontal">

    <div class="form-group">
        <asp:Label CssClass="col-xs-2 control-label" ID="locAddress" runat="server" meta:resourcekey="locAddress" Text="Street Address:"></asp:Label>
        <div class="col-xs-10 col-sm-8 col-md-6 col-lg-5">
            <asp:TextBox ID="txtAddress" runat="server" Style="margin-bottom: 6px;" CssClass="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="clearfix"></div>

    <div class="form-group" runat="server" visible="false" id="divAddress2">
        <asp:Label CssClass="col-xs-2 control-label" ID="locAddress2" runat="server" meta:resourcekey="locAddress2" Text="Street Address 2:"></asp:Label>
        <div class="col-xs-10 col-sm-8 col-md-6 col-lg-5">
            <asp:TextBox ID="txtAddress2" runat="server" Style="margin-bottom: 6px;" CssClass="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="clearfix"></div>

    <div class="form-group">
        <asp:Label CssClass="col-xs-2 control-label" ID="lblCity" runat="server"  meta:resourcekey="lblCity" Text="City:" />
        <div class="col-xs-10 col-sm-8 col-md-6 col-lg-5">
            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
        </div>
    </div>
    <div class="clearfix"></div>

    <div class="form-group">
        <asp:Label CssClass="col-xs-2 control-label" ID="lblCountry" runat="server"  meta:resourcekey="lblCountry" Text="Country:" />
        <div class="col-xs-10 col-sm-8 col-md-6 col-lg-5">
            <wsp:CountrySelector ID="country" runat="server" ExcludeDefault="True" SetDefaultValue="True" />
        </div>
    </div>
    <div class="clearfix"></div>

    <div class="form-group">
        <asp:Label CssClass="col-xs-2 control-label" ID="lblRegion" runat="server" meta:resourcekey="lblRegion" Text="Region (State):" />
        <div class="col-xs-10 col-sm-8 col-md-6 col-lg-5">
            <wsp:StateSelector ID="stateSelector" runat="server" />
        </div>
    </div>
    <div class="clearfix"></div>

    <div class="form-group">
        <asp:Label CssClass="col-xs-2 control-label" ID="lblPostalCode" runat="server" meta:resourcekey="lblPostalCode" Text="Postal Code:" />
        <div class="col-xs-10 col-sm-8 col-md-6 col-lg-5">
            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control"/>
            <asp:RequiredFieldValidator ID="valReqPostalCode" runat="server" ControlToValidate="txtPostalCode" meta:resourcekey="valReqPostalCode" ErrorMessage="Enter Postal Code" Text="*"/>
            <asp:RegularExpressionValidator ID="valRangePostalCode" ControlToValidate="txtPostalCode" ErrorMessage="Incorrect format" Text="Incorrect format" runat="server" ValidationExpression="" />
        </div>
    </div>
    <div class="clearfix"></div>

    <div class="form-group" id="divPhone" runat="server">
        <asp:Label CssClass="col-xs-2 control-label" ID="lblPhoneNumber" runat="server" meta:resourcekey="lblPhoneNumber" Text="Phone Number:" />
        <div class="col-xs-10 col-sm-8 col-md-6 col-lg-5">
            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" />           
        </div>
    </div>
    <div class="clearfix"></div>

    <div class="form-group" runat="server" id="divSecPhone">
		<asp:Label CssClass="col-xs-2 control-label" ID="lblSecPhone" runat="server" meta:resourcekey="lblSecPhone" Text="Secondary phone:"></asp:Label>
        <div class="col-xs-10 col-sm-8 col-md-6 col-lg-5">
			<asp:TextBox id="txtSecondaryPhone" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="clearfix"></div>

    <div class="form-group" runat="server" id="divFax">
		<asp:Label CssClass="col-xs-2 control-label" ID="lblFax" runat="server" meta:resourcekey="lblFax" Text="Fax:"></asp:Label>
        <div class="col-xs-10 col-sm-8 col-md-6 col-lg-5">
			<asp:TextBox id="txtFax" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="clearfix"></div>

</div>
