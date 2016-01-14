<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapView.aspx.cs" Inherits="_02148_Project.Website.MapView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<a runat="server" href="~/" class="horseLink"><img src="Images/Horse.png" /></a>
<div class="container-fluid">
  <div class="row-fluid">
          <div class="span3">
                <table class="table table-hover">
				    <tr>
					    <td id="cottageRow">
					        <img class="buildings" src="Images/Cottage.png" />
                            <p>Cottage: 2 wood, 2 wool, 2 straw</p>
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
            <tr class="floating">
              <td>
                  <div class="resource" >
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
    var lr = <%=localresources%>;

</script>

</asp:Content>
