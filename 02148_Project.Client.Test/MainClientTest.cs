//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using _02148_Project.Model;

//namespace _02148_Project.Client.Test
//{
//    [TestClass]
//    public class MainClientTest
//    {
//        private static MainClient sender;
//        private static MainClient reciever;

//        [TestInitialize]
//        public void before()
//        {
//            sender = new MainClient();
//            sender.player = DatabaseInterface.ReadPlayer("Oliver");
//            reciever = new MainClient();
//            reciever.player = DatabaseInterface.ReadPlayer("Alex");
//        }

//        [TestMethod]
//        [TestCategory("Client")]
//        public void PutResourceOfferOnMarketTest()
//        {
//            sender.PlaceResourceOfferOnMarket(new ResourceOffer(sender.player.Name, ResourceType.Iron, 100, 50));
//        }

//        [TestMethod]
//        [TestCategory("Client")]
//        public void BidOnResourceOfferOnMarketTest()
//        {
//            sender.allResourcesOnMarket = DatabaseInterface.ReadAllResourceOffers();
//            ResourceOffer offer = sender.allResourcesOnMarket.Find(delegate (ResourceOffer o) { return true; });
//            offer.HighestBidder = reciever.player.Name;
//            offer.HighestBid = 200;
//            sender.BidOnResource(offer);
//        }

//        [TestMethod]
//        [TestCategory("Client - Trade Offer")]
//        public void SendTradeOfferTest()
//        {
//            sender.SendTradeOfferToPlayer(new TradeOffer(sender.player.Name, reciever.player.Name, 
//                ResourceType.Stone, 100, ResourceType.Iron, 50));
//        }

//        [TestMethod]
//        [TestCategory("Client - Trade Offer")]
//        public void AcceptTradeOfferTest()
//        {
//            int ironAmount = 10;
//            int stoneAmount = 50;
//            sender.SendTradeOfferToPlayer(new TradeOffer(sender.player.Name, reciever.player.Name,
//                ResourceType.Stone, stoneAmount, ResourceType.Iron, ironAmount));
//            sender.allTradeOffers = DatabaseInterface.ReadAllTradeOffers(reciever.player.Name);
//            TradeOffer offer = sender.allTradeOffers.Find(delegate (TradeOffer o) { return o.RecieverName == reciever.player.Name; });
//            reciever.AcceptTradeOffer(offer.Id);

//            // Get new resource values
//            Player afterReciever = DatabaseInterface.ReadPlayer(reciever.player.Name);
//            Player afterSender = DatabaseInterface.ReadPlayer(sender.player.Name);

//            Assert.AreEqual(reciever.player.Stone + stoneAmount, afterReciever.Stone);
//            Assert.AreEqual(reciever.player.Iron, afterReciever.Iron);
//            Assert.AreEqual(sender.player.Stone, afterSender.Stone);
//            Assert.AreEqual(sender.player.Iron + ironAmount, afterSender.Iron);
//        }

//        [TestMethod]
//        [TestCategory("Client - Trade Offer")]
//        public void DeclineTradeOffetTest()
//        {
//            int ironAmount = 10;
//            int stoneAmount = 50;
//            sender.SendTradeOfferToPlayer(new TradeOffer(sender.player.Name, reciever.player.Name,
//                ResourceType.Stone, stoneAmount, ResourceType.Iron, ironAmount));
//            sender.allTradeOffers = DatabaseInterface.ReadAllTradeOffers(reciever.player.Name);
//            TradeOffer offer = sender.allTradeOffers.Find(delegate (TradeOffer o) { return o.RecieverName == reciever.player.Name; });

//            // Get new resource values
//            Player afterReciever = DatabaseInterface.ReadPlayer(reciever.player.Name);
//            Player afterSender = DatabaseInterface.ReadPlayer(sender.player.Name);
//        }

//    }
//}
