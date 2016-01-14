<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_02148_Project.Website._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


<h1>Choose a player name</h1>
<input runat="server" id="username"/>
<asp:Button runat="server" ID="submitusername" OnClick="submitusername_Click" Text="Submit"></asp:Button>
<div class="alert alert-warning" id="userwarning" visible="false" runat="server"></div>

<h1>Login as existing player</h1>
<input runat="server" id="existingusername"/>
<asp:Button runat="server" ID="submitexistingusername" OnClick="submitexistingusername_Click" Text="Submit"></asp:Button>
<div class="alert alert-warning" id="existinguserwarning" visible="false" runat="server"></div>
</asp:Content>


