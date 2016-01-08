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
        public void GetAllResourcesOnMarket()
        {
            allResourcesOnMarket = CommunicationFunctionality.UpdateAllResourceOffers();
        }

        public void GetAllTradeOffers()
        {
            allTradeOffers = CommunicationFunctionality.UpdateAlleTradeOffers(player.Name);
        }
    }
}
