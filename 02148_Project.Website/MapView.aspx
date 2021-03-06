﻿<%@ Page Title="Map" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapView.aspx.cs" Inherits="_02148_Project.Website.MapView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


<div class="container-fluid">
  <div class="row-fluid">
          <div class="span3">
               <h2 runat="server" id="playerName"></h2>
            <figure>
                <img id="goldImage" src="Images/gold3.png" />
                <figcaption><p runat="server" id="goldAmount"></p></figcaption>
            </figure>
                <table class="table table-hover">
				    <tr>
					    <td id="cottageRow">
					        <img class="buildings" src="Images/Cottage.png" />
                            <p>Cottage: 1 wood, 1 wool, 1 straw</p>
						    <asp:Button runat="server" text="Buy" OnClick="buyCottage_Click" class="btn btn-default" id="buyCottage"></asp:Button><br />
					        <div>You have: <asp:Label id="cottageNo" runat="server" ></asp:Label> cottages</div>
                            <asp:HiddenField ID="hiddenCottageNo" runat="server" />
                        </td>
				    </tr>
                    <tr>
					    <td id="millRow">
					        <img class="buildings" src="Images/Mill.png" />
                            <p>Mill: 2 food, 3 wood, 2 straw, 1 wool</p>
						    <asp:Button runat="server" OnClick="buyMill_Click" text="Buy" class="btn btn-default" id="buyMill"></asp:Button>
                            <div>You have: <asp:Label id="millNo" runat="server"></asp:Label> mills</div>
					    </td>
				    </tr>
                    <tr>
					    <td id="forgeRow">
					        <img class="buildings" src="Images/forge.png" />
                            <p>Forge: 3 stone, 2 food, 2 iron</p>
						    <asp:Button runat="server" OnClick="buyForge_Click" text="Buy" class="btn btn-default" id="buyForge"></asp:Button>
					        <div>You have: <asp:Label id="forgeNo" runat="server"></asp:Label> forges</div>
                        </td>
				    </tr>
                    <tr>
					    <td id="farmRow">
					        <img class="buildings" src="Images/Farm.png" />
                            <p>Farm: 3 food, 2 straw, 2 clay, 1 wood</p>
						    <asp:Button runat="server" OnClick="buyFarm_Click" text="Buy" class="btn btn-default" id="buyFarm"></asp:Button>
					        <div>You have: <asp:Label id="farmNo" runat="server"></asp:Label> farms</div>
                        </td>
				    </tr>
                    <tr>
					    <td id="townhallRow">
					        <img class="buildings" src="Images/TownHall.png" />
                            <p>Town Hall: 40 gold, 5 clay, 5 food, 5 wood</p>
						    <asp:Button runat="server" OnClick="buyTownHall_Click" text="Buy" class="btn btn-default" id="buyTownHall"></asp:Button>
					        <div>You have: <asp:Label id="townHallNo" runat="server"></asp:Label> town halls</div>
                        </td>
				    </tr>
                    <tr>
					    <td id="goldmineRow">
					        <img class="buildings" src="Images/Goldmine.png" />
                            <p>Goldmine: 10 gold, 15 iron, 10 stone, 5 wood</p>
						    <asp:Button runat="server" OnClick="buyGoldmine_Click" text="Buy" class="btn btn-default" id="buyGoldmine"></asp:Button>
					        <div>You have: <asp:Label id="goldMineNo" runat="server"></asp:Label> gold mines</div>
                        </td>
				    </tr>
                    
                    
	  		    </table>
        </div>



      <div class="span10">
                <div class="col-md-6" style="width:70%;">
                    <img src="Images/map.png", style="width:100%;height:100%">
                
                
                <!--<div style="background-image: url(Images/map.png); background-repeat: no-repeat; height: 400px; width: 100%" class="markedImage" id="marked">
                </div>-->
                <div style="width:100%">
                    <asp:Repeater ID="repLocalResources" runat="server">
                        <HeaderTemplate>
                            <table>
                                <thead>
                                    <tr>
                                        <th id="headlineResources">
                                            <h3>Resources</h3>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="dockFloating">
                                <td>
                                    <div class="dockResource">
                                        <img id="<%#Eval("Id")%>" src="<%#Eval("ImageSrc")%>" />
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
                <div class="col col-md-6">
                    <div>
                        <a runat="server" href="~/MarketView" class="horseLink">
                            <img src="Images/Horse.png">
                        </a>
  
        </div>
                    <br />
                    <p>

                    </p>
                    <asp:Repeater ID="repMission" runat="server">
          <HeaderTemplate>
            <table>
              <thead>
                <tr>
                                        <th id="headlineMission">
                                            <h3>Your mission is to build the constructions below (When you build a construction, it will be removed from here)</h3>
                                        </th>
                </tr>
              </thead>
              <tbody>
          </HeaderTemplate>
          <ItemTemplate>
            <tr class="dockFloating">
              <td>
                                    <div class="dockResource">
                                        <img id="<%#Eval("Id")%>" src="<%#Eval("ImageSrc")%>" style="width:100px;height:100px"/>
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
    $(document).ready(function () {
        if ("<%=hasResourcesFor(_02148_Project.Model.Construction.Cottage)%>" == "True") {
            document.getElementById("MainContent_buyCottage").disabled = false;
        } else {
            document.getElementById("MainContent_buyCottage").disabled = true;
        }
        if ("<%=hasResourcesFor(_02148_Project.Model.Construction.Mill)%>" == "True") {
            document.getElementById("MainContent_buyMill").disabled = false;
        } else {
            document.getElementById("MainContent_buyMill").disabled = true;
        }
        if ("<%=hasResourcesFor(_02148_Project.Model.Construction.Farm)%>" == "True") {
            document.getElementById("MainContent_buyFarm").disabled = false;
        } else {
            document.getElementById("MainContent_buyFarm").disabled = true;
        }
        if ("<%=hasResourcesFor(_02148_Project.Model.Construction.Forge)%>" == "True") {
            document.getElementById("MainContent_buyForge").disabled = false;
        } else {
            document.getElementById("MainContent_buyForge").disabled = true;
        }
        if ("<%=hasResourcesFor(_02148_Project.Model.Construction.Goldmine)%>" == "True") {
            document.getElementById("MainContent_buyGoldmine").disabled = false;
        } else {
            document.getElementById("MainContent_buyGoldmine").disabled = true;
        }
        if ("<%=hasResourcesFor(_02148_Project.Model.Construction.Townhall)%>" == "True") {
            document.getElementById("MainContent_buyTownHall").disabled = false;
        } else {
            document.getElementById("MainContent_buyTownHall").disabled = true;
        }
        
    });
    
</script>

</asp:Content>
