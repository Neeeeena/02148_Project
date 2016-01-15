<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapView.aspx.cs" Inherits="_02148_Project.Website.MapView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<a runat="server" href="~/MarketView" class="horseLink"><img src="Images/Horse.png" /></a>
<div class="container-fluid">
  <div class="row-fluid">
          <div class="span3">
                <table class="table table-hover">
				    <tr>
					    <td id="cottageRow">
					        <img class="buildings" src="Images/Cottage.png" />
                            <p>Cottage: 1 wood, 1 wool, 1 straw</p>
						    <asp:Button runat="server" text="Buy" OnClick="buyCottage_Click" OnClientClick="return cottage();" class="btn btn-default" id="buyCottage"></asp:Button>
					    </td>
				    </tr>
                    <tr>
					    <td id="millRow">
					        <img class="buildings" src="Images/Mill.png" />
                            <p>Mill: 2 food, 3 wood, 2 straw, 1 wool</p>
						    <asp:Button runat="server" OnClick="buyMill_Click" text="Buy" class="btn btn-default" id="buyMill"></asp:Button>
					    </td>
				    </tr>
                    <tr>
					    <td id="forgeRow">
					        <img class="buildings" src="Images/forge.png" />
                            <p>Forge: 3 stone, 2 food, 2 iron</p>
						    <asp:Button runat="server" OnClick="buyForge_Click" text="Buy" class="btn btn-default" id="buyForge"></asp:Button>
					    </td>
				    </tr>
                    <tr>
					    <td id="farmRow">
					        <img class="buildings" src="Images/Farm.png" />
                            <p>Farm: 3 food, 2 straw, 2 clay, 1 wood</p>
						    <asp:Button runat="server" OnClick="buyFarm_Click" text="Buy" class="btn btn-default" id="buyFarm"></asp:Button>
					    </td>
				    </tr>
                    <tr>
					    <td id="townhallRow">
					        <img class="buildings" src="Images/TownHall.png" />
                            <p>Town Hall: 40 gold, 5 clay, 5 food, 5 wood</p>
						    <asp:Button runat="server" OnClick="buyTownHall_Click" text="Buy" class="btn btn-default" id="buyTownHall"></asp:Button>
					    </td>
				    </tr>
                    <tr>
					    <td id="goldmineRow">
					        <img class="buildings" src="Images/Goldmine.png" />
                            <p>Goldmine: 10 gold, 15 iron, 10 stone, 5 wood</p>
						    <asp:Button runat="server" OnClick="buyGoldmine_Click" text="Buy" class="btn btn-default" id="buyGoldmine"></asp:Button>
					    </td>
				    </tr>
                    
                    
	  		    </table>
        </div>

      <div class="span10">
        <div style="background-image:url(Images/map.png); background-repeat:no-repeat; height: 600px;width: 880px" class="markedImage" id="marked" >
   
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
            <tr class="dockFloating">
              <td>
                  <div class="dockResource" >
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
    </div>

<script type="text/javascript">
    function cottage(){
        alert("You bought a cottage");
    }
</script>

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
