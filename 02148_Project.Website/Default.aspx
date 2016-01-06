<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_02148_Project.Website._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="Scripts/DragDrop.js"></script>
    <div class="jumbotron" id="marked"ondrop="drop(event)" ondragover="allowDrop(event)">
       
    </div>

    <div class="row">

        <asp:Repeater ID="localResources" runat="server" >
          <HeaderTemplate>
            <table>
              <thead>
                <tr>
                    <th>Resources</th>
                </tr>
              </thead>
              <tbody>
          </HeaderTemplate>
          <ItemTemplate>
            <tr class="floating">
              <td>
                  <div class="resource" ondragstart="drag(event)" draggable="true">
                    <img src="Images/firewood.png"  id="<%#Eval("Id")%>" />
                 </div>
              </td>
            </tr>
          </ItemTemplate>
          <FooterTemplate>
            </tbody>
            </table>
          </FooterTemplate>
    </asp:Repeater>
    </div>

</asp:Content>
