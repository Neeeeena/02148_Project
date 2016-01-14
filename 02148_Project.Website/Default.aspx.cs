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
using _02148_Project.Model.Exceptions;

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

        }

        protected void submitusername_Click(object sender, EventArgs e)
        {
            string ret = MainClient.createPlayer(username.Value);
            if (ret == null)
            {
                Response.Redirect("~/MarketView");
            }
            else
            {
                userwarning.Visible = true;
                userwarning.InnerText = ret;

            }

        }
        //Muligvis ud
        protected void submitexistingusername_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            try
=======
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
            if (MainClient.createPlayer(name) != null)
>>>>>>> a0b0b4c38c622ea7364de01e17b39feb009e8fcc
            {

                MainClient.ReadAPlayer(existingusername.Value);
                
                Response.Redirect("~/MarketView");
            }
            catch(Exception ex)
            {
                if (ex is PlayerException)
                {
                    existinguserwarning.Visible = true;
                    existinguserwarning.InnerText = "Player name doesnt exist";
                }

            }

        }
<<<<<<< HEAD
=======
        #endregion

        protected void submitBid_Click(object sender, CommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            ResourceOffer ro = marketresources.Find(x => x.Id == Int32.Parse(ID));
            int bidValue = Int32.Parse(bidInput.Value);
            if (bidValue > ro.HighestBid)
            {
                ro.HighestBid = bidValue;
                ro.HighestBidder = MainClient.player.Name;
                MainClient.BidOnResource(ro);
            }
        }
>>>>>>> a0b0b4c38c622ea7364de01e17b39feb009e8fcc
    }
}