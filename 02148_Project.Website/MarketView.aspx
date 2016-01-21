<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarketView.aspx.cs" Inherits="_02148_Project.Website.MarketView" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<!--<script type="text/javascript" src="Scripts/DragDrop.js"></script>-->


<div class="container-fluid">
  <div class="row-fluid">
        <div class="span3">
            <h2 runat="server" id="playerName"></h2>
            <figure>
                <img id="goldImage" src="Images/gold3.png" />
                <figcaption><p runat="server" id="goldAmount"></p></figcaption>
            </figure>
            <button type="button" class="btn btn-default btn-md" data-toggle="modal" data-target="#modalTradeOffer">Send TradeOffer</button>
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
                <asp:Button class="btn btn-default" id="buttonConfirmSell" runat="server" OnClick="buttonConfirmSell_Click" Text="Confirm Sell"></asp:Button>
                <asp:Button class="btn btn-default" id="buttonCancelSell" runat="server" OnClick="buttonCancelSell_Click" Text="Cancel"></asp:Button>
                
            </div>



            

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

                                

                                <!--

                                <div class="resContainer">
                                
                                <div id="ds2" class="tradeOfferDiv">
                                    <img src="Images/får.png" />
                                    <asp:label runat="server" id="lbls2">0</asp:label>
                                </div>
                                <div id="ds3" class="tradeOfferDiv">
                                    <img src="Images/mursten.png" />
                                    <asp:label runat="server" id="lbls3" >0</asp:label>
                                </div>
                                <div id="ds4" class="tradeOfferDiv">
                                    <img src="Images/stone.png" />
                                    <asp:label runat="server" id="lbls4">0</asp:label>
                                </div>
                                <div id="ds5" class="tradeOfferDiv">
                                    <img src="Images/Straw.png" />
                                    <asp:label runat="server" id="lbls5">0</asp:label>
                                </div>
                                <div id="ds6" class="tradeOfferDiv">
                                    <img src="Images/anvil.png" />
                                    <asp:label runat="server" id="lbls6">0</asp:label>
                                </div>
                                <div id="ds7" class="tradeOfferDiv">
                                    <img src="Images/food.png" />
                                    <asp:label runat="server" id="lbls7">0</asp:label>
                                </div>
                                </div>
                                <div class="resContainer">
                                <h3><b>Resources you want to receive:</b></h3>
                                <div class="tradeOfferDiv">
                                    <img src="Images/firewood.png" />
                                    <label runat="server" id="r0">0</label>
                                </div>
                                <div class="tradeOfferDiv">
                                    <img src="Images/får.png" />
                                    <label runat="server" id="r1">0</label>
                                </div>
                                <div class="tradeOfferDiv">
                                    <img src="Images/mursten.png" />
                                    <label runat="server" id="r2">0</label>
                                </div>
                                <div class="tradeOfferDiv">
                                    <img src="Images/stone.png" />
                                    <label runat="server" id="r3">0</label>
                                </div>
                                <div class="tradeOfferDiv">
                                    <img src="Images/Straw.png" />
                                    <label runat="server" id="r4">0</label>
                                </div>
                                <div class="tradeOfferDiv">
                                    <img src="Images/anvil.png" />
                                    <label runat="server" id="r5">0</label>
                            </div>
                                <div class="tradeOfferDiv">
                                    <img src="Images/food.png" />
                                    <label runat="server" id="r6">0</label>
                            </div>

                                


                            </div>
                                <br/>
                           <asp:button Text="Send Trade Offer" runat="server" id="sendTradeOfferBtns" onclick="sendTradeOffer_click"></asp:button>
                            

                                 <div>
                                 <asp:UpdatePanel ID="UpdatePanel10" runat="server"
                                 UpdateMode="Conditional">
                                 <ContentTemplate>
                                
                                 

                                 
                                 </ContentTemplate>
                                  <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                 </Triggers>
                                 </asp:UpdatePanel>
                                 </div>
                                    -->
                        <h3><b>Resources you want to sell:</b></h3>
                                <div id="ds1" class="tradeOfferDiv">
                                    <img src="Images/firewood.png" />
                                    <p runat="server" id="lbls1">0</p> 
                                </div>
                        <asp:Button ID="Button1" runat="server"
                                 Text="Update Both Panels" OnClick="Button1_Click" />


                    </div>
                    <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>
        <asp:Timer ID="Timer2" runat="server" Interval="1000" OnTick="timer_Ticked" />
        <asp:Repeater ID="repAllChat" runat="server" >
          <ItemTemplate>
                <div class="message">
                    <li class=<%# Eval("htmlClass")%> ><span> <%# Eval("SenderName")%> : <%#Eval("Content")%> </span></li>
                </div>
          </ItemTemplate>
    </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>


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
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">        
    <ContentTemplate>
        <asp:Timer ID="Timer3" runat="server" Interval="1000" OnTick="timer_Ticked" />
        <asp:Repeater ID="repP1Chat" runat="server" >

          <ItemTemplate>

                <div class="message">
                    <p class=<%# Eval("htmlClass")%> ><%# Eval("SenderName")%> : <%#Eval("Content")%></p> <br />
                </div>
          </ItemTemplate>
    </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>
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
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">        
    <ContentTemplate>
        <asp:Timer ID="Timer4" runat="server" Interval="1000" OnTick="timer_Ticked" />
        <asp:Repeater ID="repP2Chat" runat="server" >
         
          <ItemTemplate>

                <div class="message">
                    <p class=<%# Eval("htmlClass")%> ><%# Eval("SenderName")%> : <%#Eval("Content")%></p>
                    <hr />
                </div>
    </ItemTemplate>
    </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>
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
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">        
    <ContentTemplate>
        <asp:Timer ID="Timer5" runat="server" Interval="1000" OnTick="timer_Ticked" />
        <asp:Repeater ID="repP3Chat" runat="server" >      
          <ItemTemplate>
                <div class="message">
                    <p class=<%# Eval("htmlClass")%> ><%# Eval("SenderName")%> : <%#Eval("Content")%></p><br/>
                </div>
          </ItemTemplate>
    </asp:Repeater>
    </ContentTemplate>
</asp:UpdatePanel>
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


$(document).ready(function(){
    
    $('#allMsg').keypress(function(e){
      if(e.keyCode==13)
      $('#allBtn').click();
        alert("test");
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

</script>



    
<script type="text/javascript">

    /* LORTET HER VIRKER!
    $(document).ready(function () {
        $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "MarketView.aspx/AjaxCall",
                dataType: "json",
                data:'{}',
                success: function (result) {
                    alert("Succes!");
                    // window.location.href = "ClubCreation.aspx";
                },
                Error: function () {
                    alert('error');
                }
        });
    });
     */   
    
</script>

<script>
    
    $(document).ready(function () {
        $("#ds1").click(function () {
            document.getElementById('MainContent_lbls1').val() = 1;
        });
        $("#ds2").click(function () {
            document.getElementById('MainContent_lbls2').innerHTML = parseInt(document.getElementById('MainContent_lbls2').innerHTML) + 1;
        });
        $("#ds3").click(function () {
            document.getElementById('MainContent_lbls3').innerHTML = parseInt(document.getElementById('MainContent_lbls3').innerHTML) + 1;
        });
        $("#ds4").click(function () {
            document.getElementById('MainContent_lbls4').innerHTML = parseInt(document.getElementById('MainContent_lbls4').innerHTML) + 1;
        });
        $("#ds5").click(function () {
            document.getElementById('MainContent_lbls5').innerHTML = parseInt(document.getElementById('MainContent_lbls5').innerHTML) + 1;
        });
        $("#ds6").click(function () {
            document.getElementById('MainContent_lbls6').innerHTML = parseInt(document.getElementById('MainContent_lbls6').innerHTML) + 1;
        });
        $("#ds7").click(function () {
            document.getElementById('MainContent_lbls7').innerHTML = parseInt(document.getElementById('MainContent_lbls7').innerHTML) + 1;
        });
    });
</script>


    <!--
<script type="text/javascript">
    $(document).ready(function(){
        $(".tradeOfferDiv").click(function () {
            $(this).find("#lbls1").Text = $(this).find("#lbls1").Text

            
            $(this).find("#lbls1").text(String(parseInt($(this).find("#lbls1").text()) + 1));
            
        });
    });
</script>

        -->

<script>
    $('#chatTab2').click(function () {
        $().att('id','')
        $(this).attr('id','activeChat');
    });
</script>





</asp:Content>


