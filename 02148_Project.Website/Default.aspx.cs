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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitusername_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(username.Value))
            {
                userwarning.Visible = true;
                userwarning.InnerText = "A player name must be typed in";
            }
            else
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
            

        }
        //Muligvis ud
        protected void submitexistingusername_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(existingusername.Value))
            {
                existinguserwarning.Visible = true;
                existinguserwarning.InnerText = "A player name must be typed in";
            }
            else
            {
                try
                {
                    MainClient.ReadAPlayer(existingusername.Value);
                    Response.Redirect("~/MarketView?player="+existingusername.Value);
                }
                catch (Exception ex)
                {
                    if (ex is PlayerException)
                    {
                        existinguserwarning.Visible = true;
                        existinguserwarning.InnerText = "Player name doesnt exist";
                    }

                }
            }
        }

    }
}