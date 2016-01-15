<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarketView.aspx.cs" Inherits="_02148_Project.Website.MarketView" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<!--<script type="text/javascript" src="Scripts/DragDrop.js"></script>-->

<a runat="server" href="~/MapView" class="horseLink"><img src="Images/Horse.png" /></a>

<div class="container-fluid">
  <div class="row-fluid">
        <div class="span3">
      <!--Sidebar content-->
        
    </div>
        <div class="span2">

            <p>Sell resources here!</p>
            <div style="background-image:url(Images/sellbox.png); width:70px; height:70px"   id="sellBox" ondrop="drop(event)" ondragover="allowDrop(event)">

            </div>
            <input runat="server" class="hidden" id="hiddenValue" type="text" value="" />
            <div id="sellInput" runat="server">
                How much do you want to sell it for? <br />
                <INPUT runat="server" id="inputPrice" type="text" value="Insert a price" 
                    onblur="if (this.value == '') {this.value = 'Insert a price';}"
                    onfocus="if (this.value == 'Insert a price') {this.value = '';}" />
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
                <button id="but" class="open-myModal" data-id="<%#Eval("Id")%>" data-toggle="modal" data-target="#myModal" >Bid</button>

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
<div class="alert alert-warning" id="bidwarning" visible="false" runat="server">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
</div>

    <div>

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

<!-- Modal -->
<div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Bid on resource</h4>
      </div>
      <div class="modal-body">
          <p>Bid:</p>
          <input type="text" id="bidPrice" runat="server"/>
        <input runat="server" class="hidden" name="hidId" id="hidId" value=""/>
      </div>
      <div class="modal-footer">
        <asp:Button runat="server" class="btn btn-success" id="submitBid" OnClick="submitBid_Click" Text="Bid"></asp:Button>
        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
      </div>
    </div>

  </div>
</div>

<script type="text/javascript">
    $(".open-myModal").click(function () {
        var resId = $(this).data('id');
        document.getElementById("MainContent_hidId").setAttribute("value", resId);
        $("#myModal").modal("show");
    });
</script>


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










</asp:Content>


