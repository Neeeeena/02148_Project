<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarketView.aspx.cs" Inherits="_02148_Project.Website.MarketView" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<!--<script type="text/javascript" src="Scripts/DragDrop.js"></script>-->



<div class="container-fluid">
  <div class="row-fluid">
        <div class="span3">
      <!--Sidebar content-->
            <div runat="server" id="tradeOffers">
                <asp:PlaceHolder ID="tradeOfferASP" runat="server"/>
            </div>
    </div>
        <div class="span2">
            <a runat="server" href="~/MapView" class="horseLink"><img src="Images/Horse.png" /></a>
            <hr />
            <hr />
            <p>Sell resources here!</p>
            <div id="sellBox" ondrop="drop(event)" ondragover="allowDrop(event)">

            </div>
            <input runat="server" class="hidden" id="hiddenValue" type="text" value="" />
            <div id="sellInput" runat="server">
                How much do you want to sell it for? <br />
                <INPUT runat="server" id="inputPrice" type="text" placeholder="Insert a price" 
                    onblur="if (this.value == '') {this.value = 'Insert a price';}"
                    onfocus="if (this.value == 'Insert a price') {this.value = '';}" />
                <asp:Button class="btn btn-default" id="buttonCancelSell" runat="server" OnClick="buttonCancelSell_Click" Text="Cancel"></asp:Button>
                <asp:Button class="btn btn-default" id="buttonConfirmSell" runat="server" OnClick="buttonConfirmSell_Click" Text="Confirm Sell"></asp:Button>
            </div>



            <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#modalTradeOffer">Send TradeOffer</button>

            <!-- Modal -->
            <div id="modalTradeOffer" class="modal fade" role="dialog">
                <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Send Trade Offer</h4>
                    </div>
                    <div class="modal-body">
                        <asp:DropDownList ID="dd" runat="server" Width="200px">
                            <asp:ListItem Value="0"></asp:ListItem>
                            <asp:ListItem Value="1"></asp:ListItem>
                            <asp:ListItem Value="2"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:button class="tradeOfferButton" style="background-image: url(Images/firewood.png)" id="s0" runat="server" onclick="tradeOfferSellerImage0_click" />
                                <label runat="server" id="ls0">0</label>
                                <asp:button class="tradeOfferButton" style="background-image: url(Images/får.png)" id="s1" runat="server" onclick="tradeOfferSellerImage1_click"/>
                                <label runat="server" id="ls1">0</label>
                                <asp:button class="tradeOfferButton" style="background-image: url(Images/mursten.png)" id="s2" runat="server" onclick="tradeOfferSellerImage2_click"/>
                                <label runat="server" id="ls2">0</label>
                                <asp:button class="tradeOfferButton" style="background-image: url(Images/stone.png)" id="s3" runat="server" onclick="tradeOfferSellerImage3_click"/>
                                <label runat="server" id="ls3">0</label>
                                <asp:button class="tradeOfferButton" style="background-image: url(Images/Straw.png);" id="s4" runat="server" onclick="tradeOfferSellerImage4_click"/>
                                <label runat="server" id="ls4">0</label>

                     
                            <div runat="server" id="tradeOfferSeller">

                                
                            </div>
                            <!--
                            <div runat="server" id="tradeOfferReceiver">
                                <img id="r0" runat="server" src="Images/wood.jpg" onclick="tradeOfferReceiver0Image_click"/>
                                <label id="lr0" runat="server">0</label>
                                <img id="r1" runat="server" src="Images/får.png" onclick="tradeOfferReceiver1Image_click"/>
                                <label id="lr1" runat="server">0</label>
                                <img id="r2" runat="server" src="Images/mursten.png" onclick="tradeOfferReceiver2Image_click"/>
                                <label id="lr2" runat="server">0</label>
                                <img id="r3" runat="server" src="Images/stone.png" onclick="tradeOfferReceiver3Image_click"/>
                                <label id="lr3" runat="server">0</label>
                                <img id="r4" runat="server" src="Images/Straw.png" onclick="tradeOfferReceiver4Image_click"/>
                                <label id="lr4" runat="server">0</label>

                            </div>
                            -->
                 
                    
                        <asp:button runat="server" onclick="sendTradeOffer_click"></asp:button>
                            </div>
                    
                    </div>
                    <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

                </div>
            


	<div class="container" style="width:300px">
	  <h2>CHAT</h2>
	  <ul class="nav nav-tabs" >
		<li id="activeChat" style="width:25%"><a data-toggle="tab" href="#all"><p>Everyone</p></a></li>
		<li id="chatTab2" style="width:25%"><a data-toggle="tab" href="#p1" ><p id="player1Tab" runat="server"></p></a></li>
		<li id="chatTab3" style="width:25%"><a data-toggle="tab" href="#p2"><p id="player2Tab" runat="server"></p></a></li>
		<li id="chatTab4" style="width:25%"><a data-toggle="tab" href="#p3"><p id="player3Tab" runat="server"></p></a></li>
	  </ul>

	  <div class="tab-content">
		<div id="all" class="tab-pane fade in active">
			<div class="chatbox" id="allChat" runat="server">
			  <p><b>Market: </b>Everyone can see everything here :)</p>
			</div>
		
			
			<div class="input-group">
			  <input type="text" class="form-control" placeholder="Type your message here :)" id="allMsg" runat="server">
			  <span class="input-group-btn">
                  <asp:Button runat="server" class="btn btn-default" id="btnSendToAll" OnClick="btnSendToAll_Click" Text="Send"/>
			  </span>
			</div>
		</div>
		
		<div id="p1" class="tab-pane fade">
			<div class="chatbox" runat="server" id="p1Chat">
			</div>
		
			
			<div class="input-group">
			  <input type="text" class="form-control" runat="server" placeholder="Type your message here :)" id="p1Msg">
			  <span class="input-group-btn">
				<asp:Button runat="server" class="btn btn-default" id="btnSendToPlayer1" OnClick="btnSendToPlayer1_Click" Text="Send"/>
			  </span>
			</div>
		</div>
          <div id="p2" class="tab-pane fade">
			<div class="chatbox" runat="server" id="p2Chat">
			</div>
		
			
			<div class="input-group">
			  <input type="text" class="form-control" runat="server" placeholder="Type your message here :)" id="p2Msg">
			  <span class="input-group-btn">
				<asp:Button runat="server" class="btn btn-default" id="btnSendToPlayer2" OnClick="btnSendToPlayer2_Click" Text="Send"/>
			  </span>
			</div>
		</div>
          <div id="p3" class="tab-pane fade">
			<div class="chatbox" runat="server" id="p3Chat">
			</div>
		
			
			<div class="input-group">
			  <input type="text" class="form-control" runat="server" placeholder="Type your message here :)" id="p3Msg">
			  <span class="input-group-btn">
				<asp:Button runat="server" class="btn btn-default" id="btnSendToPlayer3" OnClick="btnSendToPlayer3_Click" Text="Send"/>
			  </span>
			</div>
		</div>
		
	  </div>
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
		        <h3>Seller: <%#Eval("SellerName")%></h3>
		        <h3>Bidder: <%#Eval("HighestBidder")%></h3>
		        <h3>Bid: <%#Eval("HighestBid")%></h3>
                <button id="but" class="open-myModal btn btn-default btn-sm" onclick="saveId(this)" data-id="<%#Eval("Id")%>" data-toggle="modal" data-target="#myModal" >Bid</button>

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
        <input runat="server" class="hidden" name="hidId" id="hidId" value="er"/>
      </div>
      <div class="modal-footer">
        <asp:Button runat="server" class="btn btn-success" id="submitBid" OnClick="submitBid_Click" Text="Bid"></asp:Button>
        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
      </div>
    </div>

  </div>
</div>


<script type="text/javascript">
    function saveId(ele) {
        var resId = ele.getAttribute('data-id');

        document.getElementById("MainContent_hidId").setAttribute("value", resId);
        $("#myModal").modal("show");
    }
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

        var hid = document.getElementById("MainContent_hiddenValue");
        hid.setAttribute("value", id);

    }
</script>


<script>

var p1name = "Bob";
var p2name = "p2";
var p3name = "p3";
var p4name = "p4";



var allchat = "";
var name = "Mathias";

$(document).ready(function(){
    $('#allMsg').keypress(function(e){
      if(e.keyCode==13)
      $('#allBtn').click();
    });
	
	$('#p1Msg').keypress(function(e){
      if(e.keyCode==13)
      $('#p1Btn').click();
    });
	
	$('#p2Msg').keypress(function(e){
      if(e.keyCode==13)
      $('#p2Btn').click();
    });
	
	$('#p3Msg').keypress(function(e){
      if(e.keyCode==13)
      $('#p3Btn').click();
    });
	
	$('#allBtn').click(function(e) {
		prepareMsg(name,"all",$(allMsg).val(),'#allChat','#allMsg',false);
	});
	
	$('#p1Btn').click(function(e) {
		prepareMsg(name,p1name,$(p1Msg).val(),'#p1Chat','#p1Msg',true);
	});
	
	
});

/*$(document).ready(function(){
    $('#allBtn').click(function(e){
	if($(allMsg).val() != "") {
	  $("#allChat").append("<p><b>"+name+": </b>"+$(allMsg).val()+"</p>");
	  $('#allChat').scrollTop($('#allChat')[0].scrollHeight);
	  
	  sendMsg(name,"all",$(allMsg).val(),false);
	  
	  $('#allMsg').val("");
	 // recieveMsg("Bob","Hello Mathias. How are you? Can I buy five stone?",true);
	  
	  
	}
	
	
	
      
    });
	
	
	
});
*/


function prepareMsg(sender,recieverName,content,recieverChat,msgTag,personal) {
	if(content != "") {
		$(recieverChat).append("<p><b>"+sender+": </b>"+content+"</p>");
		$(recieverChat).scrollTop($(recieverChat)[0].scrollHeight);
		
		sendMsg(sender,recieverName,content,true);
		
		$(msgTag).val("");
	}
}


function sendMsg(sender, reciever, content, personal) {
}


function recieveMsg(sender, reciever, content, personal) {
	if(!personal) {
		$('#allChat').append("<p><b>"+sender+": </b>"+content+"</p>")
	}else{
		var pl;
		switch (sender) {
			case p1name:
				pl = '#p1hat';
				break;
			case p2name:
				pl = '#p2';
				pk = p2msg;
				break;
			case p3name:
				pl = '#p3';
				pk = p3msg;
				break;
			case p4name:
				pl = '#p4';
				pk = p4msg;
				break;
			default:
				alert("Something happened :(");
		}
		$(pl).append("<p><b>"+sender+": </b>"+content+"</p>")
	}
}


function changeIDOnChat()
{
    $('#activeChat').removeAttr('id');

}


</script>

<script>
    $('#chatTab2').click(function () {
        $().att('id','')
        $(this).attr('id','activeChat');
    });
</script>

<script src="Scripts/jquery-1.10.2.js" type="text/javascript"></script>
<script src="Scripts/jquery.signalR-2.2.0.js" type="text/javascript"></script>
<script src="signalr/hubs" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        // Declare a proxy to reference the hub.
        var notifications = $.connection.chatHub;

        //debugger;
        // Create a function that the hub can call to broadcast messages.
        notifications.client.updateMessages = function () {
            getAllMessages()

        };
        // Start the connection.
        $.connection.hub.start().done(function () {
            alert("connection started")
            getAllMessages();
        }).fail(function (e) {
            alert(e);
        });
    });

    function getAllMessages() {
        alert("Youve got a new message");
    }
</script>

<script>
    $('.')
</script>



</asp:Content>


