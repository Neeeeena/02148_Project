using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02148_Project;
using _02148_Project.Model;
using _02148_Project.Model.Exceptions;
using System.Timers;
using System.Data.SqlClient;

namespace _02148_Project.Client
{
    public class MainClient
    {

        //public List<ResourceOffer> allResourcesOnMarket;
        //public List<TradeOffer> allYourRecievedTradeOffers;
        //public static List<TradeOffer> allYourSentTradeOffers;
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

        public static string createPlayer(string name)
        {
            player = new Player(name);
            try
            {
            DatabaseInterface.PutPlayer(name);
            }
            catch(Exception ex)
            {
                return ex.Message;                
            }
            //////////////// Kun for test
            DatabaseInterface.UpdatePlayerResource(name, ResourceType.Wood, 2);
            DatabaseInterface.UpdatePlayerResource(name, ResourceType.Clay, 1);
            ////////////////
            return "";
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
            ReadOtherPlayers();
            ReadAllTradeOffersForYou();
            GetNewMessage();
        }

        // Market stuff:

        public static List<ResourceOffer> UpdateResourcesOnMarket()
        {
            return DatabaseInterface.ReadAllResourceOffers();
        }

        //Missing tests
        public static void BidOnResource(ResourceOffer offer)
        {
            ResourceOffer prevOffer = DatabaseInterface.ReadResourceOffer(offer.Id);
            try
            {
            DatabaseInterface.UpdateResourceOffer(offer);
            }
            catch (ConnectionException e) {

                //parse error msg to user
                return;
            }

            MainServer.bidAccepted(offer.Id);

            //Transfering the money back to the previous highest bidder, if one exists
            if (prevOffer.HighestBidder != null)
                DatabaseInterface.UpdatePlayerResource(prevOffer.HighestBidder, ResourceType.Gold, prevOffer.HighestBid);

            //Taking money from the new highest bidder
            DatabaseInterface.UpdatePlayerResource(offer.HighestBidder, ResourceType.Gold, -offer.HighestBid);


            //When the auction is ended, the user has already spent the money, and only the wares have to be transfered

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
        //Kun for test!
        public static void ReadAPlayer()
        {
            player = DatabaseInterface.ReadAllPlayers().Find(e => e.Name == "Nina");
            DatabaseInterface.UpdatePlayerResource("Nina", ResourceType.Wool, 1);
        }

        public static List<LocalResource> GetLocalResources()
        {
            string idGenerator = "a";
            UpdateOwnGoldAndResources();
            List<LocalResource> result = new List<LocalResource>();
            for (int i = 0; i < player.Clay; i++)
            {
                result.Add(new LocalResource(ResourceType.Clay) { Id = idGenerator});
                idGenerator += "a";
            }
            for (int i = 0; i < player.Food; i++)
            {
                result.Add(new LocalResource(ResourceType.Food) { Id = idGenerator });
                idGenerator += "a";
            }
            for (int i = 0; i < player.Iron; i++)
            {
                result.Add(new LocalResource(ResourceType.Iron) { Id = idGenerator});
                idGenerator += "a";
            }
            for (int i = 0; i < player.Stone; i++)
            {
                result.Add(new LocalResource(ResourceType.Stone) { Id = idGenerator});
                idGenerator += "a";
            }
            for (int i = 0; i < player.Straw; i++)
            {
                result.Add(new LocalResource(ResourceType.Straw) { Id = idGenerator });
                idGenerator += "a";
            }
            for (int i = 0; i < player.Wood; i++)
            {
                result.Add(new LocalResource(ResourceType.Wood) { Id = idGenerator});
                idGenerator += "a";
            }
            for (int i = 0; i < player.Wool; i++)
            {
                result.Add(new LocalResource(ResourceType.Wool) { Id = idGenerator});
                idGenerator += "a";
            }
            return result;
        }

        public static void ReadOtherPlayers()
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
            if (offer != null) DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, offer.Count);

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


        // Message stuff:x

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
            foreach (Player p in players)
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

        /// <summary>
        /// Setup database listeners to update fields when there is any changes 
        /// to the database/tuple space
        /// </summary>
        /// <param name="players"></param>
        /// <param name="resources"></param>
        /// <param name="trades"></param>
        /// <param name="chat"></param>
        public static void SetupDatabaseListeners(DatabaseInterface.OnChange_Player players, 
            DatabaseInterface.OnChange_ResourceOffers resources,
            DatabaseInterface.OnChange_TradeOffers trades, 
            DatabaseInterface.OnChange_Chat chat)
        {
            DatabaseInterface.SetupDatabaseListeners(players, resources, trades, chat);
        }

        
        //Constructions with their required resources to build
        static Tuple<Construction, Tuple<int, ResourceType>[]>[] constructionPrice = {
            Tuple.Create(Construction.Cottage, new Tuple<int,ResourceType>[] {
                Tuple.Create(2,ResourceType.Wood),
                Tuple.Create(2,ResourceType.Wool),
                Tuple.Create(2,ResourceType.Straw) }),
            Tuple.Create(Construction.Forge, new Tuple<int,ResourceType>[] {
                Tuple.Create(4,ResourceType.Stone),
                Tuple.Create(4,ResourceType.Food),
                Tuple.Create(5,ResourceType.Iron) }),
            Tuple.Create(Construction.Mason, new Tuple<int,ResourceType>[] {
                Tuple.Create(2,ResourceType.Stone),
                Tuple.Create(2,ResourceType.Clay),
                Tuple.Create(5,ResourceType.Iron), }),
            Tuple.Create(Construction.Mill, new Tuple<int,ResourceType>[] {
                Tuple.Create(6,ResourceType.Wood),
                Tuple.Create(4,ResourceType.Straw),
                Tuple.Create(2,ResourceType.Food),
                Tuple.Create(1,ResourceType.Wool)  }),
            Tuple.Create(Construction.Farm, new Tuple<int,ResourceType>[] {
                Tuple.Create(6,ResourceType.Food),
                Tuple.Create(4,ResourceType.Straw),
                Tuple.Create(4,ResourceType.Clay),
                Tuple.Create(1,ResourceType.Wood) }),
            Tuple.Create(Construction.Townhall, new Tuple<int,ResourceType>[] {
                Tuple.Create(40,ResourceType.Gold),
                Tuple.Create(5,ResourceType.Clay),
                Tuple.Create(5,ResourceType.Wood),
                Tuple.Create(10,ResourceType.Food) })
        };


        public static void constructConstruction(Construction type)
        {
            foreach (Tuple<Construction, Tuple<int, ResourceType>[]> cp in constructionPrice)
                if (cp.Item1 == type)
                    foreach (Tuple<int, ResourceType> priceres in cp.Item2)
                    {
                        DatabaseInterface.UpdatePlayerResource(player.Name, priceres.Item2, - priceres.Item1);
                    }
                    //DENNE FUKTION SKAL TILFØJES TIL DB (minder om UpdatePlayerResources)
                    //DatabaseInterface.UpdatePlayerConstructions(player.Name, type, 1);
                    return;

            //THROW ERROR (construction does not exist)
        }
    }


}
