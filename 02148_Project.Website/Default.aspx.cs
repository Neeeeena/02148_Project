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

namespace _02148_Project.Website
{

    public partial class _Default : Page
    {
        public List<LocalResource> localresources;
        public List<ResourceOffer> marketresources;
        public List<TradeOffer> allYourRecievedTradeOffers;
        public List<TradeOffer> allYourSentTradeOffers;
        public Message message;

        public int movedId;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (!Page.IsPostBack)
            {

                //MainClient.deletePlayer("Martin");
                MainClient.ReadAPlayer();
                localresources = new List<LocalResource>();
                marketresources = new List<ResourceOffer>();
                RenderMarket();
                RenderLocalResources();

                MainClient.SetupDatabaseListeners(OnChange_Players, OnChange_ResourceOffer,
                    OnChange_TradeOffer, OnChange_Chat);
            }
            else
            {
                if(MainClient.player != null)
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

        protected void RenderMarket()
        {
            //for (int i = 7; i <= 9; i++)
            //{
            //    marketresources.Add(new ResourceOffer(i, "Nina", ResourceType.Wood, 1, 0));
            //}
            marketresources = MainClient.UpdateResourcesOnMarket();
            repMarketResources.DataSource = marketresources;
            repMarketResources.DataBind();

        }

        protected void RenderLocalResources()
        {
            //Add 5 resources
            //for (int i = 0; i <= 5; i++)
            //{
            //    localresources.Add(new ResourceOffer(i, "Nina", ResourceType.Wood, 1, 0));
            //}
            localresources = MainClient.GetLocalResources();
            repLocalResources.DataSource = localresources;
            repLocalResources.DataBind();

        }


        protected void buttonCancelSell_Click(Object sender, EventArgs e)
        {
            Console.Write("Jeg gør ikke noget :) ");
        }

        protected void buttonConfirmSell_Click(Object sender, EventArgs e)
        {

            var sid = hiddenValue.Value;
            var soldElement = localresources.Find(se => se.Id == sid);
            var newOffer = new ResourceOffer(MainClient.player.Name, soldElement.Type, 1, Int32.Parse(inputPrice.Value));
            MainClient.PlaceResourceOfferOnMarket(newOffer);
            RenderLocalResources();
            RenderMarket();



        }

        protected void submitName_Click(object sender, EventArgs e)
        {
            var name = nameInput.Value;
            MainClient.createPlayer(name);
            RenderLocalResources();
            RenderMarket();
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
            RenderMarket();
            marketresources = MainClient.UpdateResourcesOnMarket();

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
    }
}