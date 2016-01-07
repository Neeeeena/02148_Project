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
            <div class="col-1" id="cell1"  ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1"id="cell2" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell3" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell4" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell5" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell6" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell7" ondrop="drop(event)" ondragover="allowDrop(event)"></div>
            <div class="col-1" id="cell8" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell9" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell10" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell11" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell12" ondrop="drop(event)" ondragover="allowDrop(event)"></div>  
        </div> 
        <div class="row">
            <div class="col-1" id="cell13" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell14" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell15" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell16" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell17" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell18" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell19" ondrop="drop(event)" ondragover="allowDrop(event)"></div>
            <div class="col-1" id="cell20" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell21" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell22" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell23" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
            <div class="col-1" id="cell24" ondrop="drop(event)" ondragover="allowDrop(event)"></div> 
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
                  <div class="resource" >
                      <div >
                            <img id="<%#Eval("Id")%>" ondragstart="drag(event)" draggable="true" src="Images/firewood.png"  />
                          </div>
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


