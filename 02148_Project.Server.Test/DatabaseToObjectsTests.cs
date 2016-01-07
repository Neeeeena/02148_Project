using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _02148_Project;
using _02148_Project.Model;
using System.Collections.Generic;

namespace Server.Test
{
    [TestClass]
    public class DatabaseToObjectsTests
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
            DatabaseToObjects.PutPlayer("Nina");
        }

        [TestMethod]
        [TestCategory("DB to Objects")]
        public void GetPlayersObjectsTest()
        {
            List<Player> players = DatabaseToObjects.ReadPlayers();

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
            ResourceOffer offer = DatabaseToObjects.ReadResourceOffer(9);

            Assert.AreEqual(ResourceType.Iron, offer.Type);
            Assert.AreEqual(60, offer.Price);
        }
        
        [TestMethod]
        public void ReadResourceOffersFromMarketTest()
        {
            List<ResourceOffer> offers = DatabaseToObjects.ReadAllResourceOffers();

            Console.WriteLine("Id\tSeller\tType\tCount\tPrice\tBidder\tHigest Bid");
            foreach (ResourceOffer offer in offers)
            {
                PrintOffer(offer);
            }
        }

        [TestMethod]
        public void RemoveResourceOfferFromMarketTest()
        {
            ResourceOffer offer = DatabaseToObjects.GetResourceOffer(1);

            if (offer != null)
            {
                PrintOffer(offer);
            }
        }
        
        [TestMethod]
        public void PutResourceOfferOnMarketTest()
        {
            ResourceOffer offer = new ResourceOffer("Oliver", ResourceType.Iron, 40, 60);
            offer.Id = DatabaseToObjects.PutResourceOfferOnMarket(offer);
            PrintOffer(offer);
        }

        [TestMethod]
        public void UpdateResourceOfferTest()
        {
            // Get the current offer on the market
            ResourceOffer beforeOffer = DatabaseToObjects.ReadResourceOffer(9);
            Random random = new Random();
            int count = random.Next();

            // Create the new offer to update
            ResourceOffer offer = new ResourceOffer(9, "Oliver", ResourceType.Iron, count, 60, "Alex", 45);
            DatabaseToObjects.UpdateResourceOffer(offer);

            ResourceOffer updatedOffer = DatabaseToObjects.ReadResourceOffer(9);
            Assert.AreEqual(count, updatedOffer.Count);
            Assert.AreNotEqual(beforeOffer.Count, updatedOffer.Count);
            Assert.AreEqual(45, updatedOffer.HighestBid);
            Assert.AreEqual("Alex", updatedOffer.HighestBidder);
        }

        [TestMethod]
        public void SendMessageTest()
        {
            Message msg = new Message("Hello Alex", "Oliver", "Alex", false);
            DatabaseToObjects.SendMessage(msg);
        }

        [TestMethod]
        public void RecieveMessageTest()
        {
            Message msg = DatabaseToObjects.ReadMessage("Alex");
            Assert.AreEqual("Hello Alex", msg.Context);
            Assert.AreEqual("Oliver", msg.SenderName);
        }
    }
}
