<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Party.aspx.cs" Inherits="_02148_Project.Website.Party" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="party">
        <h1>Users online now</h1>
        <asp:UpdatePanel ID="up" runat="server">        
    <ContentTemplate>
        <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="timer_Ticked" />
        <asp:Repeater ID="repOnlinePlayers" runat="server" >
          <ItemTemplate>
                  <p class="playerOnline"><%#Eval("Name")%></p> </ br>
          </ItemTemplate>
    </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>
    </div>

</asp:Content>


