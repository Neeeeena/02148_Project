﻿using System;
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
using Microsoft.AspNet.SignalR;
using System.Web.Services;
using System.Text;
using System.Web.Script.Serialization;

namespace _02148_Project.Website
{

    public partial class MarketView : Page
    {
        public List<LocalResource> localresources;
        public List<ResourceOffer> marketresources;
        public List<TradeOffer> allYourRecievedTradeOffers;
        public List<TradeOffer> allYourSentTradeOffers;
        public List<Message> messages = new List<Message>();
        public List<Message> messagesToAll = new List<Message>();
        public List<Message> messagesP1 = new List<Message>();
        public List<Message> messagesP2 = new List<Message>();
        public List<Message> messagesP3 = new List<Message>();
        public Player Player1;
        public Player Player2;
        public Player Player3;

        public int movedId;
        public int test;

        public bool hasGottenMission = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            playerName.InnerText = MainClient.player.Name;
            goldAmount.InnerText = "You have "+MainClient.player.Gold + " pieces of gold";

            if (!Page.IsPostBack)
            {
                localresources = new List<LocalResource>();
                marketresources = new List<ResourceOffer>();



                MainClient.SetupDatabaseListeners(OnChange_Players, OnChange_ResourceOffer,
                    OnChange_TradeOffer, OnChange_Chat);
            }
            //RegisterAsyncTask(new PageAsyncTask(GetResourceOfferEvent));
            //Load data into allOtherPlayers list
            MainClient.ReadOtherPlayers();

            Player1 = MainClient.allOtherPlayers[0];
            player1Tab.InnerText = Player1.Name;

            Player2 = MainClient.allOtherPlayers[1];
            player2Tab.InnerText = Player2.Name;

            Player3 = MainClient.allOtherPlayers[2];
            player3Tab.InnerText = Player3.Name;

            List<string> names = new List<string>();
            foreach (Player player in MainClient.allOtherPlayers)
            {
                if (player.Name != "Server")
                {
                    names.Add(player.Name);
                }
            }
            tradeOfferReceiver.DataSource = names;
            tradeOfferReceiver.DataBind();

            allYourRecievedTradeOffers = MainClient.ReadAllTradeOffersForYou();
            fillTradeOffers();
            RenderTradeOffers();
            //createTradeOfferElements();

            RenderMarket();
            RenderLocalResources();
            RenderChat();
            if (MainClient.player.Name == "Alex") MainServer.initGame();
            if (!MainClient.incomeTimerHasBeenSet) MainClient.incomeHandler();
            //if (!hasGottenMission)
            //{
            //    hasGottenMission = true;
            //    MainClient.GiveMission();
            //}
        }

        protected override void OnInit(EventArgs e)
        {
            // code before base oninit
            base.OnInit(e);
            // code after base oninit
        }

        protected void sendTradeOffer_click(Object sender, EventArgs e)
        {
            Dictionary<ResourceType, int> sell = new Dictionary<ResourceType, int>();

            sell.Add(ResourceType.Wood, Int32.Parse(woodOffer.Value));
            sell.Add(ResourceType.Wool, Int32.Parse(woolOffer.Value));
            sell.Add(ResourceType.Clay, Int32.Parse(clayOffer.Value));
            sell.Add(ResourceType.Stone, Int32.Parse(stoneOffer.Value));
            sell.Add(ResourceType.Straw, Int32.Parse(strawOffer.Value));
            sell.Add(ResourceType.Iron, Int32.Parse(ironOffer.Value));
            sell.Add(ResourceType.Food, Int32.Parse(foodOffer.Value));

            Dictionary<ResourceType, int> buy = new Dictionary<ResourceType, int>();
            buy.Add(ResourceType.Wood, Int32.Parse(woodReceive.Value));
            buy.Add(ResourceType.Wool, Int32.Parse(woolReceive.Value));
            buy.Add(ResourceType.Clay, Int32.Parse(clayReceive.Value));
            buy.Add(ResourceType.Stone, Int32.Parse(stoneReceive.Value));
            buy.Add(ResourceType.Straw, Int32.Parse(strawReceive.Value));
            buy.Add(ResourceType.Iron, Int32.Parse(ironReceive.Value));
            buy.Add(ResourceType.Food, Int32.Parse(foodReceive.Value));
            TradeOffer to = new TradeOffer(MainClient.player.Name, tradeOfferReceiver.SelectedValue, sell, buy);
            MainClient.SendTradeOfferToPlayer(to);
        }
    
        protected void fillTradeOffers()
        {
            var values = Enum.GetValues(typeof(ResourceType));
            foreach (ResourceType v in values)
            {
                //SellerResources.Add(v,0);
                //ReceiverResources.Add(v,0);
            }
        }

        protected void RenderTradeOffers()
        {
            tradeOfferRepeater.DataSource = allYourRecievedTradeOffers;
            tradeOfferRepeater.DataBind();

            //List<TradeOffer> tradeOffersO = MainClient.ReadAllTradeOffersForYou();
            //tradeOffers.Controls.Clear();
            //foreach(var t in tradeOffersO)
            //{
            //    System.Web.UI.HtmlControls.HtmlGenericControl div = createDiv(t);
            //    tradeOffers.Controls.Add(div);
            //}
        }

        protected void TradeOfferRepeater_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                // Grab all your controls like this
                Repeater sellerRepeater = e.Item.FindControl("tradeSellerResources") as Repeater;
                Repeater receiverRepeater = e.Item.FindControl("tradeReceiverResources") as Repeater;
                // Get the current data
                var data = (TradeOffer)e.Item.DataItem;
                //Bind the values
                sellerRepeater.DataSource = data.SellerResources.ToList();
                sellerRepeater.DataBind();
                receiverRepeater.DataSource = data.ReceiverResources.ToList();
                receiverRepeater.DataBind();
            }
        }

        //protected HtmlGenericControl createDiv(TradeOffer to)
        //{
        //    System.Web.UI.HtmlControls.HtmlGenericControl tradeOffer = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        //    System.Web.UI.HtmlControls.HtmlGenericControl sellerName = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        //    sellerName.InnerHtml = "<b>Seller: " + to.SellerName + "</b>";
        //    System.Web.UI.HtmlControls.HtmlGenericControl resP = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        //    resP.InnerHtml = "<b>You will receive:</b> ";
        //    tradeOffer.Controls.Add(sellerName);
        //    tradeOffer.Controls.Add(resP);
        //    tradeOffer.Attributes.Add("class","tradeOffer");
            

        //    Dictionary<ResourceType, int> resources = to.SellerResources;
            

        //    foreach (KeyValuePair<ResourceType, int> r in resources)
        //    {
        //        ResourceType res = r.Key;
        //        int numb = r.Value;
        //        HtmlImage image = new HtmlImage { Src = res.GetImageSrc() };
        //        tradeOffer.Controls.Add(image);
        //        System.Web.UI.WebControls.Label numberOfRes = new System.Web.UI.WebControls.Label();
        //        numberOfRes.Text = numb.ToString() ;
        //        tradeOffer.Controls.Add(numberOfRes);
        //    }

        //    System.Web.UI.HtmlControls.HtmlGenericControl priceP = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        //    priceP.InnerHtml = "<b>You will have to pay:</b> ";
        //    tradeOffer.Controls.Add(priceP);
        //    Dictionary<ResourceType, int> price = to.ReceiverResources;

        //    foreach (KeyValuePair<ResourceType, int> r in price)
        //    {
        //        ResourceType res = r.Key;
        //        int numb = r.Value;
        //        HtmlImage image = new HtmlImage { Src = res.GetImageSrc() };
        //        tradeOffer.Controls.Add(image);
        //        System.Web.UI.WebControls.Label numberOfRes = new System.Web.UI.WebControls.Label();
        //        numberOfRes.Text = numb.ToString();
        //        tradeOffer.Controls.Add(numberOfRes);
        //    }

        //    System.Web.UI.HtmlControls.HtmlButton acceptButton = new System.Web.UI.HtmlControls.HtmlButton();
        //    acceptButton.InnerText = "Accept";
        //    acceptButton.ID = "a" + to.Id.ToString();
        //    acceptButton.Attributes.Add("runat", "server");
        //    acceptButton.ServerClick += new EventHandler(acceptTradeOffer_click);
        //    acceptButton.Attributes.Add("class", "btn btn-default");
        //    tradeOffer.Controls.Add(acceptButton);

        //    System.Web.UI.HtmlControls.HtmlButton declineButton = new System.Web.UI.HtmlControls.HtmlButton();
        //    declineButton.InnerText = "Decline";
        //    declineButton.ID = "d" + to.Id.ToString();
        //    declineButton.Attributes.Add("runat", "server");
        //    declineButton.ServerClick += new EventHandler(declineTradeOffer_click);
        //    declineButton.Attributes.Add("class", "btn btn-default");
        //    tradeOffer.Controls.Add(declineButton);

        //    return tradeOffer;
            
        //    //tradeOffer.Attributes.Add("class", "tradeOffer");
        //    //tradeOffer.Style.Add(HtmlTextWriterStyle.BackgroundColor, "gray");
        //    //createDiv.Style.Add(HtmlTextWriterStyle.Color, "Red");
        //    //createDiv.Style.Add(HtmlTextWriterStyle.Height, "100px");
        //    //createDiv.Style.Add(HtmlTextWriterStyle.Width, "400px");
        //    //createDiv.InnerHtml = " I'm a div, from code behind ";
        //    //tradeOffers.Controls.Add(tradeOffer);
        //}

      

        protected void acceptTradeOffer_click(Object sender, EventArgs e)
        {
            //Button btnTrade = (Button)sender;
            //RepeaterItem item = (RepeaterItem)btnTrade.NamingContainer;
            //var id = item.Controls[0].ID;
            //SNyd
            allYourRecievedTradeOffers = DatabaseInterface.ReadAllTradeOffers(MainClient.player.Name);
            MainClient.AcceptTradeOffer(allYourRecievedTradeOffers.ElementAt(0).Id);
            RenderTradeOffers();
        }

        protected void declineTradeOffer_click(Object sender, EventArgs e)
        {
            //MainClient.DeclineTradeOffer(Int32.Parse((string)e.CommandArgument));
            //MainClient.AcceptTradeOffer(Int32.Parse(tradeOfferId.Value));
            allYourRecievedTradeOffers = DatabaseInterface.ReadAllTradeOffers(MainClient.player.Name);
            MainClient.AcceptTradeOffer(allYourRecievedTradeOffers.ElementAt(0).Id);
            RenderTradeOffers();
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
            messages.OrderBy(a => a.Id).ToList();
            messages.Reverse();
            foreach(var mes in messages)
            {
                if (mes.ToAll)
                {
                    if (mes.SenderName.Equals(MainClient.player.Name))
                    {
                        mes.htmlClass = "myMessage";
                    }
                    messagesToAll.Add(mes);
                }
                else
                {
                    if(mes.RecieverName.Equals(Player1.Name) || mes.SenderName.Equals(Player1.Name))
                    {
                        if (mes.SenderName.Equals(MainClient.player.Name))
                        {
                            mes.htmlClass = "myMessage";
                        }
                        messagesP1.Add(mes);
                    }
                    else if(mes.RecieverName.Equals(Player2.Name) || mes.SenderName.Equals(Player2.Name)){
                        if (mes.SenderName.Equals(MainClient.player.Name))
                        {
                            mes.htmlClass = "myMessage";
                        }
                        messagesP2.Add(mes);
                    }
                    else
                    {
                        if (mes.SenderName.Equals(MainClient.player.Name))
                        {
                            mes.htmlClass = "myMessage";
                        }
                        messagesP3.Add(mes);
                    }
                }
            }
            repAllChat.DataSource = messagesToAll;
            repAllChat.DataBind();
            repP1Chat.DataSource = messagesP1;
            repP1Chat.DataBind();
            repP2Chat.DataSource = messagesP2;
            repP2Chat.DataBind();
            repP3Chat.DataSource = messagesP3;
            repP3Chat.DataBind();

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
            RenderTradeOffers();
            DatabaseInterface.MonitorTradeOffer(OnChange_TradeOffer);
        }

        public void OnChange_Chat(object sender, SqlNotificationEventArgs e)
        {
            (sender as SqlDependency).OnChange -= OnChange_Chat;
            // Get the latest message and save it locally
            
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
            goldAmount.InnerText = "You have " + MainClient.player.Gold + " pieces of gold";
             
            if (returnedMessage != "")
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
            allMsg.Value = "";
          //  RenderChat();
            

        }

        protected void btnSendToPlayer1_Click(object sender, EventArgs e)
        {
            var message = p1Msg.Value;
            var messageObject = new Message(message, MainClient.player.Name, Player1.Name);
            MainClient.SendNewMessage(messageObject);
            p1Msg.Value = "";
           // RenderChat();
            
        }

        protected void btnSendToPlayer2_Click(object sender, EventArgs e)
        {
            var message = p2Msg.Value;
            var messageObject = new Message(message, MainClient.player.Name, Player2.Name);
            MainClient.SendNewMessage(messageObject);
            p2Msg.Value = "";
            //RenderChat();
            
        }
        protected void btnSendToPlayer3_Click(object sender, EventArgs e)
        {
            var message = p3Msg.Value;
            var messageObject = new Message(message, MainClient.player.Name, Player3.Name);
            MainClient.SendNewMessage(messageObject);
            p3Msg.Value = "";
            //RenderChat();
            
        }

    }
}