using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using _02148_Project.Model;

namespace _02148_Project
{
    internal static class DatabaseHandler
    {
        //private const string connectionString = @"Data Source=DESKTOP-E0GOLC2\SQLEXPRESS;Initial Catalog=nacmo_db;User ID=oliver;Password=zaq1xsw2";
        private const string connectionString = @"Data Source=SURFACE\SQLDatabase;Initial Catalog=master;User ID=local;Password=1234";
        private static SqlConnection connection;

        /// <summary>
        /// Create a new SqlConnection with the given connection string and open it
        /// </summary>
        internal static void OpenConnection()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        /// <summary>
        /// Close the connection to the database, and set the object to null
        /// </summary>
        internal static void CloseConnection()
        {
            if (connection != null)
            { 
                connection.Close();
                connection = null;
            }
        }

        /// <summary>
        /// Create a player from the parsed name.
        /// Throws a SQL exception, if the name allready exsits in the table
        /// </summary>
        /// <param name="name">Name of the player</param>
        internal static void CreatePlayer(string name)
        {
            OpenConnection();
            string query = "INSERT INTO Players (Name) VALUES (@Name);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Get all the players from the database
        /// </summary>
        /// <returns>Rows from the database with player data</returns>
        internal static SqlDataReader ReadAllPlayers()
        {
            OpenConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM Players", connection);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Place a resource on the marketsplace
        /// </summary>
        internal static int PlaceResources(ResourceOffer offer)
        {
            OpenConnection();
            string query = "INSERT INTO Market (SellerName, ResourceType, Count, Price) "
                + "OUTPUT INSERTED.Id "
                + "VALUES (@Name, @Resource, @Count, @Price);";
   
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", offer.SellerName);
            command.Parameters.AddWithValue("@Resource", offer.Type);
            command.Parameters.AddWithValue("@Count", offer.Count);
            command.Parameters.AddWithValue("@Price", offer.Price);
            return (int) command.ExecuteScalar();
        }

        /// <summary>
        /// Update the player data in the database
        /// </summary>
        /// <param name="player">Player to update</param>
        internal static void UpdatePlayerData(Player player)
        {
            OpenConnection();
            string query = "UPDATE Players "
                + "SET Wood = @Wood, Clay = @Clay, Wool = @Wool, "
                + "Stone = @Stone, Iron = @Iron, Straw = @Straw, "
                + "Food = @Food, Gold = @Gold "
                + "WHERE Name = @Name;";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Wood", player.Wood);
            command.Parameters.AddWithValue("@Clay", player.Clay);
            command.Parameters.AddWithValue("@Wool", player.Wool);
            command.Parameters.AddWithValue("@Stone", player.Stone);
            command.Parameters.AddWithValue("@Iron", player.Iron);
            command.Parameters.AddWithValue("@Straw", player.Straw);
            command.Parameters.AddWithValue("@Food", player.Food);
            command.Parameters.AddWithValue("@Gold", player.Gold);
            command.Parameters.AddWithValue("@Name", player.Name);

            command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Reads all the data about a player in the database and returns the 
        /// SQL data reader object with the data
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <returns>The data reader object with the returned data</returns>
        internal static SqlDataReader ReadPlayerData(string name)
        {
            OpenConnection();
            string query = "SELECT * FROM Players WHERE Name = @Name;";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", name);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Read all the resource from the market
        /// </summary>
        /// <returns>A SQL reader object with the result data</returns>
        internal static SqlDataReader ReadAllResourcesOnMarket()
        {
            OpenConnection();
            string query = "SELECT * "
                + "FROM Market "
                + "LEFT JOIN Players On Market.SellerName = Players.Name;";
            SqlCommand command = new SqlCommand(query, connection);

            return command.ExecuteReader();
        }

        /// <summary>
        /// Read the data of a resource offer on the market
        /// </summary>
        /// <param name="id">Of the offer to read</param>
        /// <returns>A SQL data reader object with the query result</returns>
        internal static SqlDataReader ReadResourceOnMarket(int id)
        {
            OpenConnection();
            string query = "SELECT * FROM Market WHERE Id = @Id;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Get a resource from the market by reading it and then deleting it. 
        /// </summary>
        /// <param name="id">Id of the resource offer to get</param>
        /// <returns>A SqlDataReader object with the data</returns>
        internal static SqlDataReader GetResourceOnMarket(int id)
        {
            OpenConnection();
            string query = "DELETE FROM Market OUTPUT DELETED.* WHERE Id = " + id;
            SqlCommand command = new SqlCommand(query, connection);

            return command.ExecuteReader();
        }

        /// <summary>
        /// Update a resource offer with new data. Used when placing a new bid on an offer
        /// </summary>
        /// <param name="id">Of the offer</param>
        /// <param name="sellerName">The name of the seller</param>
        /// <param name="type">Type of resource on sale</param>
        /// <param name="count">Number of items on sale</param>
        /// <param name="price">Requested price of the resources</param>
        /// <param name="highestBidder">The name of the currents bidder, who is going to win the offer</param>
        /// <param name="highestBid">The bid on the resources</param>
        internal static void UpdateResourceOffer(ResourceOffer offer)
        {
            OpenConnection();
            string query = "UPDATE Market "
                + "SET SellerName = @SellerName, ResourceType = @Type, "
                + "Count = @Count, Price = @Price, HighestBidder = @Bidder, Bid = @Bid "
                + "WHERE Market.Id = " + offer.Id + ";";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SellerName", offer.SellerName);
            command.Parameters.AddWithValue("@Type", offer.Type);
            command.Parameters.AddWithValue("@Count", offer.Count);
            command.Parameters.AddWithValue("@Price", offer.Price);
            command.Parameters.AddWithValue("@Bidder", offer.HighestBidder ?? Convert.DBNull);
            command.Parameters.AddWithValue("@Bid", offer.HighestBid);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Get a trade offer from the database by deleting it and returning the data
        /// </summary>
        /// <param name="id">Id of the offer</param>
        /// <returns>A SQL data reader object with the results from the query</returns>
        internal static SqlDataReader GetTradeOffer(int id)
        {
            OpenConnection();
            string query = "DELETE FROM TradeOffers OUTPUT DELETED.* WHERE Id = " + id;
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Reads all the tradeoffers from the database and returns the reader object from the 
        /// sql query
        /// </summary>
        /// <param name="reciever">Name of the reciever</param>
        /// <returns>The data reader object with results from the query</returns>
        internal static SqlDataReader ReadAllTradeOffers(string reciever)
        {
            OpenConnection();
            string query = "SELECT * FROM TradeOffers WHERE RecieverName = '" + reciever + "';";
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Place a trade offer on the market
        /// </summary>
        /// <param name="offer">The offer to place on the market</param>
        internal static void PlaceTradeOffer(TradeOffer offer)
        {
            OpenConnection();
            string query = "INSERT INTO TradeOffers (SellerName, RecieverName, ResourceType, Count, PriceType, Price) "
                + "VALUES (@Seller, @Reciever, @Type, @Count, @PriceType, @Price);";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Seller", offer.SellerName);
            command.Parameters.AddWithValue("@Reciever", offer.RecieverName);
            command.Parameters.AddWithValue("@Type", offer.Type);
            command.Parameters.AddWithValue("@Count", offer.Count);
            command.Parameters.AddWithValue("@PriceType", offer.PriceType);
            command.Parameters.AddWithValue("@Price", offer.Price);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Sends a message by placing the chat object in the database
        /// </summary>
        /// <param name="msg">Message to send</param>
        internal static void SendMessage(Message msg)
        {
            OpenConnection();
            string query = "INSERT INTO Chat (Message, SenderName, RecieverName) "
                + "VALUES (@Message, @Sender, @Reciever);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Message", msg.Context);
            command.Parameters.AddWithValue("@Sender", msg.SenderName);
            command.Parameters.AddWithValue("@Reciever", msg.RecieverName);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Get a message from the database. This will delete the message from the database
        /// and return the deleted data
        /// </summary>
        /// <param name="reciever">Name of the reciever for whom the message should be</param>
        /// <returns>The data reader object from the sql query</returns>
        internal static SqlDataReader GetMessage(string reciever)
        {
            OpenConnection();
            string query = "WITH toprow AS (SELECT TOP 1 * FROM Chat "
                + "WHERE RecieverName = '" + reciever + "' ORDER BY Id ASC) "
                + "DELETE FROM toprow "
                + "OUTPUT DELETED.*;";
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader();
        }
    }
}
