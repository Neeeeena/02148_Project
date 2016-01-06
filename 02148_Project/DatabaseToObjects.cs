using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using _02148_Project.Model;

namespace _02148_Project
{
    public static class DatabaseToObjects
    {
        /// <summary>
        /// Get all the players from the database as objects 
        /// </summary>
        /// <returns>A list of Players</returns>
        public static List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>();
            DatabaseInterface.OpenConnection();
            SqlDataReader reader = DatabaseInterface.GetPlayers();

            // Get all the players from the results
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    players.Add(new Player(reader.GetString(0)));
                }
            }
            reader.Close();
            return players;
        }

        /// <summary>
        /// Read a list of resource offers from the market
        /// </summary>
        /// <returns>A list of resources offers</returns>
        public static List<ResourceOffer> ReadResourceOffers()
        {
            List<ResourceOffer> offers = new List<ResourceOffer>();
            DatabaseInterface.OpenConnection();
            SqlDataReader reader = DatabaseInterface.ReadResourcesOnMarket();

            if (reader.HasRows) // Check if the reader has any results. 
            {
                while (reader.Read())
                {
                    // Check the highest bidder for null value
                    if (reader.IsDBNull(5))
                    {
                        offers.Add(new ResourceOffer(reader.GetInt32(0),
                            reader.GetString(1), (ResourceType)reader.GetInt32(2),
                            reader.GetInt32(3), reader.GetInt32(4)));
                    }
                    else
                    {
                        offers.Add(new ResourceOffer(reader.GetInt32(0),
                            reader.GetString(1), (ResourceType)reader.GetInt32(2),
                            reader.GetInt32(3), reader.GetInt32(4), 
                            reader.GetString(5), reader.GetInt32(6)));
                    }
                }
            }
            reader.Close();
            return offers;
        }

        /// <summary>
        /// Get the resource offer from the market 
        /// </summary>
        /// <param name="id">Id of the resource offer to remove</param>
        /// <returns>The resource offer from the database</returns>
        public static ResourceOffer GetResourceOffer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
