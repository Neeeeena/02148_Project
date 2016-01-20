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
        public List<Message> messages;
        public Player Player1;
        public Player Player2;
        public Player Player3;

        public int movedId;
        public int test;
        protected void Page_Load(object sender, EventArgs e)
        {

            playerName.InnerText = MainClient.player.Name;
            goldAmount.InnerText = "You have "+MainClient.player.Gold + " pieces of gold";

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

            
            dd.Items[0].Text = Player1.Name;
            dd.Items[1].Text = Player2.Name;
            dd.Items[1].Text = Player3.Name;

            fillTradeOffers();
            //createTradeOfferElements();

            RenderMarket();
            RenderLocalResources();
            repMarketResources.DataSource = marketresources;
            repMarketResources.DataBind();
            repLocalResources.DataSource = localresources;
            repLocalResources.DataBind();
            RenderChat();
            MainServer.initGame();
        }


        protected override void OnInit(EventArgs e)
        {
            // code before base oninit
            base.OnInit(e);
            RenderTradeOffers();
            // code after base oninit
        }

        //protected void createTradeOfferElements()
        //{

        //    System.Web.UI.HtmlControls.HtmlGenericControl tradeOfferSeller = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        //    System.Web.UI.HtmlControls.HtmlGenericControl labelSeller = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        //    labelSeller.InnerHtml = "Resources you want to sell: ";
        //    tradeOfferSeller.Controls.Add(labelSeller);
        //    foreach (KeyValuePair<ResourceType, int> r in SellerResources)
        //    {
        //        ResourceType res = r.Key;
        //        int numb = r.Value;
        //        HtmlImage image = new HtmlImage { Src = res.GetImageSrc() };
        //        image.Attributes.Add("runat", "server");
        //        image.Attributes.Add("onclick", "tradeOfferSellerImage_click");
        //        tradeOfferSeller.Controls.Add(image);
        //        System.Web.UI.WebControls.Label numberOfRes = new System.Web.UI.WebControls.Label();
        //        numberOfRes.Text = numb.ToString();
        //        numberOfRes.ID = "S" + res;
        //        tradeOfferSeller.Controls.Add(numberOfRes);
        //    }



        //    System.Web.UI.HtmlControls.HtmlGenericControl tradeOfferReceiver = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        //    System.Web.UI.HtmlControls.HtmlGenericControl labelReceiver = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        //    labelReceiver.InnerHtml = "Resources you want to receive: ";
        //    tradeOfferReceiver.Controls.Add(labelReceiver);
        //    foreach (KeyValuePair<ResourceType, int> r in ReceiverResources)
        //    {
        //        ResourceType res = r.Key;
        //        int numb = r.Value;
        //        HtmlImage image = new HtmlImage { Src = res.GetImageSrc() };
        //        image.ID = "" + res;
        //        tradeOfferReceiver.Controls.Add(image);
        //        System.Web.UI.WebControls.Label numberOfRes = new System.Web.UI.WebControls.Label();
        //        numberOfRes.Text = numb.ToString();
        //        numberOfRes.ID = "R" + res;
        //        tradeOfferReceiver.Controls.Add(numberOfRes);
        //    }

        //}

        //protected void tradeOfferSellerImage0_click(Object sender, EventArgs e)
        //{
        //    ls0.InnerHtml = (Int32.Parse(ls0.InnerHtml) + 1).ToString();
        //}

        //protected void tradeOfferSellerImage1_click(Object sender, EventArgs e)
        //{
        //    ls1.InnerHtml = (Int32.Parse(ls1.InnerHtml) + 1).ToString();
        //}

        //protected void tradeOfferSellerImage2_click(Object sender, EventArgs e)
        //{
        //    ls2.InnerHtml = (Int32.Parse(ls2.InnerHtml) + 1).ToString();
        //}

        //protected void tradeOfferSellerImage3_click(Object sender, EventArgs e)
        //{
        //    ls3.InnerHtml = (Int32.Parse(ls3.InnerHtml) + 1).ToString();
        //}

        //protected void tradeOfferSellerImage4_click(Object sender, EventArgs e)
        //{
        //    ls4.InnerHtml = (Int32.Parse(ls4.InnerHtml) + 1).ToString();
        //}

        protected void tradeOfferReceiverImage0_click(Object sender, EventArgs e)
        {
            lr0.InnerHtml = (Int32.Parse(lr0.InnerHtml) + 1).ToString();
        }

        protected void tradeOfferReceiverImage1_click(Object sender, EventArgs e)
        {
            lr1.InnerHtml = (Int32.Parse(lr1.InnerHtml) + 1).ToString();
        }

        protected void tradeOfferReceiverImage2_click(Object sender, EventArgs e)
        {
            lr2.InnerHtml = (Int32.Parse(lr2.InnerHtml) + 1).ToString();
        }

        protected void tradeOfferReceiverImage3_click(Object sender, EventArgs e)
        {
            lr3.InnerHtml = (Int32.Parse(lr3.InnerHtml) + 1).ToString();
        }

        protected void tradeOfferReceiverImage4_click(Object sender, EventArgs e)
        {
            lr4.InnerHtml = (Int32.Parse(lr4.InnerHtml) + 1).ToString();
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

        protected void sendTradeOffer_click(Object sender, EventArgs e)
        {

        }

        protected void RenderTradeOffers()
        {
            List<TradeOffer> tradeOffersO = MainClient.ReadAllTradeOffersForYou();
            tradeOffers.Controls.Clear();
            foreach(var t in tradeOffersO)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl div = createDiv(t);
                tradeOffers.Controls.Add(div);
            }
        }
        protected HtmlGenericControl createDiv(TradeOffer to)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl tradeOffer = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            System.Web.UI.HtmlControls.HtmlGenericControl sellerName = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
            sellerName.InnerHtml = "<b>Seller: " + to.SellerName + "</b>";
            System.Web.UI.HtmlControls.HtmlGenericControl resP = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
            resP.InnerHtml = "<b>You will receive:</b> ";
            tradeOffer.Controls.Add(sellerName);
            tradeOffer.Controls.Add(resP);
            tradeOffer.Attributes.Add("class","tradeOffer");
            

            Dictionary<ResourceType, int> resources = to.SellerResources;
            

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

            System.Web.UI.HtmlControls.HtmlGenericControl priceP = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
            priceP.InnerHtml = "<b>You will have to pay:</b> ";
            tradeOffer.Controls.Add(priceP);
            Dictionary<ResourceType, int> price = to.ReceiverResources;

            foreach (KeyValuePair<ResourceType, int> r in price)
            {
                ResourceType res = r.Key;
                int numb = r.Value;
                HtmlImage image = new HtmlImage { Src = res.GetImageSrc() };
                tradeOffer.Controls.Add(image);
                System.Web.UI.WebControls.Label numberOfRes = new System.Web.UI.WebControls.Label();
                numberOfRes.Text = numb.ToString();
                tradeOffer.Controls.Add(numberOfRes);
            }

            System.Web.UI.HtmlControls.HtmlButton acceptButton = new System.Web.UI.HtmlControls.HtmlButton();
            acceptButton.InnerText = "Accept";
            acceptButton.ID = "a" + to.Id.ToString();
            acceptButton.Attributes.Add("runat", "server");
            acceptButton.ServerClick += new EventHandler(acceptTradeOffer_click);
            acceptButton.Attributes.Add("class", "btn btn-default");
            tradeOffer.Controls.Add(acceptButton);

            System.Web.UI.HtmlControls.HtmlButton declineButton = new System.Web.UI.HtmlControls.HtmlButton();
            declineButton.InnerText = "Decline";
            declineButton.ID = "d" + to.Id.ToString();
            declineButton.Attributes.Add("runat", "server");
            acceptButton.ServerClick += new EventHandler(declineTradeOffer_click);
            declineButton.Attributes.Add("class", "btn btn-default");
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
            HtmlButton button = (HtmlButton)sender;
            MainClient.AcceptTradeOffer(Int32.Parse(button.ID.Remove(0,1)));
            RenderTradeOffers();
        }

        protected void declineTradeOffer_click(Object sender, EventArgs e)
        {
            HtmlButton button = (HtmlButton)sender;
            MainClient.DeclineTradeOffer(Int32.Parse(button.ID.Remove(0, 1)));
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
        [WebMethod]
        public static string ReturnMessages()
        {
            //messages = MainClient.GetNewMessage();
            ////StringBuilder sb = new StringBuilder();
            ////foreach(var m in messages)
            ////{
            ////    sb.Append(m.Content);
            ////}
            //HtmlGenericControl divcontrol = new HtmlGenericControl();
            //divcontrol.TagName = "div";
            //Label l = new Label();
            //l.Text = messages.ElementAt(messages.Count - 1).Content;
            //divcontrol.Controls.Add(l);
            //return divcontrol;
            return "Hejsa";
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
                    if (mes.RecieverName != null)
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
            RenderTradeOffers();
            DatabaseInterface.MonitorTradeOffer(OnChange_TradeOffer);
        }

        public void OnChange_Chat(object sender, SqlNotificationEventArgs e)
        {
            (sender as SqlDependency).OnChange -= OnChange_Chat;
            // Get the latest message and save it locally

            ChatHub.SendMessages();
        
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