using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02148_Project;
using _02148_Project.Model;
using System.Timers;
using _02148_Project.Model.Exceptions;
using System.Data.SqlClient;

namespace _02148_Project.Client
{
    public class MainClient
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
            ////////////////
            return "";
        }

        public static string deletePlayer(string name)
        {
            // Check object not found error code
            try {
                DatabaseInterface.DeletePlayer(name);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "";
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

        // Kan ikke returne string
        public static List<ResourceOffer> UpdateResourcesOnMarket()
        {
            return DatabaseInterface.ReadAllResourceOffers();
        }

        public static bool BidOnResource(ResourceOffer offer)
        {
            //Try update offer
            try
            {
            DatabaseInterface.UpdateResourceOffer(offer);
            }
            //Catch exception hvis bid var lavere
            catch (ResourceOfferException)
            {
                return false;
            }
            return true;            
        }

        public static string PlaceResourceOfferOnMarket(ResourceOffer offer)
        {
            try
            {
                DatabaseInterface.PutResourceOfferOnMarket(offer);
                DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, -1);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "";
        }

        // SERVER SKAL HAVE EN GetResourceFromMarked og så en UpdatePlayerTable hvor den -guld og +resource på en spiller.
        // Til når en ressource bliver købt


        // Own player gold and resource stuff
        public static string UpdateOwnGoldAndResources()
        {
            try
            {
                player = DatabaseInterface.ReadPlayer(player.Name);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "";
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

        // Kan ikke returne string
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
        // Kan ikke returne string
        public static List<TradeOffer> ReadAllTradeOffersForYou()
        {
            try {
                return DatabaseInterface.ReadAllTradeOffers(player.Name);
            }
            catch(Exception ex)
            {
                return new List<TradeOffer>();
            }
        }

        public static string SendTradeOfferToPlayer(TradeOffer offer)
        {
            //Set en timer på trade-offeret
            //Lav event med reader.hasRow (hvis true, tag den selv ned og update ressourcer, ellers gør intet)

            //put det op
            try {
                int id = DatabaseInterface.PutTradeOffer(offer);
                SubtractResource(offer.Type, offer.Count);
                DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, -offer.Count);

                Timer timer = new Timer(10000);
                timer.Elapsed += TakeBackTradeOffer;
                timer.AutoReset = false;

                Tuple<Timer, int> meh = new Tuple<Timer, int>(timer, id);

                timersWithId.Add(meh);

                timer.Start();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "";

            // Evt add til allYourSentTradeOffers                        
        }

        // Event så kan ikke returne string
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

        public static string AcceptTradeOffer(int id)
        {
            try {
                TradeOffer offer = DatabaseInterface.GetTradeOffer(id);
                SubtractResource(offer.PriceType, offer.Price);
                DatabaseInterface.UpdatePlayerResource(offer.SellerName, offer.PriceType, offer.Price);
                DatabaseInterface.UpdatePlayerResource(player.Name, offer.PriceType, -offer.Price);
                DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, offer.Count);
                SendNewMessage(new Message("Trade accepted", player.Name, offer.SellerName));
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "";        
        }

        public static string DeclineTradeOffer(int id)
        {
            try
            {
                TradeOffer offer = DatabaseInterface.GetTradeOffer(id);
                DatabaseInterface.UpdatePlayerResource(offer.SellerName, offer.Type, offer.Count);
                SendNewMessage(new Message("Trade declined", player.Name, offer.SellerName));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }


        // Message stuff:
        //Kan ikke returne string
        public static Message GetNewMessage()
        {
            return DatabaseInterface.GetMessage(player.Name);
      
        }

        // Exception skal kastes
        public static string SendNewMessage(Message msg)
        {
            try {
                DatabaseInterface.SendMessage(msg);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "";
        }

        public static string SendNewMessageToAll(Message msg)
        {
            try {
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
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "";
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
        /// On change methode for when the players table changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnChange_Players(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;
            dependency.OnChange -= OnChange_Players;

            // Need to update the correct field, not players in this class
            ReadOtherPlayers();
            GetLocalResources();
            DatabaseInterface.MonitorPlayers(OnChange_Players);
        }

        public void OnChange_ResourceOffer(object sender, SqlNotificationEventArgs e)
        {
            (sender as SqlDependency).OnChange -= OnChange_ResourceOffer;
            // Find a way to update with the latest resource offers
            UpdateResourcesOnMarket();
            DatabaseInterface.MonitorResourceOffers(OnChange_ResourceOffer);
        }

        public void OnChange_TradeOffer(object sender, SqlNotificationEventArgs e)
        {
            (sender as SqlDependency).OnChange -= OnChange_ResourceOffer;
            // Find a way to update tradeoffers
            DatabaseInterface.MonitorTradeOffer(OnChange_ResourceOffer);
        }

        public void OnChange_Chat(object sender, SqlNotificationEventArgs e)
        {
            (sender as SqlDependency).OnChange -= OnChange_Chat;
            // Find a way to get the latest message
            DatabaseInterface.MonitorChat(OnChange_Chat);
        }
    }
}
