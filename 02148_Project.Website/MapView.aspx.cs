using _02148_Project.Client;
using _02148_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _02148_Project.Website
{
    public partial class MapView : Page
    {
        public List<LocalResource> localresources;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                localresources = new List<LocalResource>();
                RenderLocalResources();

            }
            else
            {
                if (MainClient.player != null)
                {

                    RenderLocalResources();
                    repLocalResources.DataSource = localresources;
                    repLocalResources.DataBind();

                }

            }

        }


        protected void RenderLocalResources()
        {

            localresources = MainClient.GetLocalResources();
            repLocalResources.DataSource = localresources;
            repLocalResources.DataBind();

        }

        protected void buyMill_Click(object sender, EventArgs e)
        {
            MainClient.constructConstruction(Construction.Mill);
            RenderLocalResources();
        }

        protected void buyCottage_Click(object sender, EventArgs e)
        {
            MainClient.constructConstruction(Construction.Cottage);
            RenderLocalResources();
        }
        protected bool hasResourcesFor(Construction type)
        {
            return MainClient.hasResourcesFor(type);
        }

    }
}