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
    public static class MainClient
    {

        //public List<ResourceOffer> allResourcesOnMarket;
        //public List<TradeOffer> allYourRecievedTradeOffers;
        public static List<TradeOffer> allYourSentTradeOffers;
        //public List<Message> collectedMessages = new List<Message>();
        public static List<Player> allOtherPlayers;
        
        public static Player player;

        public static Timer updateTimer;

        public static List<Tuple<Timer, int>> timersWithId = new List<Tuple<Timer, int>>();
        
        public static void GameSetup()
        {
            //Setup all fields before game, like username, goldamount at start, etc.
            //Code Behind probably

        }

        public static void createPlayer(string name)
        {
            player = new Player(name);
            DatabaseInterface.PutPlayer(name);
            //Kun for test
            DatabaseInterface.UpdatePlayerResource(name, ResourceType.Wood, 2);
        }

        public static void deletePlayer(string name)
        {
            DatabaseInterface.DeletePlayer(name);
        }
   

        public static void setUpdateTimer()
        {
            updateTimer = new Timer(1000);
            updateTimer.AutoReset = true;
            updateTimer.Elapsed += GetUpdatesFromTupleSpace;
        }

        private static void GetUpdatesFromTupleSpace(object sender, ElapsedEventArgs e)
        {
            UpdateResourcesOnMarket();
            UpdateOwnGoldAndResources();
            ReadOtherPlayersGold();
            ReadAllTradeOffersForYou();
            GetNewMessage();
        }

        // Market stuff:

        public static List<ResourceOffer> UpdateResourcesOnMarket()
        {
            return DatabaseInterface.ReadAllResourceOffers();
        }

        public static void BidOnResource(ResourceOffer offer)
        {
            //Try update offer
            DatabaseInterface.UpdateResourceOffer(offer);
            //Catch exception hvis bid var lavere
        }

        public static void PlaceResourceOfferOnMarket(ResourceOffer offer)
        {
            DatabaseInterface.PutResourceOfferOnMarket(offer);
            DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, -1);
        }

        // SERVER SKAL HAVE EN GetResourceFromMarked og så en UpdatePlayerTable hvor den -guld og +resource på en spiller.
        // Til når en ressource bliver købt


        // Own player gold and resource stuff
        public static void UpdateOwnGoldAndResources()
        {
            player = DatabaseInterface.ReadPlayer(player.Name);
        }

        public static List<LocalResource> GetLocalResources()
        {
            string idGenerator = "a";
            UpdateOwnGoldAndResources();
            List<LocalResource> result = new List<LocalResource>();
            for(int i = 0; i < player.Clay; i++)
            {
                result.Add(new LocalResource() { Id = idGenerator,Type=ResourceType.Clay});
                idGenerator += "a";
            }
            for (int i = 0; i < player.Food; i++)
            {
                result.Add(new LocalResource() { Id = idGenerator, Type = ResourceType.Food });
                idGenerator += "a";
            }
            for (int i = 0; i < player.Iron; i++)
            {
                result.Add(new LocalResource() { Id = idGenerator, Type = ResourceType.Iron });
                idGenerator += "a";
            }
            for (int i = 0; i < player.Stone; i++)
            {
                result.Add(new LocalResource() { Id = idGenerator, Type = ResourceType.Stone });
                idGenerator += "a";
            }
            for (int i = 0; i < player.Straw; i++)
            {
                result.Add(new LocalResource() { Id = idGenerator, Type = ResourceType.Straw });
                idGenerator += "a";
            }
            for (int i = 0; i < player.Wood; i++)
            {
                result.Add(new LocalResource() { Id = idGenerator, Type = ResourceType.Wood });
                idGenerator += "a";
            }
            for (int i = 0; i < player.Wool; i++)
            {
                result.Add(new LocalResource() { Id = idGenerator, Type = ResourceType.Wool });
                idGenerator += "a";
            }
            return result;
        }

        public static void ReadOtherPlayersGold()
        {
            allOtherPlayers = DatabaseInterface.ReadAllPlayers();
            removeYourself();
        }

        // Used by ReadOtherPlayersGold
        private static void removeYourself()
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
        public static List<TradeOffer> ReadAllTradeOffersForYou()
        {
            return DatabaseInterface.ReadAllTradeOffers(player.Name);
        }

        public static void SendTradeOfferToPlayer(TradeOffer offer)
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

        private static void TakeBackTradeOffer(object sender, ElapsedEventArgs e)
        {
            // Ældste timer er altid på position 0
            int id = timersWithId.ElementAt(0).Item2;
            //Try get
            TradeOffer offer = DatabaseInterface.GetTradeOffer(id);
            //If gotten
            if(offer != null) DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, offer.Count);

            timersWithId.RemoveAt(0);
        }

        public static void AcceptTradeOffer(int id)
        {
            TradeOffer offer = DatabaseInterface.GetTradeOffer(id);
            SubtractResource(offer.PriceType, offer.Price);
            DatabaseInterface.UpdatePlayerResource(offer.SellerName, offer.PriceType, offer.Price);
            DatabaseInterface.UpdatePlayerResource(player.Name, offer.PriceType, -offer.Price);
            DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, offer.Count);
            SendNewMessage(new Message("Trade accepted", player.Name, offer.SellerName));           
        }

        public static void DeclineTradeOffer(int id)
        {
            TradeOffer offer = DatabaseInterface.GetTradeOffer(id);
            DatabaseInterface.UpdatePlayerResource(offer.SellerName, offer.Type, offer.Count); 
            SendNewMessage(new Message("Trade declined", player.Name, offer.SellerName));
        }


        // Message stuff:

        public static Message GetNewMessage()
        {
            return DatabaseInterface.GetMessage(player.Name);
      
        }

        // Exception skal kastes
        public static void SendNewMessage(Message msg)
        {
            DatabaseInterface.SendMessage(msg);
        }

        public static void SendNewMessageToAll(Message msg)
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


        private static void SubtractResource(ResourceType type, int count)
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
