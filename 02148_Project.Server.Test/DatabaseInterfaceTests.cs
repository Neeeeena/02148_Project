using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _02148_Project;
using _02148_Project.Model;
using System.Collections.Generic;

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
        public void CreatePlayerTest()
        {
            DatabaseInterface.PutPlayer("Nina");
        }

        [TestMethod]
        [TestCategory("DB to Objects")]
        public void GetPlayersObjectsTest()
        {
            List<Player> players = DatabaseInterface.ReadPlayers();
            
            Console.WriteLine("Players in the database as objects");
            Console.WriteLine("Name");
            foreach (Player player in players)
            {
                Console.WriteLine("{0}", player.Name);
            }
        }

        [TestMethod]
        public void ReadResourceOfferTest()
        {
            ResourceOffer offer = DatabaseInterface.ReadResourceOffer(9);

            Assert.AreEqual(ResourceType.Iron, offer.Type);
            Assert.AreEqual(60, offer.Price);
        }
        
        [TestMethod]
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
        public void GetResourceOfferFromMarketTest()
        {
            ResourceOffer offer = DatabaseInterface.GetResourceOffer(5);

            if (offer != null)
            {
                PrintOffer(offer);
            }
            Console.WriteLine("End of test");
        }
        
        [TestMethod]
        public void PutResourceOfferOnMarketTest()
        {
            ResourceOffer offer = new ResourceOffer("Oliver", ResourceType.Iron, 40, 60);
            offer.Id = DatabaseInterface.PutResourceOfferOnMarket(offer);
            PrintOffer(offer);
        }

        [TestMethod]
        public void UpdateResourceOfferTest()
        {
            // Get the current offer on the market
            ResourceOffer beforeOffer = DatabaseInterface.ReadResourceOffer(9);
            Random random = new Random();
            int count = random.Next();

            // Create the new offer to update
            ResourceOffer offer = new ResourceOffer(9, "Oliver", ResourceType.Iron, count, 60, "Alex", 45);
            DatabaseInterface.UpdateResourceOffer(offer);

            ResourceOffer updatedOffer = DatabaseInterface.ReadResourceOffer(9);
            Assert.AreEqual(count, updatedOffer.Count);
            Assert.AreNotEqual(beforeOffer.Count, updatedOffer.Count);
            Assert.AreEqual(45, updatedOffer.HighestBid);
            Assert.AreEqual("Alex", updatedOffer.HighestBidder);
        }

        [TestMethod]
        public void SendMessageTest()
        {
            Message msg = new Message("Hello Alex", "Oliver", "Alex", false);
            DatabaseInterface.SendMessage(msg);
        }

        [TestMethod]
        public void RecieveMessageTest()
        {
            Message msg = DatabaseInterface.GetMessage("Alex");
            Console.WriteLine("{0}\t{1}\t{2}\t{3}", msg.Context, msg.SenderName, msg.RecieverName, msg.ToAll);
        }


        [TestMethod]
        [TestCategory("Trade Offer")]
        public void PutTradeOfferInDatabaseTest()
        {
            TradeOffer offer = new TradeOffer("Oliver", "Alex", ResourceType.Stone, 15, 10);
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
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", offer.Id, offer.SellerName,
                    offer.RecieverName, (ResourceType)offer.Type, offer.Count, offer.Price);
            }
        }

        [TestMethod]
        [TestCategory("Trade Offer")]
        public void GetTradeOfferInDatabaseTest()
        {
            TradeOffer offer = DatabaseInterface.GetTradeOffer(2);
            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", offer.Id, offer.SellerName,
                offer.RecieverName, (ResourceType)offer.Type, offer.Count, offer.Price);
        }
    }
}
