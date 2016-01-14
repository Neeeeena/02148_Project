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
            try
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
    }
}