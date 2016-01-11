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
        public List<ResourceOffer> localresources;
        public List<ResourceOffer> marketresources;
        public MainClient mc;
        public int movedId;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (!Page.IsPostBack)
            {
                mc = new MainClient();
                localresources = new List<ResourceOffer>();
                marketresources = new List<ResourceOffer>();
                RenderMarket();
                RenderLocalResources();

            }
            else
            {
                repMarketResources.DataSource = marketresources;
                repMarketResources.DataBind();
                repLocalResources.DataSource = localresources;
                repLocalResources.DataBind();
            }

        }

        protected void RenderMarket()
        {
            //for (int i = 7; i <= 9; i++)
            //{
            //    marketresources.Add(new ResourceOffer(i, "Nina", ResourceType.Wood, 1, 0));
            //}
            marketresources = mc.UpdateResourcesOnMarket();
            repMarketResources.DataSource = marketresources;
            repMarketResources.DataBind();

        }

        protected void RenderLocalResources()
        {
            //Add 5 resources
            for (int i = 0; i <= 5; i++)
            {
                localresources.Add(new ResourceOffer(i, "Nina", ResourceType.Wood, 1, 0));
            }

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
            var soldElement = localresources.Find(se => se.Id == Int32.Parse(sid));
            soldElement.Price = Int32.Parse(inputPrice.Value);
            localresources.Remove(soldElement);
            mc.PlaceResourceOfferOnMarket(soldElement);
            marketresources.Add(soldElement);
            repLocalResources.DataSource = localresources;
            repLocalResources.DataBind();
            repMarketResources.DataSource = marketresources;
            repMarketResources.DataBind();



        }
    }
}