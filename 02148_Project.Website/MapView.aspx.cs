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
        public List<MissionGoal> missionGoals;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            playerName.InnerText = MainClient.player.Name;
            goldAmount.InnerText = "You have " + MainClient.player.Gold + " pieces of gold";

            cottageNo.Text = ""+MainClient.player.Cottage;
            millNo.Text = "" + MainClient.player.Mill;
            forgeNo.Text = "" + MainClient.player.Forge;
            townHallNo.Text = "" + MainClient.player.Townhall;
            goldMineNo.Text = "" + MainClient.player.Goldmine;
            farmNo.Text = "" + MainClient.player.Farm;

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
            missionGoals = MainClient.GetMissionGoals();
            repMission.DataSource = missionGoals;
            repMission.DataBind();
            

        }

        protected void buyMill_Click(object sender, EventArgs e)
        {
            MainClient.constructConstruction(Construction.Mill);
            RenderLocalResources();
            millNo.Text = "" + MainClient.player.Mill;
        }

        protected void buyCottage_Click(object sender, EventArgs e)
        {
            MainClient.constructConstruction(Construction.Cottage);
            RenderLocalResources();
            cottageNo.Text = "" + MainClient.player.Cottage;
        }
        protected bool hasResourcesFor(Construction type)
        {
            return MainClient.hasResourcesFor(type);
        }

        protected void buyForge_Click(object sender, EventArgs e)
        {
            MainClient.constructConstruction(Construction.Forge);
            RenderLocalResources();
            forgeNo.Text = "" + MainClient.player.Forge;
        }

        protected void buyTownHall_Click(object sender, EventArgs e)
        {
            MainClient.constructConstruction(Construction.Townhall);
            RenderLocalResources();
            townHallNo.Text = "" + MainClient.player.Townhall;
        }

        protected void buyGoldmine_Click(object sender, EventArgs e)
        {
            MainClient.constructConstruction(Construction.Goldmine);
            RenderLocalResources();
            goldMineNo.Text = "" + MainClient.player.Goldmine;
        }

        protected void buyFarm_Click(object sender, EventArgs e)
        {
            MainClient.constructConstruction(Construction.Farm);
            RenderLocalResources();
            farmNo.Text = "" + MainClient.player.Farm;
        }
    }
}