using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _02148_Project.Model;

namespace _02148_Project.Website
{

    public partial class _Default : Page
    {
        List<ResourceOffer> resources;
        protected void Page_Load(object sender, EventArgs e)
        {
            resources = new List<ResourceOffer>();

            RenderMarket();
        }

        protected void RenderMarket()
        {
            //Add 5 resources
            for (int i = 1; i <= 5; i++)
            {
                resources.Add(new ResourceOffer() { Id = i });
            }
            localResources.DataSource = resources;
            localResources.DataBind();

        }
    }
}