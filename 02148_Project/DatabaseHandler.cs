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
    public static class DatabaseHandler
    {
        private const string connectionString = @"Data Source=DESKTOP-E0GOLC2\SQLEXPRESS;Initial Catalog=nacmo_db;User ID=oliver;Password=zaq1xsw2";
        private static SqlConnection connection;

        /// <summary>
        /// Create a new SqlConnection with the given connection string and open it
        /// </summary>
        public static void OpenConnection()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        /// <summary>
        /// Close the connection to the database, and set the object to null
        /// </summary>
        public static void CloseConnection()
        {
            connection.Close();
            connection = null;
        }


        /// <summary>
        /// Create a player from the parsed name.
        /// Throws a SQL exception, if the name allready exsits in the table
        /// </summary>
        /// <param name="name">Name of the player</param>
        public static void CreatePlayer(string name)
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
        public static SqlDataReader ReadPlayers()
        {
            OpenConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM Players", connection);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Place a resource on the marketsplace
        /// </summary>
        public static int PlaceResources(string sellerName, int resource, int count, int price)
        {
            OpenConnection();
            string query = "INSERT INTO Market (SellerName, ResourceType, Count, Price) "
                + "OUTPUT INSERTED.Id "
                + "VALUES (@Name, @Resource, @Count, @Price);";
   
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", sellerName);
            command.Parameters.AddWithValue("@Resource", resource);
            command.Parameters.AddWithValue("@Count", count);
            command.Parameters.AddWithValue("@Price", price);
            return (int) command.ExecuteScalar();
        }

        /// <summary>
        /// Read all the resource from the market
        /// </summary>
        /// <returns>A SQL reader object with the result data</returns>
        public static SqlDataReader ReadResourcesOnMarket()
        {
            OpenConnection();
            string query = "SELECT * "
                + "FROM Market "
                + "LEFT JOIN Players On Market.SellerName = Players.Name;";
            SqlCommand command = new SqlCommand(query, connection);

            return command.ExecuteReader();
        }

        /// <summary>
        /// Get a resource from the market by reading it and then deleting it. 
        /// </summary>
        /// <param name="id">Id of the resource offer to get</param>
        /// <returns>A SqlDataReader object with the data</returns>
        public static SqlDataReader GetResourceOnMarket(int id)
        {
            OpenConnection();
            string query = "DELETE FROM Market OUTPUT DELETED.* WHERE Id = " + id;
            SqlCommand command = new SqlCommand(query, connection);

            return command.ExecuteReader();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sellerName"></param>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <param name="price"></param>
        /// <param name="highestBidder"></param>
        /// <param name="highestBid"></param>
        public static void UpdateResourceOffer(int id, string sellerName, ResourceType type, 
            int count, int price, string highestBidder, int highestBid)
        {
            OpenConnection();
            string query = "UPDATE Market "
                + "SET SellerName = @SellerName, ResourceType = @Type, "
                + "Count = @Count, Price = @Price, HighestBidder = @Bidder, Bid = @Bid "
                + "WHERE Market.Id = " + id + ";";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SellerName", sellerName);
            command.Parameters.AddWithValue("@Type", type);
            command.Parameters.AddWithValue("@Count", count);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@Bidder", highestBidder ?? Convert.DBNull);
            command.Parameters.AddWithValue("@Bid", highestBid);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static SqlDataReader GetTradeOffer(int id)
        {
            OpenConnection();
            string query = "DELETE FROM TradeOffers OUTPUT DELETED.* WHERE Id = " + id;
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader();
        }

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
            string query = "INSERT INTO TradeOffers (SellerName, RecieverName, ResourceType, Count, Price) "
                + "VALUES (@Seller, @Reciever, @Type, @Count, @Price);";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Seller", offer.SellerName);
            command.Parameters.AddWithValue("@Reciever", offer.RecieverName);
            command.Parameters.AddWithValue("@Type", offer.Type);
            command.Parameters.AddWithValue("@Count", offer.Count);
            command.Parameters.AddWithValue("@Price", offer.Price);
            command.ExecuteNonQuery();
        }

        public static void SendMessage(Message msg)
        {
            OpenConnection();
            string query = "INSERT INTO Chat (Message, SenderName, RecieverName, ToAll) "
                + "VALUES (@Message, @Sender, @Reciever, @ToAll);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Message", msg.Context);
            command.Parameters.AddWithValue("@Sender", msg.SenderName);
            command.Parameters.AddWithValue("@Reciever", msg.RecieverName);
            command.Parameters.AddWithValue("@ToAll", msg.ToAll);
            command.ExecuteNonQuery();
        }

        public static SqlDataReader GetMessage(string reciever)
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
