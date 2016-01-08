using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02148_Project;
using _02148_Project.Model;

namespace _02148_Project.Client
{
    public class MainClient
    {

        public List<ResourceOffer> allResourcesOnMarket;
        public List<TradeOffer> allTradeOffers;
        public List<Message> collectedMessages = new List<Message>();

        public Player player;


        public void GameSetup()
        {
            //Setup all fields before game, like username, goldamount at start etc.
        }

        public void SetupTimer()
        {
            //Setup timeren/timersne
        }

        //SKAL SÅDAN SET KALDES AF EN HANDLER, BARE LIGE FOR ORDEN I DESIGN OG HVORDAN DER SNAKKES SAMMEN
        
        // Market stuff:

        public void UpdateResourcesOnMarket()
        {
            allResourcesOnMarket = DatabaseInterface.ReadAllResourceOffers();
        }

        public void BidOnResource(ResourceOffer offer)
        {
            DatabaseInterface.UpdateResourceOffer(offer);
        }

        public void PlaceResourceOfferOnMarket(ResourceOffer offer)
        {
            DatabaseInterface.PutResourceOfferOnMarket(offer);
        }

        // SERVER SKAL HAVE EN GetResourceFromMarked og så en UpdatePlayerTable hvor den -guld og +resource på en spiller.



        // Trade offer stuff:

        public void GetAllTradeOffers()
        {
            allTradeOffers = DatabaseInterface.ReadAllTradeOffers(player.Name);
        }



        // Message stuff:

        public void GetNewMessage()
        {
            Message msg = DatabaseInterface.GetMessage(player.Name);
            if(msg != null) collectedMessages.Add(msg);            
        }

        // Exception skal kastes
        public void SendNewMessage(Message msg)
        {
            DatabaseInterface.SendMessage(msg);
        }
    }
}
