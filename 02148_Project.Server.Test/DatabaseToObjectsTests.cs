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

        [TestMethod]
        public void CreatePlayerTest()
        {
            DatabaseInterface.CreatePlayer("Ole");
        }

        [TestMethod]
        [TestCategory("DB to Objects")]
        public void GetPlayersObjectsTest()
        {
            List<Player> players = DatabaseToObjects.GetPlayers();

            Console.WriteLine("Players in the database as objects");
            Console.WriteLine("Id\tName");
            foreach (Player player in players)
            {
                Console.WriteLine("{0}", player.Name);
            }
        }
        
        [TestMethod]
        public void ReadResourceOfferFromMarketTest()
        {
            List<ResourceOffer> offers = DatabaseToObjects.ReadResourceOffers();

            Console.WriteLine("Id\tSeller Name\tType\tCount\tPrice\tHighest Bidder\tHigest Bid");
            foreach (ResourceOffer offer in offers)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                    offer.Id, offer.SellerName, offer.Type, offer.Count,
                    offer.Price, offer.HighestBidder, offer.HighestBid);
            }

        }

        [TestMethod]
        public void RemoveResourceOfferFromMarketTest()
        {
            ResourceOffer offer = DatabaseToObjects.GetResourceOffer(1);

            if (offer != null)
            {
                Console.WriteLine("{0}\t{1}", offer.Id, offer.SellerName);
            }
        }
    }
}
