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
            //Setup all fields before game, like username, goldamount at start, etc.
        }

        public void SetupTimer()
        {
            //Setup timeren/timersne
        }

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


        // Own player gold and resource stuff
        public void UpdateOwnGoldAndResources()
        {
            player = DatabaseInterface.ReadPlayer(player.Name);
        }

        //Update players skal addes hvis man vil se de andre guld eller ressourcer


        // Trade offer stuff:

        public void ReadAllTradeOffers()
        {
            // Read både dem til dig, og dem du har lagt op
            allTradeOffers = DatabaseInterface.ReadAllTradeOffers(player.Name);
        }

        public void SendTradeOfferToPlayer(TradeOffer offer)
        {
            //Set en timer på trade-offeret
            //Lav event med reader.hasRow (hvis true, tag den selv ned og update ressourcer, ellers går intet)
        
            //put det op
            DatabaseInterface.PutTradeOffer(offer);
            SubtractResource(offer.Type, offer.Count);
            DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, -offer.Count);                        
        }

        public void AcceptTradeOffer(int id)
        {
            TradeOffer offer = DatabaseInterface.GetTradeOffer(id);
            SubtractResource(offer.PriceType, offer.Price);
            DatabaseInterface.UpdatePlayerResource(offer.SellerName, offer.PriceType, offer.Price);
            DatabaseInterface.UpdatePlayerResource(player.Name, offer.PriceType, -offer.Price);
            DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, offer.Count);
            SendNewMessage(new Message("Trade accepted", player.Name, offer.SellerName));           
        }

        public void DeclineTradeOffer(int id)
        {
            TradeOffer offer = DatabaseInterface.GetTradeOffer(id);
            DatabaseInterface.UpdatePlayerResource(offer.SellerName, offer.Type, offer.Count); 
            SendNewMessage(new Message("Trade declined", player.Name, offer.SellerName));
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

        public void SendNewMessageToAll(Message msg)
        {
            List<Player> players = new List<Player>();
            players = DatabaseInterface.ReadAllPlayers();
            foreach(Player p in players)
            {
                if (p.Name != player.Name)
                {
                    msg.RecieverName = p.Name;
                    DatabaseInterface.SendMessage(msg);
                }
            }
        }


        private void SubtractResource(ResourceType type, int count)
        {
            switch (type)
            {
                case ResourceType.Clay:
                    player.Clay -= count;
                    break;
                case ResourceType.Food:
                    player.Food -= count;
                    break;
                case ResourceType.Gold:
                    player.Gold -= count;
                    break;
                case ResourceType.Iron:
                    player.Iron -= count;
                    break;
                case ResourceType.Stone:
                    player.Stone -= count;
                    break;
                case ResourceType.Straw:
                    player.Straw -= count;
                    break;
                case ResourceType.Wood:
                    player.Wood -= count;
                    break;
                default:   //Default er wool
                    player.Wool -= count;
                    break;
            }
        }
    }
}
