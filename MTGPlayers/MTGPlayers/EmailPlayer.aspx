<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailPlayer.aspx.cs" Inherits="MTGPlayers.EmailPlayer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div>
    <label for="txtFrom">From:</label>
    <asp:TextBox ID="txtFrom" runat="server" MaxLength="50"></asp:TextBox>
</div>
<div>
    <label for="txtFrom">Subject:</label>
    <asp:TextBox ID="TextBox1" runat="server" MaxLength="50"></asp:TextBox>
</div>
</asp:Content>
