using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _02148_Project.Model;
using _02148_Project.Client;

namespace _02148_Project.Website
{

    public partial class _Default : Page
    {
        public List<LocalResource> localresources;
        public List<ResourceOffer> marketresources;
        public int movedId;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (!Page.IsPostBack)
            {

                //MainClient.deletePlayer("Martin");
                MainClient.ReadAPlayer();
                localresources = new List<LocalResource>();
                marketresources = new List<ResourceOffer>();
               // RenderMarket();
                //RenderLocalResources();

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
    }
}