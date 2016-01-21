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
    public partial class Party : System.Web.UI.Page
    {

        List<Player> players { get; set; }
        Player player { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            renderOnlinePlayers();
        }

        protected void timer_Ticked(object sender, EventArgs e)
        {

        }

        protected void renderOnlinePlayers()
        {
            players = DatabaseInterface.ReadAllPlayers();
            players.Remove(players.Find(x => x.Name == "Server"));
            if (players.Count >= 4)
            {
                Response.Redirect("~/MarketView?player=" + MainClient.player.Name);
            }
            repOnlinePlayers.DataSource = players;
            repOnlinePlayers.DataBind();
        }
    }
}