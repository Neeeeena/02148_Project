<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_02148_Project.Website._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="Scripts/DragDrop.js"></script>
    <div class="container-fluid">
  <div class="row-fluid">
    <div class="span2">
      <!--Sidebar content-->
        <p>This is where you buy houses, a blacksmith etc.</p>
    </div>
      <div class="span10">
    <div class="jumbotron" id="marked" >
        <h3>Marked</h3>
       <div class="grid-container outline">
           
        <div class="row">
            <div class="col-1" id="cell1" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1"id="cell2" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell3" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell4" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell5"  ondragstart="drag(event)" draggable="true"ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell6" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell7" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div>
            <div class="col-1" id="cell8" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell9" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell10" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell11" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell12" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div>  
        </div> 
        <div class="row">
            <div class="col-1" id="cell13" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell14" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell15" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell16" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell17" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell18" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell19" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div>
            <div class="col-1" id="cell20" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell21" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell22" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell23" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell24" ondragstart="drag(event)" draggable="true" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
        </div>  

    </div>
        </div>

    <div class="jumbotron">

        <asp:Repeater ID="localResources" runat="server" >
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
</div>
  </div>
</div>
</asp:Content>
