<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Players.aspx.cs" Inherits="MTGPlayers.Players" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>
    Player List
</h2>
<asp:GridView runat="server" ID="gvPlayers" AutoGenerateColumns="false"
 DataKeyNames="PlayerID" OnRowDeleting="gvPlayers_RowDeleting" 
        onrowdatabound="gvPlayers_RowDataBound" >
    <Columns>
        <asp:BoundField HeaderText="Player ID" DataField="PlayerID" />
        <asp:BoundField HeaderText="Player Name" DataField="PlayerName" />
        <asp:BoundField HeaderText="Province" DataField="ProvinceName" />
        <asp:BoundField HeaderText="City" DataField="CityName" />
        <asp:BoundField HeaderText="Store" DataField="StoreName" />
        <asp:BoundField HeaderText="Time Played" DataField="TimePlayed" />
        <asp:BoundField HeaderText="Email" DataField="Email" />
        <asp:BoundField HeaderText="About" DataField="About" />
        <asp:BoundField HeaderText="Role" DataField="Role" />
        <asp:HyperLinkField HeaderText="Edit" DataNavigateUrlFields="PlayerID"
            DataNavigateUrlFormatString="EditUser.aspx?PlayerID={0}"
            Text="Edit" />
        <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" />
    </Columns>
</asp:GridView>
</asp:Content>
