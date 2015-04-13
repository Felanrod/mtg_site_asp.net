<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stores.aspx.cs" Inherits="MTGPlayers.Stores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>
    Store List
</h2>

<asp:GridView runat="server" ID="gvStores" AutoGenerateColumns="false"
 DataKeyNames="StoreID" OnRowDeleting="gvStores_RowDeleting" 
        onrowdatabound="gvStores_RowDataBound" >
    <Columns>
        <asp:BoundField HeaderText="Store ID" DataField="StoreID" />
        <asp:BoundField HeaderText="Store Name" DataField="StoreName" />
        <asp:BoundField HeaderText="City" DataField="CityName" />
        <asp:BoundField HeaderText="Address" DataField="StoreAddress" />
        <asp:BoundField HeaderText="Phone Number" DataField="PhoneNumber" />
        <asp:HyperLinkField HeaderText="Edit" DataNavigateUrlFields="StoreID"
            Text="Edit" />
        <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" />
    </Columns>
</asp:GridView>
</asp:Content>
