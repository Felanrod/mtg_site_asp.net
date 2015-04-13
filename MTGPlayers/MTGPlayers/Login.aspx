<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MTGPlayers.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        Login
    </h2>
    <div>
        <asp:label id="lblError" runat="server" CssClass="failureNotification"></asp:label>
    </div>
    <div>
        <label for="txtPlayerName">Username</label>
        <asp:TextBox ID="txtPlayerName" runat="server"></asp:TextBox>
    </div>
    <div>
        <label for="txtPass">Password</label>
        <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
    </div>
    <asp:Button ID="btnLogin" runat="server" Text="Login" 
        onclick="btnLogin_Click" />
</asp:Content>
