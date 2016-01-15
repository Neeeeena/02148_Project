<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarketView.aspx.cs" Inherits="_02148_Project.Website.MarketView" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<!--<script type="text/javascript" src="Scripts/DragDrop.js"></script>-->

<a runat="server" href="~/MapView" class="horseLink"><img src="Images/Horse.png" /></a>

<div class="container-fluid">
  <div class="row-fluid">
        <div class="span3">
      <!--Sidebar content-->
        <p>This is where you buy houses, a blacksmith etc.</p>
        <input runat="server" type="text" id="nameInput" value="Write a mofo username!" />
        <asp:Button runat="server" ID="submitName" OnClick="submitName_Click" Text="Submit"/>
    </div>
        <div class="span2">

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
        <div class="span8">
    <div style="background-image:url(Images/market.png); background-repeat:no-repeat; height: 500px;width: 600px" class="markedImage" id="marked" >
        <div id="markedContent">
            <asp:UpdatePanel ID="up" runat="server">        
    <ContentTemplate>
        <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="timer_Ticked" />
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
		        <img class="resource_image" id="<%#Eval("Id")%>" src="<%#Eval("ImageSrc")%>"/>
		        <h3>Seller: "<%#Eval("SellerName")%>"</h3>
		        <h3>Bidder:" <%#Eval("HighestBidder")%>"</h3>
		        <h3>Bid:" <%#Eval("HighestBid")%>"</h3>
                    <div id="txtfield<%#Eval("Id")%>">
		               <input id="bidInput" type="text" runat="server" />
                        <!--Tilføj javascript funktion der gemmer id i hidden input field-->
                       <asp:Button id="submitBid" runat="server" OnCommand="submitBid_Click" CommandArgument='<%#Eval("Id")%>' Text="Test"></asp:Button>
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
    </ContentTemplate>
</asp:UpdatePanel>
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
            <tr class="dockFloating">
              <td>
                  <div class="dockResource" >
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

    function refresh(){
        location.reload(true);
    }

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

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".resource").each(function () {
            var $id = "#txtfield" + $(this).find("img").attr("id");
            $($id).hide();
        });
        $(".resource_image").click(function () {
            var $id = "#txtfield" + $(this).attr("id");
            $($id).toggle();
        });
    });
</script>


</asp:Content>


