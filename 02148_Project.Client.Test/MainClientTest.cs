﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _02148_Project.Model;

namespace _02148_Project.Client.Test
{
    [TestClass]
    public class MainClientTest
    {
        private static MainClient sender;
        private static MainClient reciever;

        [TestInitialize]
        public void before()
        {
            sender = new MainClient();
            sender.player = DatabaseInterface.ReadPlayer("Oliver");
            reciever = new MainClient();
            reciever.player = DatabaseInterface.ReadPlayer("Alex");
        }

        [TestMethod]
        [TestCategory("Client")]
        public void PutResourceOfferOnMarketTest()
        {
            sender.PlaceResourceOfferOnMarket(new ResourceOffer(sender.player.Name, ResourceType.Iron, 100, 50));
        }

        [TestMethod]
        [TestCategory("Client")]
        public void BidOnResourceOfferOnMarketTest()
        {
            sender.allResourcesOnMarket = DatabaseInterface.ReadAllResourceOffers();
            ResourceOffer offer = sender.allResourcesOnMarket.Find(delegate (ResourceOffer o) { return true; });
            offer.HighestBidder = reciever.player.Name;
            offer.HighestBid = 100;
            sender.BidOnResource(offer);
        }

        [TestMethod]
        [TestCategory("Client")]
        public void SendTradeOfferTest()
        {
            sender.SendTradeOfferToPlayer(new TradeOffer(sender.player.Name, reciever.player.Name, 
                ResourceType.Stone, 100, ResourceType.Iron, 50));
        }

        [TestMethod]
        [TestCategory("Client")]
        public void AcceptTradeOfferTest()
        {
            sender.SendTradeOfferToPlayer(new TradeOffer(sender.player.Name, reciever.player.Name,
                ResourceType.Stone, 100, ResourceType.Iron, 50));
            sender.allTradeOffers = DatabaseInterface.ReadAllTradeOffers(reciever.player.Name);
            TradeOffer offer = sender.allTradeOffers.Find(delegate (TradeOffer o) { return o.RecieverName == reciever.player.Name; });
            reciever.AcceptTradeOffer(offer.Id);
            Player after = DatabaseInterface.ReadPlayer(reciever.player.Name);

            Assert.AreEqual(reciever.player.Stone + 100, after.Stone);
        }
    }
}
