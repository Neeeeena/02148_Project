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
        public void CreatePlayerTest()
        {
            DatabaseInterface.CreatePlayer("Ole");
        }
    }
}
