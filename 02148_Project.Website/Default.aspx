<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_02148_Project.Website._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<!--<script type="text/javascript" src="Scripts/DragDrop.js"></script>-->
<div style="background-image:url(Images/map.png); background-repeat:no-repeat; height: 500px;width: 600px" class="content">


</div>


<input type="button" id="hideshow" value="hide/show"/>

<div class="container-fluid">
  <div class="row-fluid">
        <div class="span3">
      <!--Sidebar content-->
        <p>This is where you buy houses, a blacksmith etc.</p>
        <input runat="server" type="text" id="nameInput" value="Write a mofo username!" />
        <asp:Button runat="server" ID="submitName" OnClick="submitName_Click" Text="Submit"/>
    </div>
        <div class="span2">
            <div "> 
                <table class="table table-hover" style="width:100%">
				    <tr>
					    <td>
					        <!-- <img src="untitled.png" width="30" height="25">
						        <p ID=houseCount>0</p>-->
						    <button type="button" class="btn btn-primary btn-block btn-lg" id="houseButton">
							    BUY (2W, 2T)
						    </button>
					    </td>
				    </tr>
	  		    </table>

            <p>Sell resources here!</p>
            <div style="background-image:url(Images/sellbox.png); width:70px; height:70px"   id="sellBox" ondrop="drop(event)" ondragover="allowDrop(event)">

            </div>
            <input runat="server" class="hidden" id="hiddenValue" type="text" value="" />
            <div id="sellInput" runat="server">
                How much do you want to sell it for? <br />
                <INPUT runat="server" id="inputPrice" type="text" value="Insert price">
                <asp:Button id="buttonCancelSell" runat="server" OnClick="buttonCancelSell_Click" Text="Cancel"></asp:Button>
                <asp:Button id="buttonConfirmSell" runat="server" OnClick="buttonConfirmSell_Click" Text="Confirm Sell"></asp:Button>
            </div>


            </div>
        </div>
        <div class="span8">
    <div style="background-image:url(Images/market.png); background-repeat:no-repeat; height: 500px;width: 600px" class="markedImage" id="marked" >
        <div id="markedContent">
        <asp:Repeater ID="repMarketResources" runat="server" >
          <HeaderTemplate>
            <table>
              <thead>
                <tr>
                </tr>
              </thead>
              <tbody>
          </HeaderTemplate>
          <ItemTemplate>
            <tr class="floating">
              <td>
                  <div class="resource">
                        <img id="<%#Eval("Id")%>" src="<%#Eval("ImageSrc")%>"  />  
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

    <div class="jumbotron">

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
        </div>        
</div>
  </div>
</div>

<script type="text/javascript">
    jQuery(document).ready(function () {
        jQuery("#hideshow").live("click", function (event) {
            jQuery("content").toggle("show");
        });
    });
</script>

<script type="text/javascript">


    function allowDrop(ev) {
        ev.preventDefault();
    }

    function drag(ev) {
        ev.dataTransfer.setData("text/id", ev.target.id);

    }

    function drop(ev) {

        ev.preventDefault();

        var id = ev.dataTransfer.getData("text/id");
        var img = document.getElementById(id);
        ev.target.appendChild(img);
        console.log("ID " + id);
        var hid = document.getElementById("MainContent_hiddenValue");
        hid.setAttribute("value", id);
        console.log("HID " + hid.value);



    }

</script>


</asp:Content>


