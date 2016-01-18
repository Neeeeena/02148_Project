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

        public int movedId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //MainClient.deletePlayer("Martin");
                localresources = new List<LocalResource>();
                marketresources = new List<ResourceOffer>();
                RenderMarket();
                RenderLocalResources();
                RenderTradeOffers();

                MainClient.SetupDatabaseListeners(OnChange_Players, OnChange_ResourceOffer,
                    OnChange_TradeOffer, OnChange_Chat);
            }
            else
            {
                if (MainClient.player != null)
                {
                    RenderMarket();
                    RenderLocalResources();
                    repMarketResources.DataSource = marketresources;
                    repMarketResources.DataBind();
                    repLocalResources.DataSource = localresources;
                    repLocalResources.DataBind();
                }
            }



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

            Dictionary<ResourceType, int> resources = to.resources;
            Dictionary<ResourceType, int> price = to.price;

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

            return tradeOffer;
            
            //tradeOffer.Attributes.Add("class", "tradeOffer");
            //tradeOffer.Style.Add(HtmlTextWriterStyle.BackgroundColor, "gray");
            //createDiv.Style.Add(HtmlTextWriterStyle.Color, "Red");
            //createDiv.Style.Add(HtmlTextWriterStyle.Height, "100px");
            //createDiv.Style.Add(HtmlTextWriterStyle.Width, "400px");
            //createDiv.InnerHtml = " I'm a div, from code behind ";
            //tradeOffers.Controls.Add(tradeOffer);
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
    }
}