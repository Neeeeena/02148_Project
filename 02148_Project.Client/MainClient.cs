using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02148_Project;
using _02148_Project.Model;
using System.Timers;

namespace _02148_Project.Client
{
    public class MainClient
    {

        public List<ResourceOffer> allResourcesOnMarket;
        public List<TradeOffer> allYourRecievedTradeOffers;
        public List<TradeOffer> allYourSentTradeOffers;
        public List<Message> collectedMessages = new List<Message>();
        public List<Player> allOtherPlayers;
        
        public Player player;

        public Timer updateTimer;

        public List<Tuple<Timer, int>> timersWithId = new List<Tuple<Timer, int>>();
        
        public void GameSetup()
        {
            //Setup all fields before game, like username, goldamount at start, etc.
            //Code Behind probably
        }

        public void setUpdateTimer()
        {
            updateTimer = new Timer(1000);
            updateTimer.AutoReset = true;
            updateTimer.Elapsed += GetUpdatesFromTupleSpace;
        }

        private void GetUpdatesFromTupleSpace(object sender, ElapsedEventArgs e)
        {
            UpdateResourcesOnMarket();
            UpdateOwnGoldAndResources();
            ReadOtherPlayersGold();
            ReadAllTradeOffersForYou();
            GetNewMessage();
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
        // Til når en ressource bliver købt


        // Own player gold and resource stuff
        public void UpdateOwnGoldAndResources()
        {
            player = DatabaseInterface.ReadPlayer(player.Name);
        }

        public void ReadOtherPlayersGold()
        {
            allOtherPlayers = DatabaseInterface.ReadAllPlayers();
            removeYourself();
        }

        // Used by ReadOtherPlayersGold
        private void removeYourself()
        {
            for (int i = 0; i < allOtherPlayers.Count; i++)
            {
                if (allOtherPlayers.ElementAt(i).Name == player.Name)
                {
                    allOtherPlayers.RemoveAt(i);
                    break;
                }
            }
        }
        

        // Trade offer stuff:

        // Hent trade-offers til dig
        public void ReadAllTradeOffersForYou()
        {
            allYourRecievedTradeOffers = DatabaseInterface.ReadAllTradeOffers(player.Name);
        }

        public void SendTradeOfferToPlayer(TradeOffer offer)
        {
            //Set en timer på trade-offeret
            //Lav event med reader.hasRow (hvis true, tag den selv ned og update ressourcer, ellers gør intet)
        
            //put det op
            int id = DatabaseInterface.PutTradeOffer(offer);
            SubtractResource(offer.Type, offer.Count);
            DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, -offer.Count);

            Timer timer = new Timer(10000);
            timer.Elapsed += TakeBackTradeOffer;
            timer.AutoReset = false;

            Tuple<Timer, int> meh = new Tuple<Timer, int>(timer, id);

            timersWithId.Add(meh);

            timer.Start();

            // Evt add til allYourSentTradeOffers                        
        }

        private void TakeBackTradeOffer(object sender, ElapsedEventArgs e)
        {
            // Ældste timer er altid på position 0
            int id = timersWithId.ElementAt(0).Item2;
            //Try get
            TradeOffer offer = DatabaseInterface.GetTradeOffer(id);
            //If gotten
            if(offer != null) DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, offer.Count);

            timersWithId.RemoveAt(0);
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
