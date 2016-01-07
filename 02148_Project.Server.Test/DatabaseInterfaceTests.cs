using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _02148_Project;
using System.Data.SqlClient;

namespace _02148_Project.Server.Test
{
    [TestClass]
    public class DatabaseInterfaceTests
    {
        [ClassInitialize]
        public static void Before(TestContext context)
        {
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            DatabaseInterface.CloseConnection();
            Console.WriteLine("Connection clossed");
        }
        
        [TestMethod]
        public void GetPlayersTest()
        {
            SqlDataReader reader = DatabaseInterface.ReadPlayers();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}", reader.GetString(0));
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
            Console.WriteLine(DatabaseInterface.PlaceResources("Oliver", 5, 200, 150));
        }

        [TestMethod]
        public void GetResourcesFromMarketTest()
        {
            SqlDataReader reader = DatabaseInterface.ReadResourcesOnMarket();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.IsDBNull(5))
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader.GetInt32(0),
                            reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3),
                            reader.GetInt32(4));
                    } 
                    else
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", reader.GetInt32(0),
                            reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3),
                            reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6));
                    }
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
