using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _02148_Project;
using _02148_Project.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Server.Test
{
    [TestClass]
    public class DatabaseInterfaceTests
    {
        /// <summary>
        /// Method to output a offer to the test output
        /// </summary>
        /// <param name="offer">Offer to output</param>
        private static void PrintOffer(ResourceOffer offer)
        {
            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                offer.Id, offer.SellerName, offer.Type, offer.Count,
                offer.Price, offer.HighestBidder, offer.HighestBid);
        }

        [TestMethod]
        [TestCategory("Player")]
        public void CreatePlayerTest()
        {
            try
            {
                DatabaseInterface.PutPlayer("Oliver");
            } 
            catch (Exception ex)
            {
                Assert.AreEqual("Username already taken", ex.Message);
            }
        }

        [TestMethod]
        [TestCategory("Player")]
        public void ReadPlayersObjectsTest()
        {
            List<Player> players = DatabaseInterface.ReadAllPlayers();
            
            Console.WriteLine("Players in the database as objects");
            Console.WriteLine("Name");
            foreach (Player player in players)
            {
                Console.WriteLine("{0}\t{1}\t{2}", player.Name, player.Wood, player.Gold);
            }
        }

        [TestMethod]
        [TestCategory("Player")]
        public void ReadPlayerWithResourcesTest()
        {
            Player player = DatabaseInterface.ReadPlayer("Oliver");

            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", 
                player.Name, player.Wood, player.Clay, player.Wool, player.Stone,
                player.Iron, player.Straw, player.Food, player.Gold);

            Assert.AreEqual("Oliver", player.Name);
            Assert.IsNotNull(player.Wood);
        }

        [TestMethod]
        [TestCategory("Player")]
        public void UpdatePlayerResourcesTest()
        {
            // Get the data before
            Player playerBefore = DatabaseInterface.ReadPlayer("Oliver");

            // Create and update the new data
            Random random = new Random();
            Player update = new Player("Oliver", random.Next(), random.Next(), random.Next(),
                random.Next(), random.Next(), random.Next(), random.Next(), random.Next());
            DatabaseInterface.UpdatePlayer(update);

            // Output the results
            Player player = DatabaseInterface.ReadPlayer("Oliver");
            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
                player.Name, player.Wood, player.Clay, player.Wool, player.Stone,
                player.Iron, player.Straw, player.Food, player.Gold);

            Assert.AreNotEqual(playerBefore.Wood, player.Wood);
            Assert.AreNotEqual(playerBefore.Gold, player.Gold);
        }

        [TestMethod]
        [TestCategory("Player")]
        public void UpdatePlayerWithOnlyOneResourceTest()
        {
            Player player = DatabaseInterface.ReadPlayer("Oliver");
            DatabaseInterface.UpdatePlayerResource("Oliver", ResourceType.Gold, 10);
            Player after = DatabaseInterface.ReadPlayer("Oliver");
            Assert.AreEqual(player.Gold + 10, after.Gold);
        }

        [TestMethod]
        [TestCategory("Player")]
        public void SubtractOneResourceTypeFromPlayerTest()
        {
            Player before = DatabaseInterface.ReadPlayer("Oliver");
            DatabaseInterface.UpdatePlayerResource("Oliver", ResourceType.Iron, -10);
            Player after = DatabaseInterface.ReadPlayer("Oliver");
            Assert.AreEqual(before.Iron - 10, after.Iron);
        }

        [TestMethod]
        [TestCategory("Player")]
        public void DeletePlayerTest()
        {
            DatabaseInterface.PutPlayer("Dummy");
            Player player = DatabaseInterface.ReadPlayer("Dummy");
            Assert.IsNotNull(player);
            DatabaseInterface.DeletePlayer("Dummy");
        }

        [TestMethod]
        [TestCategory("ResourceOffer")]
        public void ReadResourceOfferTest()
        {
            ResourceOffer offer = DatabaseInterface.ReadResourceOffer(2);
            Assert.AreEqual(ResourceType.Iron, offer.Type);
            Assert.AreEqual(60, offer.Price);
        }

        [TestMethod]
        [TestCategory("ResourceOffer")]
        public void ReadResourceOffersFromMarketTest()
        {
            List<ResourceOffer> offers = DatabaseInterface.ReadAllResourceOffers();

            Console.WriteLine("Id\tSeller\tType\tCount\tPrice\tBidder\tHigest Bid");
            foreach (ResourceOffer offer in offers)
            {
                PrintOffer(offer);
            }
        }

        [TestMethod]
        [TestCategory("ResourceOffer")]
        public void GetResourceOfferFromMarketTest()
        {
            ResourceOffer offer = DatabaseInterface.GetResourceOffer(1);

            Assert.IsNotNull(offer);
            PrintOffer(offer);
            Console.WriteLine("End of test");
        }
        
        [TestMethod]
        [TestCategory("ResourceOffer")]
        public void PutResourceOfferOnMarketTest()
        {
            ResourceOffer offer = new ResourceOffer("Oliver", ResourceType.Iron, 40, 60);
            offer.Id = DatabaseInterface.PutResourceOfferOnMarket(offer);
            PrintOffer(offer);
        }

        [TestMethod]
        [TestCategory("ResourceOffer")]
        public void UpdateResourceOfferTest()
        {
            int id = 3; // Used to select the resource in the database to test on
            // Get the current offer on the market
            ResourceOffer beforeOffer = DatabaseInterface.ReadResourceOffer(id);
            Random random = new Random();
            int count = 100;

            // Create the new offer to update
            ResourceOffer offer = new ResourceOffer(id, "Oliver", ResourceType.Iron, 
                count, 60, "Alex", count);
            DatabaseInterface.UpdateResourceOffer(offer);

            ResourceOffer updatedOffer = DatabaseInterface.ReadResourceOffer(id);
            Assert.AreEqual(count, updatedOffer.Count);
            Assert.AreNotEqual(beforeOffer.Count, updatedOffer.Count);
            Assert.AreEqual(count, updatedOffer.HighestBid);
            Assert.AreEqual("Alex", updatedOffer.HighestBidder);
        }

        [TestMethod]
        [TestCategory("Chat")]
        public void SendMessageTest()
        {
            Message msg = new Message("Hello Alex", "Oliver", "Alex");
            DatabaseInterface.SendMessage(msg);
        }

        [TestMethod]
        [TestCategory("Chat")]
        public void RecieveMessageTest()
        {
            List<Message> messages = DatabaseInterface.ReadMessages("Alex");
            foreach (Message msg in messages)
                Console.WriteLine("{0}\t{1}\t{2}", msg.Context, msg.SenderName, msg.RecieverName);
        }


        [TestMethod]
        [TestCategory("Trade Offer")]
        public void PutTradeOfferInDatabaseTest()
        {
            TradeOffer offer = new TradeOffer("Oliver", "Alex", new Dictionary<ResourceType, int>(), new Dictionary<ResourceType, int>());
            DatabaseInterface.PutTradeOffer(offer);
        }

        [TestMethod]
        [TestCategory("Trade Offer")]
        public void ReadTradeOffersInDatabaseTest()
        {
            List<TradeOffer> offers = DatabaseInterface.ReadAllTradeOffers("Alex");
            Console.WriteLine("Id\tSeller\tReciever\tType\tCount\tPrice");
            foreach (TradeOffer offer in offers)
            {
                Console.WriteLine("{0}\t{1}\t{2}", offer.Id, offer.SellerName,
                    offer.RecieverName);
            }
        }

        [TestMethod]
        [TestCategory("Trade Offer")]
        public void GetTradeOfferInDatabaseTest()
        {
            TradeOffer offer = DatabaseInterface.GetTradeOffer(1);
            Console.WriteLine("{0}\t{1}\t{2}", offer.Id, offer.SellerName,
                offer.RecieverName);
        }

        [TestMethod]
        [TestCategory("Database notifications")]
        public void DependencyOnPlayersTest()
        {
            DatabaseInterface.DependencyInitialization();
            DatabaseInterface.SetupDatabaseListeners(OnChange_Players_Example, 
                OnChange_ResourceOffers, OnChange_TradeOffers, OnChange_Chat);
            Console.WriteLine("Name\tWood");
            int i = 1;
            while (i < 6)
            {
                if (i == 2)
                {
                    DatabaseInterface.UpdatePlayerResource("Alex", ResourceType.Wood, 100);
                }
                if (i == 4)
                {
                    DatabaseInterface.UpdatePlayerResource("Oliver", ResourceType.Wood, 100);
                }

                Console.WriteLine(i + " - Eventbased update");
                foreach (Player player in players)
                {
                    Console.WriteLine("{0}\t{1}", player.Name, player.Wood);
                }
                Console.WriteLine("Manual update");
                foreach (Player player in DatabaseInterface.ReadAllPlayers())
                {
                    Console.WriteLine("{0}\t{1}", player.Name, player.Wood);
                }
                Console.WriteLine();

                System.Threading.Thread.Sleep(1000);
                i++;
            }

            DatabaseInterface.DependencyTermination();
        }

        #region OnChange_Examples

        public static List<Player> players = new List<Player>();

        /// <summary>
        /// On change methode for when the players table changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnChange_Players_Example(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;
            dependency.OnChange -= OnChange_Players_Example;

            // Need to update the correct field, not players in this class
            players = DatabaseInterface.ReadAllPlayers();
            DatabaseInterface.MonitorPlayers(OnChange_Players_Example);
        }

        /// <summary>
        /// On change methode, for when the resource offers changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChange_ResourceOffers(object sender, SqlNotificationEventArgs e)
        {
            //(sender as SqlDependency).OnChange -= OnChange_ResourceOffers;
            //resouceOffers = ReadAllResourceOffers();
            //DatabaseHandler.MonitorResourceOffers();
        }

        private void OnChange_TradeOffers(object sender, SqlNotificationEventArgs e)
        {
            //(sender as SqlDependency).OnChange -= OnChange_TradeOffers;
            //// Call methode to update all the relevant trade offers
            ////tradeOffers = ReadAllTradeOffers();
            //DatabaseHandler.MonitorTradeOffers();
        }

        private void OnChange_Chat(object sender, SqlNotificationEventArgs e)
        {
            //(sender as SqlDependency).OnChange -= OnChange_Chat;
            //// Call methode to update the latest message 
            ////message = GetMessage();
            //DatabaseHandler.MonitorChat();
        }
        #endregion
    
        [TestMethod]
        [TestCategory("Construction")]
        public void ReadConstruction_PlayerTest()
        {
            Player player = DatabaseInterface.ReadPlayer("Oliver");
            Assert.IsNotNull(player);

            Console.WriteLine("{0}\t{1}\t{2}\t{3}", player.Name, player.Goldmine, player.Cottage, player.Forge);
        }

        [TestMethod]
        [TestCategory("Construction")]
        public void UpdateConstruction_PlayerTest()
        {
            Player before = DatabaseInterface.ReadPlayer("Oliver");
            DatabaseInterface.UpdatePlayerConstructions("Oliver", Construction.Goldmine, 1);
            Player after = DatabaseInterface.ReadPlayer("Oliver");

            Assert.AreEqual(before.Goldmine + 1, after.Goldmine);
        }
    }
}
