using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _02148_Project.Model;
using _02148_Project.Client;
using _02148_Project;
using System.Data.SqlClient;
using System.Web.Caching;
using _02148_Project.Model.Exceptions;
using System.Web.UI.HtmlControls;

namespace _02148_Project.Website
{

    public partial class MarketView : Page
    {
        public List<LocalResource> localresources;
        public List<ResourceOffer> marketresources;
        public List<TradeOffer> allYourRecievedTradeOffers;
        public List<TradeOffer> allYourSentTradeOffers;
        public List<Message> messages;
        public Player Player1;
        public Player Player2;
        public Player Player3;

        public int movedId;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                localresources = new List<LocalResource>();
                marketresources = new List<ResourceOffer>();
                messages = new List<Message>();

                MainClient.SetupDatabaseListeners(OnChange_Players, OnChange_ResourceOffer,
                    OnChange_TradeOffer, OnChange_Chat);
            }
            //Load data into allOtherPlayers list
            MainClient.ReadOtherPlayers();

            Player1 = MainClient.allOtherPlayers[0];
            player1Tab.InnerText = Player1.Name;

            Player2 = MainClient.allOtherPlayers[1];
            player2Tab.InnerText = Player2.Name;

            Player3 = MainClient.allOtherPlayers[2];
            player3Tab.InnerText = Player3.Name;

            RenderMarket();
            RenderLocalResources();
            repMarketResources.DataSource = marketresources;
            repMarketResources.DataBind();
            repLocalResources.DataSource = localresources;
            repLocalResources.DataBind();
            RenderChat();

            
            
            



        }

        protected void RenderTradeOffers()
        {
            List<TradeOffer> tradeOffersO = MainClient.ReadAllTradeOffersForYou();
            foreach(var t in tradeOffersO)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl div = createDiv(t);
                tradeOffers.Controls.Add(div);
            }
        }

        protected HtmlGenericControl createDiv(TradeOffer to)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl tradeOffer = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            System.Web.UI.WebControls.Label sellerName = new System.Web.UI.WebControls.Label();
            sellerName.Text = to.SellerName;
            System.Web.UI.WebControls.Label receiverName = new System.Web.UI.WebControls.Label();
            receiverName.Text = to.RecieverName;
            tradeOffer.Controls.Add(sellerName);
            tradeOffer.Controls.Add(receiverName);
            tradeOffer.Attributes.Add("class","tradeOffer");

            Dictionary<ResourceType, int> resources = to.SellerResources;
            Dictionary<ResourceType, int> price = to.ReceiverResources;

            foreach (KeyValuePair<ResourceType, int> r in resources)
            {
                ResourceType res = r.Key;
                int numb = r.Value;
                HtmlImage image = new HtmlImage { Src = res.GetImageSrc() };
                tradeOffer.Controls.Add(image);
                System.Web.UI.WebControls.Label numberOfRes = new System.Web.UI.WebControls.Label();
                numberOfRes.Text = numb.ToString() ;
                tradeOffer.Controls.Add(numberOfRes);
            }

            System.Web.UI.HtmlControls.HtmlButton acceptButton = new System.Web.UI.HtmlControls.HtmlButton();
            acceptButton.InnerText = "Accept";
            acceptButton.ID = to.Id.ToString();
            acceptButton.Attributes.Add("runat", "server");
            acceptButton.Attributes.Add("onclick", "acceptTradeOffer_click");
            acceptButton.Attributes.Add("class", "btn btn-default");
            System.Web.UI.HtmlControls.HtmlButton declineButton = new System.Web.UI.HtmlControls.HtmlButton();
            declineButton.InnerText = "Decline";
            declineButton.Attributes.Add("runat", "server");
            declineButton.Attributes.Add("class", "btn btn-default");
            tradeOffer.Controls.Add(acceptButton);
            tradeOffer.Controls.Add(declineButton);

            return tradeOffer;
            
            //tradeOffer.Attributes.Add("class", "tradeOffer");
            //tradeOffer.Style.Add(HtmlTextWriterStyle.BackgroundColor, "gray");
            //createDiv.Style.Add(HtmlTextWriterStyle.Color, "Red");
            //createDiv.Style.Add(HtmlTextWriterStyle.Height, "100px");
            //createDiv.Style.Add(HtmlTextWriterStyle.Width, "400px");
            //createDiv.InnerHtml = " I'm a div, from code behind ";
            //tradeOffers.Controls.Add(tradeOffer);
        }

        protected void acceptTradeOffer_click(Object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MainClient.AcceptTradeOffer(Int32.Parse(button.ID));
        }

        protected void timer_Ticked(object sender, EventArgs e)
        {
        }

        protected void RenderMarket()
        {
            marketresources = MainClient.UpdateResourcesOnMarket();
            repMarketResources.DataSource = marketresources;
            repMarketResources.DataBind();
        }

        protected void RenderLocalResources()
        {
            localresources = MainClient.GetLocalResources();
            repLocalResources.DataSource = localresources;
            repLocalResources.DataBind();
        }

        protected void RenderChat()
        {
            messages = MainClient.GetNewMessage();
            foreach(var mes in messages)
            {
                if (mes.ToAll)
                {
                    Label l = new Label();
                    l.Text = mes.SenderName + ": " + mes.Content;
                    if (mes.SenderName.Equals(MainClient.player.Name))
                    {
                        l.Attributes.Add("Class", "myMessage");
                    }
                    allChat.Controls.Add(l);
                    allChat.Controls.Add(new Literal {Text = "<hr/>" });
                }
                else
                {
                    if(mes.RecieverName.Equals(Player1.Name) || mes.SenderName.Equals(Player1.Name))
                    {
                        Label l = new Label();
                        l.Text = mes.SenderName + ": " + mes.Content;
                        if (mes.SenderName.Equals(MainClient.player.Name))
                        {
                            l.Attributes.Add("Class", "myMessage");
                        }
                        p1Chat.Controls.Add(l);
                        p1Chat.Controls.Add(new Literal { Text = "<hr/>" });
                    }
                    else if(mes.RecieverName.Equals(Player2.Name) || mes.SenderName.Equals(Player2.Name)){
                        Label l = new Label();
                        l.Text = mes.SenderName + ": " + mes.Content;
                        if (mes.SenderName.Equals(MainClient.player.Name))
                        {
                            l.Attributes.Add("Class", "myMessage");
                        }
                        p2Chat.Controls.Add(l);
                        p2Chat.Controls.Add(new Literal { Text = "<hr/>" });
                    }
                    else
                    {
                        Label l = new Label();
                        l.Text = mes.SenderName + ": " + mes.Content;
                        if (mes.SenderName.Equals(MainClient.player.Name))
                        {
                            l.Attributes.Add("Class", "myMessage");
                        }
                        p3Chat.Controls.Add(l);
                        p3Chat.Controls.Add(new Literal {  Text = "<hr/>" });
                    }
                }
            }

           }


        protected void buttonCancelSell_Click(Object sender, EventArgs e)
        {
            Console.Write("Jeg gør ikke noget :) ");
        }

        protected void buttonConfirmSell_Click(Object sender, EventArgs e)
        {
            var sid = hiddenValue.Value;
            var soldElement = localresources.Find(se => se.Id == sid);
            var sellValue = Int32.Parse(inputPrice.Value);
            var newOffer = new ResourceOffer(MainClient.player.Name, soldElement.Type, 1, sellValue);
            newOffer.HighestBid = Int32.Parse(inputPrice.Value);
            MainClient.PlaceResourceOfferOnMarket(newOffer);
            RenderLocalResources();
            RenderMarket();
            inputPrice.Value = "";
        }

        #region DatabaseListeners

        /// <summary>
        /// On change methode for when the players table changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnChange_Players(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;
            dependency.OnChange -= OnChange_Players;

            // Need to update the correct field, not players in this class
            MainClient.ReadOtherPlayers();
            localresources = MainClient.GetLocalResources();

            DatabaseInterface.MonitorPlayers(OnChange_Players);
        }

        public void OnChange_ResourceOffer(object sender, SqlNotificationEventArgs e)
        {
            (sender as SqlDependency).OnChange -= OnChange_ResourceOffer;
            // Find a way to update with the latest resource offers
            RenderLocalResources();
            RenderMarket();

            DatabaseInterface.MonitorResourceOffers(OnChange_ResourceOffer);
        }

        public void OnChange_TradeOffer(object sender, SqlNotificationEventArgs e)
        {
            (sender as SqlDependency).OnChange -= OnChange_TradeOffer;
            // Update all trade offer fields
            if (MainClient.player != null)
            {
                allYourRecievedTradeOffers = DatabaseInterface.ReadAllTradeOffers(MainClient.player.Name);
                allYourSentTradeOffers = DatabaseInterface.ReadAllSendTradeOffers(MainClient.player.Name);
            }
            DatabaseInterface.MonitorTradeOffer(OnChange_TradeOffer);
        }

        public void OnChange_Chat(object sender, SqlNotificationEventArgs e)
        {
            (sender as SqlDependency).OnChange -= OnChange_Chat;
            // Get the latest message and save it locally
            if (MainClient.player != null)
            {
                messages = DatabaseInterface.ReadMessages(MainClient.player.Name);
            }
            DatabaseInterface.MonitorChat(OnChange_Chat);
        }
        #endregion


        protected void submitBid_Click(object sender, EventArgs e)
        {
                string ID = hidId.Value;
                string price = bidPrice.Value;
            int bidValue = Int32.Parse(price);

                ResourceOffer ro = marketresources.Find(x => x.Id == Int32.Parse(ID));
                    ro.HighestBid = bidValue;
                    ro.HighestBidder = MainClient.player.Name;

            string returnedMessage = MainClient.BidOnResource(ro);
             
            if(returnedMessage != "")
            {
                //Message box error
                }
                bidwarning.Visible = false;
                bidPrice.Value = "";
        }

        protected void btnSendToAll_Click(object sender, EventArgs e)
        {
            var message = allMsg.Value;
            var messageObject = new Message(message,MainClient.player.Name,"",true);
            MainClient.SendNewMessage(messageObject);
            RenderChat();
        }

        protected void btnSendToPlayer1_Click(object sender, EventArgs e)
        {
            var message = p1Msg.Value;
            var messageObject = new Message(message, MainClient.player.Name, Player1.Name);
            MainClient.SendNewMessage(messageObject);
            RenderChat();
        }

        protected void btnSendToPlayer2_Click(object sender, EventArgs e)
        {
            var message = p2Msg.Value;
            var messageObject = new Message(message, MainClient.player.Name, Player2.Name);
            MainClient.SendNewMessage(messageObject);
            RenderChat();
        }
        protected void btnSendToPlayer3_Click(object sender, EventArgs e)
        {
            var message = p3Msg.Value;
            var messageObject = new Message(message, MainClient.player.Name, Player3.Name);
            MainClient.SendNewMessage(messageObject);
            RenderChat();
        }

    }
}