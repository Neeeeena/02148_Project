using _02148_Project.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using _02148_Project.Model.Exceptions;
using System;

namespace _02148_Project
{
    public static class DatabaseInterface
    {
        private const string connectionString = @"Data Source=ALEX-PC;Initial Catalog=UseThis;User ID=fuk;Password=fuk";
        //private const string connectionString = @"Data Source=DESKTOP-E0GOLC2\SQLEXPRESS;Initial Catalog=nacmo_db;User ID=oliver;Password=zaq1xsw2;Max Pool Size = 1000;Connect Timeout=30";
        //private const string connectionString = @"Data Source=SURFACE\SQLDatabase;Initial Catalog=VillageRush;User ID=local;Password=1234;Max Pool Size=1000";

        #region ConvertMethods
        /// <summary>
        /// Get a resource offer from a data reader object
        /// </summary>
        /// <param name="reader">SQL Data reader object with the relevant data</param>
        /// <returns>The Resource offer from the reader object</returns>
        internal static ResourceOffer GetResourceOfferFromReader(SqlDataReader reader)
        {
            if (reader.IsDBNull(5))
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
        internal static Player GetPlayerFromReader(SqlDataReader reader)
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
        internal static TradeOffer GetTradeOfferFromReader(SqlDataReader reader)
        {
            return new TradeOffer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                (ResourceType)reader.GetInt32(3), reader.GetInt32(4), (ResourceType) reader.GetInt32(5), reader.GetInt32(6));
        }
        #endregion

        #region ErrorHandling
        /// <summary>
        /// Used for generel exception handling for sql exceptions from the handler 
        /// </summary>
        /// <param name="ex">Exception to handel</param>
        private static void SqlExceptionHandling(SqlException ex)
        {
            if (ex.ErrorCode == 26)
            {
                //ConnectionException.Error.UnableToLocateDatabase
                throw new ConnectionException("Unable to locate the database");
            }
            else if (ex.Number == -2146232060)
            {
                //ConnectionException.Error.LoginFailure
                throw new ConnectionException("Login failed");
            }
            else if (ex.Number == 267)
            {
                throw new ConnectionException("Object can not be found");
            }
            else
            {
                throw new ConnectionException("Unknown error occured");
            }
        }
        #endregion

        #region Player
        /// <summary>
        /// Read all the players from the database as objects 
        /// </summary>
        /// <returns>A list of Players</returns>
        public static List<Player> ReadAllPlayers()
        {
            List<Player> players = new List<Player>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Players";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    players.Add(GetPlayerFromReader(reader));
                                }
                            }
                            return players;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
            return players;
        }


        /// <summary>
        /// Read a players data from the database, and returns the player object
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <returns>A player object with all the relevant data</returns>
        public static Player ReadPlayer(string name)
        {
            Player player = null;
            try
            {
                string query = "SELECT * FROM Players WHERE Name = @Name;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                player = GetPlayerFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
            if (player == null) throw new PlayerException("Player not found", name);
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Players "
                        + "SET " + type.ToString() + " = " + type.ToString() + " + @Count "
                        + "WHERE Name = @Name;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Count", count);
                        command.ExecuteNonQuery();
                    }
                }
            } 
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Players "
                        + "SET Wood = @Wood, Clay = @Clay, Wool = @Wool, "
                        + "Stone = @Stone, Iron = @Iron, Straw = @Straw, "
                        + "Food = @Food, Gold = @Gold "
                        + "WHERE Name = @Name;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Wood", player.Wood);
                        command.Parameters.AddWithValue("@Clay", player.Clay);
                        command.Parameters.AddWithValue("@Wool", player.Wool);
                        command.Parameters.AddWithValue("@Stone", player.Stone);
                        command.Parameters.AddWithValue("@Iron", player.Iron);
                        command.Parameters.AddWithValue("@Straw", player.Straw);
                        command.Parameters.AddWithValue("@Food", player.Food);
                        command.Parameters.AddWithValue("@Gold", player.Gold);
                        command.Parameters.AddWithValue("@Name", player.Name);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Players (Name) VALUES (@Name);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new PlayerException("Username already taken", name);
                }
                else
                {
                    SqlExceptionHandling(ex); 
                }
            }
        }

        /// <summary>
        /// Delete a player with the parsed username
        /// </summary>
        /// <param name="name">User to delete</param>
        public static void DeletePlayer(string name)
        {
            try
            {
                string query = "DELETE FROM Players WHERE Name = @Name;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.ExecuteNonQuery();
                    }
                }
            } 
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
        }

        #endregion

        #region ResourceOffer
        /// <summary>
        /// Read a resource offer on the market, without removing it
        /// </summary>
        /// <param name="id">Id of the resource offer to read</param>
        /// <returns>The offer from the database</returns>
        public static ResourceOffer ReadResourceOffer(int id)
        {
            string query = "SELECT * FROM Market WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return GetResourceOfferFromReader(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            //SqlDataReader reader = DatabaseHandler.ReadResourceOnMarket(id);
            //ResourceOffer offer = null;
            //if (reader.HasRows)
            //{
            //    reader.Read();
            //    offer = GetResourceOfferFromReader(reader);
            //}
            //reader.Dispose();
            //DatabaseHandler.CloseConnection();
            //return offer;
        }

        /// <summary>
        /// Read a list of resource offers from the market
        /// </summary>
        /// <returns>A list of resources offers</returns>
        public static List<ResourceOffer> ReadAllResourceOffers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Market ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<ResourceOffer> offer = new List<ResourceOffer>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                offer.Add(GetResourceOfferFromReader(reader));
                            }
                        }
                        return offer;
                    }
                }
            }
        }

        /// <summary>
        /// Update a resource offer on the market
        /// </summary>
        /// <param name="offer">Offer to update</param>
        public static void UpdateResourceOffer(ResourceOffer offer)
        {
            try
            {
                string query = "UPDATE Market "
                    + "SET SellerName = @SellerName, ResourceType = @Type, "
                    + "Count = @Count, Price = @Price, HighestBidder = @Bidder, Bid = @Bid "
                    + "WHERE Market.Id = " + offer.Id + " AND Market.Bid < @Bid;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SellerName", offer.SellerName);
                        command.Parameters.AddWithValue("@Type", offer.Type);
                        command.Parameters.AddWithValue("@Count", offer.Count);
                        command.Parameters.AddWithValue("@Price", offer.Price);
                        command.Parameters.AddWithValue("@Bidder", offer.HighestBidder ?? Convert.DBNull);
                        command.Parameters.AddWithValue("@Bid", offer.HighestBid);

                        // If no rows where updated 
                        if (command.ExecuteNonQuery() == 0)
                        {
                            throw new ResourceOfferException("Unable to bid on ressource. Either it is gone or your bid was to low", offer);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
        }

        /// <summary>
        /// Get the resource offer from the market 
        /// </summary>
        /// <param name="id">Id of the resource offer to remove</param>
        /// <returns>The resource offer from the database</returns>
        public static ResourceOffer GetResourceOffer(int id)
        {
            string query = "DELETE FROM Market OUTPUT DELETED.* WHERE Id = " + id;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return GetResourceOfferFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
            return null;
        }

        /// <summary>
        /// Put a resource offer on the market
        /// </summary>
        /// <param name="offer">Offer to place on the market</param>
        public static int PutResourceOfferOnMarket(ResourceOffer offer)
        {
            try
            {
                string query = "INSERT INTO Market (SellerName, ResourceType, Count, Price) "
                    + "OUTPUT INSERTED.Id "
                    + "VALUES (@Name, @Resource, @Count, @Price);";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", offer.SellerName);
                        command.Parameters.AddWithValue("@Resource", offer.Type);
                        command.Parameters.AddWithValue("@Count", offer.Count);
                        command.Parameters.AddWithValue("@Price", offer.Price);

                        return (int)command.ExecuteScalar();
                    }
                }

            }
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
            return 0;
        }
        #endregion

        #region TradeOffer
        /// <summary>
        /// Get a list of all the tradeoffers in the database to a given user
        /// </summary>
        /// <param name="reciever">Name of the reciever to the tradeoffer</param>
        /// <returns>A list of tradeoffers</returns>
        public static List<TradeOffer> ReadAllTradeOffers(string reciever)
        {
            List<TradeOffer> offers = new List<TradeOffer>();
            try
            {
                string query = "SELECT * FROM TradeOffers WHERE RecieverName = '" + reciever + "';";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    offers.Add(GetTradeOfferFromReader(reader));
                                }
                            }
                            return offers;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
            return offers;
        }

        /// <summary>
        /// Read alll trade offers send by the given user
        /// </summary>
        /// <param name="sender">Name of user how have send trade offer</param>
        /// <returns>A list of trade offers, which the user has send</returns>
        public static List<TradeOffer> ReadAllSendTradeOffers(string sender)
        {
            string query = "SELECT * FROM TradeOffers WHERE SellerName = '" + sender + "';";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<TradeOffer> offers = new List<TradeOffer>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                offers.Add(GetTradeOfferFromReader(reader));
                            }
                        }
                        return offers;
                    }
                }
            }
        }


        /// <summary>
        /// Get a tradeoffer from the database. This will remove it
        /// </summary>
        /// <param name="id">Of the trade offer</param>
        /// <returns>The requested tradeoffer</returns>
        public static TradeOffer GetTradeOffer(int id)
        {
            TradeOffer offer = null;
            try
            {
                string query = "DELETE FROM TradeOffers OUTPUT DELETED.* WHERE Id = " + id;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                offer = GetTradeOfferFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
            return offer;
        }
        
        /// <summary>
        /// Put a trade offer on to the market
        /// </summary>
        /// <param name="offer"></param>
        public static int PutTradeOffer(TradeOffer offer)
        {
            try
            {
                string query = "INSERT INTO TradeOffers (SellerName, RecieverName, ResourceType, Count, PriceType, Price) "
                    + "OUTPUT INSERTED.Id "
                    + "VALUES (@Seller, @Reciever, @Type, @Count, @PriceType, @Price);";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Seller", offer.SellerName);
                        command.Parameters.AddWithValue("@Reciever", offer.RecieverName);
                        command.Parameters.AddWithValue("@Type", offer.Type);
                        command.Parameters.AddWithValue("@Count", offer.Count);
                        command.Parameters.AddWithValue("@PriceType", offer.PriceType);
                        command.Parameters.AddWithValue("@Price", offer.Price);

                        return (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
            return 0;
        }
        #endregion

        #region Messages
        /// <summary>
        /// Send a message to the server
        /// </summary>
        /// <param name="msg">Message to send</param>
        public static void SendMessage(Message msg)
        {
            try
            {
                string query = "INSERT INTO Chat (Message, SenderName, RecieverName) "
                    + "VALUES (@Message, @Sender, @Reciever);";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Message", msg.Context);
                        command.Parameters.AddWithValue("@Sender", msg.SenderName);
                        command.Parameters.AddWithValue("@Reciever", msg.RecieverName);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
        }

        /// <summary>
        /// Get/reciev a message from the server to the specifed reciever
        /// </summary>
        /// <param name="reciever">Name of the reciever</param>
        /// <returns>The latest message to the reciever</returns>
        public static Message GetMessage(string reciever)
        {
            try
            {
                string query = "WITH toprow AS (SELECT TOP 1 * FROM Chat "
                    + "WHERE RecieverName = '" + reciever + "' ORDER BY Id ASC) "
                    + "DELETE FROM toprow "
                    + "OUTPUT DELETED.*;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return new Message(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                SqlExceptionHandling(ex);
            }
            return null;
        }
        #endregion

        #region Notifications
        public delegate void OnChange_Player(object sender, SqlNotificationEventArgs e);
        public delegate void OnChange_ResourceOffers(object sender, SqlNotificationEventArgs e);
        public delegate void OnChange_TradeOffers(object sender, SqlNotificationEventArgs e);
        public delegate void OnChange_Chat(object sender, SqlNotificationEventArgs e);

        /// <summary>
        /// Start the sql dependency class, so event listeners can be setup
        /// </summary>
        public static void DependencyInitialization()
        {
            SqlDependency.Start(connectionString);
        }

        /// <summary>
        /// Stop all event listeners to the SQL database
        /// </summary>
        public static void DependencyTermination()
        {
            SqlDependency.Stop(connectionString);
        }

        /// <summary>
        /// Setup all the listeners for the database tables
        /// </summary>
        public static void SetupDatabaseListeners(OnChange_Player player, OnChange_ResourceOffers resourceOffer, 
            OnChange_TradeOffers tradeOffer, OnChange_Chat chat)
        {
            DependencyInitialization();
            MonitorPlayers(player);
            MonitorResourceOffers(resourceOffer);
            MonitorTradeOffer(tradeOffer);
            MonitorChat(chat);
        }

        /// <summary>
        /// Setup listener for the player table
        /// </summary>
        /// <param name="player">The event handler methode which should be run when the event fires</param>
        public static void MonitorPlayers(OnChange_Player playerMethode)
        {
            try
            {
                string query = "SELECT Name, Wood, Clay, Wool, " +
                    "Stone, Iron, Straw, Food, Gold FROM dbo.Players";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDependency dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(playerMethode);
                        command.ExecuteNonQuery();
                    }
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (SqlException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            { }
        }

        /// <summary>
        /// Setup listener for the resource offer table
        /// </summary>
        /// <param name="resourceOfferMethode"></param>
        public static void MonitorResourceOffers(OnChange_ResourceOffers resourceOfferMethode)
        {
            try
            {
                string query = "SELECT Id, HighestBidder, Bid FROM dbo.Market";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDependency dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(resourceOfferMethode);
                        command.ExecuteNonQuery();
                    }
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (SqlException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            { }
        }

        /// <summary>
        /// Setup listener for the trade offer 
        /// </summary>
        /// <param name="tradeOfferMethode">Event methode to be run when event fires</param>
        public static void MonitorTradeOffer(OnChange_TradeOffers tradeOfferMethode)
        {
            try
            {
                string query = "SELECT Id FROM dbo.TradeOffers;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDependency dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(tradeOfferMethode);
                        command.ExecuteNonQuery();
                    }
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (SqlException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            { }
        }

        /// <summary>
        /// Setup listener for the chat table
        /// </summary>
        /// <param name="chatMethode"></param>
        public static void MonitorChat(OnChange_Chat chatMethode)
        {
            try
            {
                string query = "SELECT Id FROM dbo.Chat";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDependency dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(chatMethode);
                        command.ExecuteNonQuery();
                    }
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (SqlException ex) { }
#pragma warning restore CS0168 // Variable is declared but never used
        }
        #endregion
    }
}
