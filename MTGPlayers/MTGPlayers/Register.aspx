<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MTGPlayers.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
       Register!
    </h2>
    <div>
        <asp:label id="lblError" runat="server" CssClass="failureNotification" ></asp:label>
    </div>
    <div>
        <label for="txtPlayerName">Player Name:</label>
        <asp:TextBox ID="txtPlayerName" runat="server" MaxLength="50"></asp:TextBox>
    </div>
    <div>
        <label for="ddlProvince">Select Your Province:</label>
        <asp:DropDownList ID="ddlProvince" runat="server" AppendDataBoundItems="true"
            DataTextField="ProvinceName" DataValueField="ProvinceID" AutoPostBack="true" 
            onselectedindexchanged="ddlProvince_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div>
        <label for="ddlCity">Select Your City:</label>
        <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="true"
            DataTextField="CityName" DataValueField="CityID" AutoPostBack="true" 
            onselectedindexchanged="ddlCity_SelectedIndexChanged">
            <asp:ListItem Text="Different City" Value="0"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label for="ddlStore">Select Your Store:</label>
        <asp:DropDownList ID="ddlStore" runat="server" AppendDataBoundItems="true"
            DataTextField="StoreName" DataValueField="StoreID">
            <asp:ListItem Text="NA" Value="0"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label for="ddlPlayed">How much have you played?</label>
        <asp:DropDownList ID="ddlPlayed" runat="server" AppendDataBoundItems="true"
            DataTextField="TimePlayed" DataValueField="PlayedID">
            <asp:ListItem Text="NA" Value="0"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label for="txtEmail">Email: </label>
        <asp:TextBox ID="txtEmail" runat="server"  MaxLength="50"></asp:TextBox>
        <asp:RegularExpressionValidator ID="revEmail" runat="server"
         ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
         ControlToValidate="txtEmail" ErrorMessage="Not a valid email!"
         CssClass="failureNotification">
         </asp:RegularExpressionValidator>
    </div>
    <div>
        <label for="cbEmail">I would like to show my email to others: </label>
        <asp:CheckBox ID="cbEmail" runat="server" Checked="false" />
    </div>
    <div>
        <label for="txtAbout">A little about yourself: </label>
        <asp:TextBox ID="txtAbout" runat="server" TextMode="MultiLine"
         MaxLength="140" />
    </div>
    <div>
        <label for="txtPass">Password</label>
        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
    </div>
     <div>
        <label for="txtPass2">Re-Enter Password</label>
        <asp:TextBox ID="txtPass2" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
        <asp:CompareValidator ID="cvPassword"
            runat="server" ErrorMessage="Passwords Need to Match" ControlToValidate="txtPass2"
             ControlToCompare="txtPass" CssClass="failureNotification" Display="Dynamic"></asp:CompareValidator>
    </div>
    <div>
        <label for="rblRole">Role</label>
        <asp:RadioButtonList ID="rblRole" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="User" Text="User" Selected="True"></asp:ListItem>
            <asp:ListItem Value="Admin" Text="Admin"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <asp:Button ID="btnRegister" runat="server" Text="Register" 
        onclick="btnRegister_Click" />
</asp:Content>
