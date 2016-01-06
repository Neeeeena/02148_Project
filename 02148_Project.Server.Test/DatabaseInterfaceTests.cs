using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _02148_Project;
using System.Data.SqlClient;

namespace _02148_Project.Server.Test
{
    [TestClass]
    public class DatabaseInterfaceTests
    {
        private static DatabaseInterface dbi;

        [ClassInitialize]
        public static void Before(TestContext context)
        {
            dbi = new DatabaseInterface();
            dbi.OpenConnection();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            dbi.CloseConnection();
            Console.WriteLine("Connection clossed");
        }

        //[TestMethod]
        //public void ConnectionTest()
        //{
        //    dbi = new DatabaseInterface();
        //    dbi.OpenConnection();
        //    Console.WriteLine("Connection is open");
        //    dbi.CloseConnection();
        //    Console.WriteLine("Connection is now closed");
        //}

        [TestMethod]
        public void GetPlayersTest()
        {
            SqlDataReader reader = dbi.GetPlayers();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}", reader.GetInt32(0),
                        reader.GetString(1));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
        }

        [TestMethod]
        public void PlaceMarketTest()
        {
            dbi.PlaceResources(1, 5, 200, 150);
        }

        [TestMethod]
        public void GetResourcesFromMarketTest()
        {
            SqlDataReader reader = dbi.ReadResourcesOnMarket();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", reader.GetInt32(0),
                        reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), 
                        reader.GetInt32(4), reader.GetString(5));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
        }
    }
}
