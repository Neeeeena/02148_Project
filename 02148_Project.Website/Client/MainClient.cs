﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02148_Project;
using _02148_Project.Model;
using _02148_Project.Website;
using _02148_Project.Model.Exceptions;
using System.Timers;
using System.Data.SqlClient;
using System.Diagnostics;

namespace _02148_Project.Client
{
    public class MainClient
    {

        private static bool missionGiven = false;
        //public List<ResourceOffer> allResourcesOnMarket;
        //public List<TradeOffer> allYourRecievedTradeOffers;
        //public static List<TradeOffer> allYourSentTradeOffers;
        //public List<Message> collectedMessages = new List<Message>();

        public static List<Construction> MissionList = new List<Construction>();

        public static List<Player> allOtherPlayers = new List<Player>();
        
        public static Player player;

        public static Timer updateTimer;

        public static List<Tuple<Timer, int>> timersWithId = new List<Tuple<Timer, int>>();

        public static bool youWin = false;

        public static System.Timers.Timer incomeTimer = new System.Timers.Timer();

        public static bool incomeTimerHasBeenSet = false;

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
            return null;
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

        public static void GiveMission()
        {
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                MissionList.Add((Construction)r.Next(0, 6));
            }
        }

        // Market stuff:

        // Kan ikke returne string
        public static List<ResourceOffer> UpdateResourcesOnMarket()
        {
            try {
                return DatabaseInterface.ReadAllResourceOffers();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " UpdateAllResourcesOnMarked() made this exception");
            }
            return new List<ResourceOffer>();
        }

        //Missing tests
        public static string BidOnResource(ResourceOffer offer)
        {
            ResourceOffer prevOffer = DatabaseInterface.ReadResourceOffer(offer.Id);
            if (player.Gold < offer.HighestBid) return ("I must apologize my good sir, but you do not seem to have enough medieval currency");
            try
            {
                //Hvad sker der nu, hvis UpdateRessourceOffer virker, men den af en eller anden grund ikke kan opdatere dit guld?
                //Så kaster den en exception, men updateResource bliver vel stadig kaldt?


                DatabaseInterface.UpdateResourceOffer(offer);
                //Transfering the money back to the previous highest bidder, if one exists
                if (prevOffer.HighestBidder != null)
                    DatabaseInterface.UpdatePlayerResource(prevOffer.HighestBidder, ResourceType.Gold, prevOffer.HighestBid);

                //Taking money from the new highest bidder
                DatabaseInterface.UpdatePlayerResource(offer.HighestBidder, ResourceType.Gold, -offer.HighestBid);
            }
            catch (Exception ex) {

                //parse error msg to user
                return ex.Message;
            }
            return "";

            //MainServer.bidAccepted(offer.Id);

            //When the auction is ended, the user has already spent the money, and only the wares have to be transfered

        }

        public static string PlaceResourceOfferOnMarket(ResourceOffer offer)
        {
            try
            {
                int id = DatabaseInterface.PutResourceOfferOnMarket(offer);
                DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, -1);
                MainServer.adverts.Add(new Advert(id));
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
        //Kun for test!
        public static void ReadAPlayer(string userName)
        {
            player = DatabaseInterface.ReadPlayer(userName);
            //DatabaseInterface.UpdatePlayerResource("Oliver", ResourceType.Wool, 1);
        }
        public static List<MissionGoal> GetMissionGoals()
        {
            if (!missionGiven)
            {
                GiveMission();
                missionGiven = true;
            }
            int nextId = 0;
            List<MissionGoal> missionGoals = new List<MissionGoal>();
            foreach(Construction c in MissionList)
                missionGoals.Add(new MissionGoal(c) { Id = "b" + ++nextId });         
            return missionGoals;
        }

        public static List<LocalResource> GetLocalResources()
        {
            int nextId = 0;
            UpdateOwnGoldAndResources();
            List<LocalResource> result = new List<LocalResource>();
            for (int i = 0; i < player.Clay; i++)
                result.Add(new LocalResource(ResourceType.Clay) { Id = "a" + ++nextId});
            for (int i = 0; i < player.Food; i++)
                result.Add(new LocalResource(ResourceType.Food) { Id = "a" + ++nextId });
            for (int i = 0; i < player.Iron; i++)
                result.Add(new LocalResource(ResourceType.Iron) { Id = "a" + ++nextId });
            for (int i = 0; i < player.Stone; i++)
                result.Add(new LocalResource(ResourceType.Stone) { Id = "a" + ++nextId });
            for (int i = 0; i < player.Straw; i++)
                result.Add(new LocalResource(ResourceType.Straw) { Id = "a" + ++nextId });
            for (int i = 0; i < player.Wood; i++)
                result.Add(new LocalResource(ResourceType.Wood) { Id = "a" + ++nextId });
            for (int i = 0; i < player.Wool; i++)
                result.Add(new LocalResource(ResourceType.Wool) { Id = "a" + ++nextId });
            return result;
        }

        // Kan ikke returne string
        public static void ReadOtherPlayers()
        {
            try {
                allOtherPlayers = DatabaseInterface.ReadAllPlayers();
                removeYourself();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " This was from ReadOtherPlayer");
            }
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
                Console.WriteLine(ex.Message + " ReadAllTradeOffersForYou() threw this");
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

                //SubtractResource(offer.Type, offer.Count); - skal måske slet ikke bruges

                SubtractPlayerResources(offer.SellerResources, player.Name);

                Timer timer = new Timer(30000);
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
            if (offer != null) AddPlayerResources(offer.SellerResources, player.Name);

            timersWithId.RemoveAt(0);
        }

        public static string AcceptTradeOffer(int id)
        {
            try {
                TradeOffer offer = DatabaseInterface.GetTradeOffer(id);
                //SubtractResource(offer.PriceType, offer.Price); - skal måske ikke bruges
                //DatabaseInterface.UpdatePlayerResource(offer.SellerName, offer.PriceType, offer.Price);
                AddPlayerResources(offer.ReceiverResources, offer.SellerName);
                //DatabaseInterface.UpdatePlayerResource(player.Name, offer.PriceType, -offer.Price);
                SubtractPlayerResources(offer.ReceiverResources, player.Name);
                //DatabaseInterface.UpdatePlayerResource(player.Name, offer.Type, offer.Count);
                AddPlayerResources(offer.SellerResources, player.Name);
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
                //DatabaseInterface.UpdatePlayerResource(offer.SellerName, offer.Type, offer.Count); 
                AddPlayerResources(offer.SellerResources, offer.SellerName);
                SendNewMessage(new Message("Trade declined", player.Name, offer.SellerName));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }

        public static void AddPlayerResources(Dictionary<ResourceType,int> dic ,string name)
        {
            foreach(var pair in dic)
            {
                if(pair.Value != 0)
                {
                    DatabaseInterface.UpdatePlayerResource(name, pair.Key, pair.Value);
                }
            }
        }

        public static void SubtractPlayerResources(Dictionary<ResourceType, int> dic, string name)
        {
            foreach (var pair in dic)
            {
                if (pair.Value != 0)
                {
                    DatabaseInterface.UpdatePlayerResource(name, pair.Key, -pair.Value);
                }
            }
        }



        // Message stuff:
        //Kan ikke returne string
        public static List<Message> GetNewMessage()
        {
            try
            {
                return DatabaseInterface.ReadMessages(player.Name);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " GetMessage() made this exception");
            }
            return null;
      
        }

        // Exception skal kastes
        public static string SendNewMessage(Message message)
        {
            try
            {
                DatabaseInterface.SendMessage(message);
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
            
            Tuple.Create(Construction.Forge, new Tuple<int,ResourceType>[] {
                Tuple.Create(3,ResourceType.Stone),
                Tuple.Create(2,ResourceType.Food),
                Tuple.Create(2,ResourceType.Iron) }),
            Tuple.Create(Construction.Cottage, new Tuple<int,ResourceType>[] {
                Tuple.Create(1,ResourceType.Wood),
                Tuple.Create(1,ResourceType.Wool),
                Tuple.Create(1,ResourceType.Straw) }),
            Tuple.Create(Construction.Mill, new Tuple<int,ResourceType>[] {
                Tuple.Create(3,ResourceType.Wood),
                Tuple.Create(2,ResourceType.Straw),
                Tuple.Create(2,ResourceType.Food),
                Tuple.Create(1,ResourceType.Wool)  }),
            Tuple.Create(Construction.Farm, new Tuple<int,ResourceType>[] {
                Tuple.Create(3,ResourceType.Food),
                Tuple.Create(2,ResourceType.Straw),
                Tuple.Create(2,ResourceType.Clay),
                Tuple.Create(1,ResourceType.Wood) }),
            Tuple.Create(Construction.Townhall, new Tuple<int,ResourceType>[] {
                Tuple.Create(40,ResourceType.Gold),
                Tuple.Create(5,ResourceType.Clay),
                Tuple.Create(5,ResourceType.Wood),
                Tuple.Create(5,ResourceType.Food) }),
            Tuple.Create(Construction.Goldmine, new Tuple<int,ResourceType>[] {
                Tuple.Create(10,ResourceType.Gold),
                Tuple.Create(15,ResourceType.Iron),
                Tuple.Create(10,ResourceType.Stone),
                Tuple.Create(5,ResourceType.Wood) })
        };


        public static string constructConstruction(Construction type)
        {
            foreach (Tuple<Construction, Tuple<int, ResourceType>[]> cp in constructionPrice)
            {
                if (cp.Item1 == type)
                {
                    foreach (Tuple<int, ResourceType> priceres in cp.Item2)
                    {
                        //Muligvis tilføj noget handling her, hvis det ikke er muligt at tage alle resourcerne?
                        //Eller bare formod at gui siger "NEJ!!!!!!"?
                        try
                        {
                            DatabaseInterface.UpdatePlayerResource(player.Name, priceres.Item2, -priceres.Item1);
                            SubtractResource(priceres.Item2, priceres.Item1);
                        }
                        catch (Exception e) //INDSÆT RIGTIG ERROR
                        {
                            // If an error should happen while taking the resources, the
                            // already paid resources are returned to the buyer
                            //ERROR MSG???
                            foreach (Tuple<int, ResourceType> priceresReturn in cp.Item2)
                            {
                                if (priceresReturn == priceres)
                                {
                                    return e.Message;
                                }
                                DatabaseInterface.UpdatePlayerResource(player.Name, priceresReturn.Item2, priceresReturn.Item1);
                                SubtractResource(priceresReturn.Item2, priceresReturn.Item1);
                            }
                            return e.Message;

                        }
                    }
                }
            }
            DatabaseInterface.UpdatePlayerConstructions(player.Name, type, 1);
            UpdateMissionListAndWinCheck(type);
            return "";

            //THROW ERROR (construction does not exist)
        }

        private static void UpdateMissionListAndWinCheck(Construction con)
        {
            if(MissionList.Contains(con))
            {
                MissionList.Remove(con);
                if (MissionList.Count == 0)
                {
                    youWin = true;
                }
            }

            
        }

        private static int getIncome()
        {
            Random r = new Random();
            return 10 + r.Next(5) +
                   player.Cottage   * 1 +
                   player.Forge     * 2 + (player.Forge / 3) * r.Next(3) +
                   player.Mill      * 1 +
                   player.Farm      * 1 + (player.Farm / 2) * r.Next(2) +
                   player.Townhall  * 4 +
                   player.Goldmine  * (2 + r.Next(5));
        }

        private static void giveIncome(Object source, System.Timers.ElapsedEventArgs e)
        {
            DatabaseInterface.UpdatePlayerResource(player.Name, ResourceType.Gold, getIncome());
        }

        //START IN NEW THREAD
        public static void incomeHandler()
        {
            incomeTimer.Interval = 10000; // 30 seconds, possibly change?
            incomeTimer.AutoReset = true;
            incomeTimer.Elapsed += giveIncome;
            incomeTimer.Start();
            incomeTimerHasBeenSet = true;
        }

        public static bool hasResourcesFor(Construction type)
        {
            foreach (Tuple<Construction, Tuple<int, ResourceType>[]> cp in constructionPrice)
            {
                if (cp.Item1 == type)
                {
                    foreach (Tuple<int, ResourceType> ir in cp.Item2)
                        switch (ir.Item2)
                        {
                            case ResourceType.Clay:
                                if (player.Clay < ir.Item1) return false;
                                break;
                            case ResourceType.Food:
                                if (player.Food < ir.Item1) return false;
                                break;
                            case ResourceType.Gold:
                                if (player.Gold < ir.Item1) return false;
                                break;
                            case ResourceType.Iron:
                                if (player.Iron < ir.Item1) return false;
                                break;
                            case ResourceType.Stone:
                                if (player.Stone < ir.Item1) return false;
                                break;
                            case ResourceType.Straw:
                                if (player.Straw < ir.Item1) return false;
                                break;
                            case ResourceType.Wood:
                                if (player.Wood < ir.Item1) return false;
                                break;
                            default:   //Default er wool
                                if (player.Wool < ir.Item1) return false;
                                break;
                        }
                    return true;
                }          
            }
            //building does not exist error??
            return false;
        }
    }


}
