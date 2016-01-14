<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapView.aspx.cs" Inherits="_02148_Project.Website.MapView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<a runat="server" href="~/" class="horseLink"><img src="Images/Horse.png" /></a>
<div class="container-fluid">
  <div class="row-fluid">
          <div class="span2">
                <table class="table table-hover" style="width:100%">
				    <tr>
					    <td>
					        <img class="buildings" src="Images/Cottage.png" />
                            <p>Cottage: 2 pieces of wood</p>
						    <asp:Button runat="server" text="Buy" class="btn btn-default" id="houseButton">
						    </asp:Button>
					    </td>
				    </tr>
	  		    </table>
        </div>

      <div class="span10">
<div style="background-image:url(Images/map.png); background-repeat:no-repeat; height: 600px;width: 1000px" class="markedImage" id="marked" >
   
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

</asp:Content>
