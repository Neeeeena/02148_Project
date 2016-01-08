using _02148_Project.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using _02148_Project.Model.Exceptions;
using System;

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
            reader.Read();
            if (reader.IsDBNull(6))
            {
                return new ResourceOffer(reader.GetInt32(0), reader.GetString(1), (ResourceType)reader.GetInt32(2),
                    reader.GetInt32(3), reader.GetInt32(4));
            }
            else
            {
                return new ResourceOffer(reader.GetInt32(0), reader.GetString(1), (ResourceType)reader.GetInt32(2),
                   reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6));
            }
        }

        /// <summary>
        /// Get a player object from the reader
        /// </summary>
        /// <param name="reader">Reader with the data</param>
        /// <returns>The player in the reader object</returns>
        private static Player GetPlayerFromReader(SqlDataReader reader)
        {
            return new Player(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2),
                                reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5),
                                reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8));
        }

        /// <summary>
        /// Get a tradeoffer objet from the reader results
        /// </summary>
        /// <param name="reader">Reader with the data</param>
        /// <returns>A trade offer object</returns>
        private static TradeOffer GetTradeOfferFromReader(SqlDataReader reader)
        {
            return new TradeOffer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                (ResourceType)reader.GetInt32(3), reader.GetInt32(4), (ResourceType) reader.GetInt32(5), reader.GetInt32(6));
        }

        /// <summary>
        /// Read all the players from the database as objects 
        /// </summary>
        /// <returns>A list of Players</returns>
        public static List<Player> ReadAllPlayers()
        {
            List<Player> players = new List<Player>();
            DatabaseHandler.OpenConnection();
            SqlDataReader reader = DatabaseHandler.ReadAllPlayers();

            // Get all the players from the results
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    players.Add(GetPlayerFromReader(reader));
                }
            }
            reader.Dispose();
            DatabaseHandler.CloseConnection();
            return players;
        }


        /// <summary>
        /// Read a players data from the database, and returns the player object
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <returns>A player object with all the relevant data</returns>
        public static Player ReadPlayer(string name)
        {
            SqlDataReader reader = DatabaseHandler.ReadPlayerData(name);
            Player player = null;
            if (reader.HasRows)
            {
                reader.Read();
                player = GetPlayerFromReader(reader);
            }
            reader.Dispose();
            DatabaseHandler.CloseConnection();
            return player;
        }

        /// <summary>
        /// Update a specific resource type for a player
        /// </summary>
        /// <param name="name">Name of the player to update</param>
        /// <param name="type">Type of resource to update</param>
        /// <param name="count">Amount to add to the players resources</param>
        public static void UpdatePlayerResource(string name, ResourceType type, int count)
        {
            try
            {
                DatabaseHandler.UpdatePlayerResource(name, type, count);
            } 
            catch (SqlException ex)
            {
                if (ex.ErrorCode == 26)
                {
                    throw new ConnectionException("Unable to locate database");
                }
                else
                {
                    throw new ConnectionException("The resources was not updated due to an server error");
                }
            }
            finally
            {
                DatabaseHandler.CloseConnection();
            }
        }

        /// <summary>
        /// Update a player and all his resources
        /// </summary>
        /// <param name="player">Player to update in the database</param>
        public static void UpdatePlayer(Player player)
        {
            try
            {
                DatabaseHandler.UpdatePlayerData(player);
            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == 26)
                {
                    throw new ConnectionException("Unable to connect to the database", player);
                } 
                else
                {
                    throw new ConnectionException("Unable to update the player", player);
                }
            }
            finally
            {
                DatabaseHandler.CloseConnection();
            }
        }

        /// <summary>
        /// Create a player with the given name
        /// </summary>
        /// <param name="name">Name of the player</param>
        public static void PutPlayer(string name)
        {
            try
            {
                DatabaseHandler.CreatePlayer(name);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new PlayerException("Username already taken", name);
                }
                else if (ex.ErrorCode == 26)
                {
                    throw new ConnectionException(ConnectionException.Error.UnableToLocateDatabase);
                }
                else
                {
                    throw new Exception("Unknown error occurred");
                }
            }
            finally
            {
                DatabaseHandler.CloseConnection();
            }
        }

        /// <summary>
        /// Read a resource offer on the market, without removing it
        /// </summary>
        /// <param name="id">Id of the resource offer to read</param>
        /// <returns>The offer from the database</returns>
        public static ResourceOffer ReadResourceOffer(int id)
        {
            SqlDataReader reader = DatabaseHandler.ReadResourceOnMarket(id);
            ResourceOffer offer = null;
            if (reader.HasRows)
            {
                offer = GetResourceOfferFromReader(reader);
            }
            reader.Dispose();
            DatabaseHandler.CloseConnection();
            return offer;
        }

        /// <summary>
        /// Read a list of resource offers from the market
        /// </summary>
        /// <returns>A list of resources offers</returns>
        public static List<ResourceOffer> ReadAllResourceOffers()
        {
            List<ResourceOffer> offers = new List<ResourceOffer>();
            SqlDataReader reader = DatabaseHandler.ReadAllResourcesOnMarket();

            if (reader.HasRows) // Check if the reader has any results. 
            {
                while (reader.Read())
                {
                    offers.Add(GetResourceOfferFromReader(reader));
                }
            }
            reader.Dispose();
            DatabaseHandler.CloseConnection();
            return offers;
        }

        /// <summary>
        /// Update a resource offer on the market
        /// </summary>
        /// <param name="offer">Offer to update</param>
        public static void UpdateResourceOffer(ResourceOffer offer)
        {
            try
            {
                DatabaseHandler.UpdateResourceOffer(offer);
            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == 26)
                {
                    throw new ConnectionException(ConnectionException.Error.UnableToLocateDatabase);
                }
                else
                { 
                    throw new Exception("Unknown error occurred");
                }
            }
            finally
            {
                DatabaseHandler.CloseConnection();
            }
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
            reader.Dispose();
            DatabaseHandler.CloseConnection();
            return offer;
        }

        /// <summary>
        /// Get a list of all the tradeoffers in the database to a given user
        /// </summary>
        /// <param name="reciever">Name of the reciever to the tradeoffer</param>
        /// <returns>A list of tradeoffers</returns>
        public static List<TradeOffer> ReadAllTradeOffers(string reciever)
        {
            SqlDataReader reader = DatabaseHandler.ReadAllTradeOffers(reciever);
            List<TradeOffer> offers = new List<TradeOffer>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    offers.Add(GetTradeOfferFromReader(reader));
                }
            }
            reader.Dispose();
            DatabaseHandler.CloseConnection();
            return offers;
        }

        /// <summary>
        /// Get a tradeoffer from the database. This will remove it
        /// </summary>
        /// <param name="id">Of the trade offer</param>
        /// <returns>The requested tradeoffer</returns>
        public static TradeOffer GetTradeOffer(int id)
        {
            SqlDataReader reader = DatabaseHandler.GetTradeOffer(id);
            TradeOffer offer = null;
            if (reader.HasRows)
            {
                reader.Read();
                offer = GetTradeOfferFromReader(reader);
            }
            reader.Dispose();
            DatabaseHandler.CloseConnection();
            return offer;
        }
        
        /// <summary>
        /// Put a trade offer on to the market
        /// </summary>
        /// <param name="offer"></param>
        public static void PutTradeOffer(TradeOffer offer)
        {
            DatabaseHandler.PlaceTradeOffer(offer);
            DatabaseHandler.CloseConnection();
        }

        /// <summary>
        /// Put a resource offer on the market
        /// </summary>
        /// <param name="offer">Offer to place on the market</param>
        public static int PutResourceOfferOnMarket(ResourceOffer offer)
        {
            int id;
            try
            {
                id = DatabaseHandler.PlaceResources(offer);
            } 
            catch (SqlException ex)
            {
                if (ex.ErrorCode == 26)
                {
                    throw new ConnectionException(ConnectionException.Error.UnableToLocateDatabase);
                } 
                else
                {
                    throw new Exception("Unknown error occurred");
                }
            }
            finally
            {
                DatabaseHandler.CloseConnection();
            }
            return id;
        }

        /// <summary>
        /// Send a message to the server
        /// </summary>
        /// <param name="msg">Message to send</param>
        public static void SendMessage(Message msg)
        {
            try
            {
                DatabaseHandler.SendMessage(msg);
            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == 26)
                {
                    throw new ConnectionException(ConnectionException.Error.UnableToLocateDatabase);
                }
                else
                {
                    throw new MessageException("Uanble to send your message", msg);
                }
            }
            finally
            {
                DatabaseHandler.CloseConnection();
            }
        }

        /// <summary>
        /// Get/reciev a message from the server to the specifed reciever
        /// </summary>
        /// <param name="reciever">Name of the reciever</param>
        /// <returns>The latest message to the reciever</returns>
        public static Message GetMessage(string reciever)
        {
            SqlDataReader reader = DatabaseHandler.GetMessage(reciever);
            Message msg = null;
            if (reader.HasRows)
            {
                reader.Read();
                msg = new Message(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                reader.Dispose();
                DatabaseHandler.CloseConnection();
            }
            else
            {
                throw new MessageException("No message where found", new Message(null, null, reciever));
            }
            return msg;
        }
    }
}
