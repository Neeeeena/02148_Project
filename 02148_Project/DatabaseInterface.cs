using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using _02148_Project.Model;

namespace _02148_Project
{
    public static class DatabaseInterface
    {

        /// <summary>
        /// Get a resource offer from a data reader object
        /// </summary>
        /// <param name="reader">SQL Data reader object with the relevant data</param>
        /// <returns>The Resource offer from the reader object</returns>
        private static ResourceOffer GetResourceOfferFromReader(SqlDataReader reader)
        {
            ResourceOffer offer = null;
            reader.Read();
            if (reader.IsDBNull(5))
            {
                offer = new ResourceOffer(reader.GetInt32(0), reader.GetString(1), (ResourceType)reader.GetInt32(2),
                    reader.GetInt32(3), reader.GetInt32(4));
            }
            else
            {
                offer = new ResourceOffer(reader.GetInt32(0), reader.GetString(1), (ResourceType)reader.GetInt32(2),
                   reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6));
            }
            return offer;
        }

        /// <summary>
        /// Read all the players from the database as objects 
        /// </summary>
        /// <returns>A list of Players</returns>
        public static List<Player> ReadPlayers()
        {
            List<Player> players = new List<Player>();
            DatabaseHandler.OpenConnection();
            SqlDataReader reader = DatabaseHandler.ReadPlayers();

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
        /// Create a player with the given name
        /// </summary>
        /// <param name="name">Name of the player</param>
        public static void PutPlayer(string name)
        {
            DatabaseHandler.CreatePlayer(name);
        }

        public static ResourceOffer ReadResourceOffer(int id)
        {
            SqlDataReader reader = DatabaseHandler.GetResourceOnMarket(id);
            ResourceOffer offer = null;
            if (reader.HasRows)
            {
                offer = GetResourceOfferFromReader(reader);
            }
            return offer;
        }

        /// <summary>
        /// Read a list of resource offers from the market
        /// </summary>
        /// <returns>A list of resources offers</returns>
        public static List<ResourceOffer> ReadAllResourceOffers()
        {
            List<ResourceOffer> offers = new List<ResourceOffer>();
            SqlDataReader reader = DatabaseHandler.ReadResourcesOnMarket();

            if (reader.HasRows) // Check if the reader has any results. 
            {
                while (reader.Read())
                {
                    // Check the highest bidder for null value
                    offers.Add(GetResourceOfferFromReader(reader));
                }
            }
            reader.Close();
            return offers;
        }

        /// <summary>
        /// Update a resource offer on the market
        /// </summary>
        /// <param name="offer">Offer to update</param>
        public static void UpdateResourceOffer(ResourceOffer offer)
        {
            DatabaseHandler.UpdateResourceOffer(offer.Id, offer.SellerName, offer.Type, offer.Count,
                offer.Price, offer.HighestBidder, offer.HighestBid);
        }

        /// <summary>
        /// Get the resource offer from the market 
        /// </summary>
        /// <param name="id">Id of the resource offer to remove</param>
        /// <returns>The resource offer from the database</returns>
        public static ResourceOffer GetResourceOffer(int id)
        {
            SqlDataReader reader = DatabaseHandler.GetResourceOnMarket(id);
            ResourceOffer offer = null;

            if (reader.HasRows)
            {
                offer = GetResourceOfferFromReader(reader);
            }
            return offer;
        }

        public static List<TradeOffer> ReadAllTradeOffers(string reciever)
        {
            SqlDataReader reader = DatabaseHandler.ReadAllTradeOffers(reciever);
            List<TradeOffer> offers = new List<TradeOffer>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    offers.Add(new TradeOffer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        (ResourceType)reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5)));
                }
            }
            reader.Close();
            return offers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TradeOffer GetTradeOffer(int id)
        {
            SqlDataReader reader = DatabaseHandler.GetTradeOffer(id);
            
            if (reader.HasRows)
            {
                reader.Read();
                return new TradeOffer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    (ResourceType)reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
            }
            return null;
        }

        /// <summary>
        /// Put a trade offer on to the market
        /// </summary>
        /// <param name="offer"></param>
        public static void PutTradeOffer(TradeOffer offer)
        {
            DatabaseHandler.PlaceTradeOffer(offer);
        }

        /// <summary>
        /// Put a resource offer on the market
        /// </summary>
        /// <param name="offer">Offer to place on the market</param>
        public static int PutResourceOfferOnMarket(ResourceOffer offer)
        {
            return DatabaseHandler.PlaceResources(offer.SellerName, (int) offer.Type, offer.Count, offer.Price);
        }

        public static void SendMessage(Message msg)
        {
            DatabaseHandler.SendMessage(msg);
        }

        public static Message GetMessage(string reciever)
        {
            SqlDataReader reader = DatabaseHandler.GetMessage(reciever);
            Message msg = null;
            if (reader.HasRows)
            {
                reader.Read();
                msg = new Message(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetBoolean(4));                
            }
            reader.Close();
            return msg;
        }
    }
}
