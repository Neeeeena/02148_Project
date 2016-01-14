<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapView.aspx.cs" Inherits="_02148_Project.Website.MapView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<a runat="server" href="~/" class="horseLink"><img src="Images/Horse.png" /></a>

<div style="background-image:url(Images/map.png); background-repeat:no-repeat; height: 600px;width: 1000px" class="markedImage" id="marked" >
    
</div>
        <asp:Repeater ID="repLocalResources" runat="server" >
          <HeaderTemplate>
            <table>
              <thead>
                <tr>
                    <th id="headlineResources"><h3 >Resources</h3></th>
                </tr>
              </thead>
              <tbody>
          </HeaderTemplate>
          <ItemTemplate>
            <tr class="floating">
              <td>
                  <div class="resource" >
                       <img id="<%#Eval("Id")%>" ondragstart="drag(event)" draggable="true" src="<%#Eval("ImageSrc")%>"  />
                 </div>
              </td>
            </tr>
          </ItemTemplate>
          <FooterTemplate>
            </tbody>
            </table>
          </FooterTemplate>
    </asp:Repeater>
</asp:Content>
