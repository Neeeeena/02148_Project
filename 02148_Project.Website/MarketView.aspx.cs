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

namespace _02148_Project.Website
{

    public partial class MarketView : Page
    {
        public List<LocalResource> localresources;
        public List<ResourceOffer> marketresources;
        public List<TradeOffer> allYourRecievedTradeOffers;
        public List<TradeOffer> allYourSentTradeOffers;
        public Message message;
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

                MainClient.SetupDatabaseListeners(OnChange_Players, OnChange_ResourceOffer,
                    OnChange_TradeOffer, OnChange_Chat);
            }
            RenderMarket();
            RenderLocalResources();
            repMarketResources.DataSource = marketresources;
            repMarketResources.DataBind();
            repLocalResources.DataSource = localresources;
            repLocalResources.DataBind();

            //Load data into allOtherPlayers list
            MainClient.ReadOtherPlayers();

            Player1 = MainClient.allOtherPlayers[0];
            player1Tab.InnerText = Player1.Name;

            Player2 = MainClient.allOtherPlayers[1];
            player2Tab.InnerText = Player2.Name;

            Player3 = MainClient.allOtherPlayers[2];
            player3Tab.InnerText = Player3.Name;
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
            List<Message> m = new List<Message>() { new Message("Hej Mathias", "Nina", "Mathias"), new Message("Hej Nina", "Mathias", "Nina") };
            foreach(var mes in m){
                Label l = new Label();
                l.Text = mes.SenderName + ": "+ mes.Context;
                if (mes.SenderName.Equals(MainClient.player.Name))
                {
                    l.Attributes.Add("Class", "myMessage");
                }
                allChat.Controls.Add(l);
                allChat.Controls.Add(new Literal { ID = "row", Text = "<hr/>" });
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

        protected void send_message_btn_click(Object sender, EventArgs e)
        {
            var msg = allMsg.Value;
            //var msgTuple =  new Message(message, )
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
                message = DatabaseInterface.GetMessage(MainClient.player.Name);
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
            var messageObject = new Message(message,MainClient.player.Name,"");
            MainClient.SendNewMessageToAll(messageObject);
        }

        protected void btnSendToPlayer1_Click(object sender, EventArgs e)
        {
            var message = p1Msg.Value;
            var messageObject = new Message(message, MainClient.player.Name, Player1.Name);
            MainClient.SendNewMessage(messageObject);
        }

        protected void btnSendToPlayer2_Click(object sender, EventArgs e)
        {
            var message = p2Msg.Value;
            var messageObject = new Message(message, MainClient.player.Name, Player2.Name);
            MainClient.SendNewMessage(messageObject);
        }
        protected void btnSendToPlayer3_Click(object sender, EventArgs e)
        {
            var message = p3Msg.Value;
            var messageObject = new Message(message, MainClient.player.Name, Player3.Name);
            MainClient.SendNewMessage(messageObject);
        }

    }
}