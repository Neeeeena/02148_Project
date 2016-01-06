using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace _02148_Project
{
    public static class DatabaseInterface
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
        public static SqlDataReader GetPlayers()
        {
            OpenConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM Players", connection);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Place a resource on the marketsplace
        /// </summary>
        public static void PlaceResources(string sellerName, int resource, int count, int price)
        {
            OpenConnection();
            string query = "INSERT INTO Market (SellerName, ResourceType, Count, Price) " +
                "VALUES (@Name, @Resource, @Count, @Price);";
   
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", sellerName);
            command.Parameters.AddWithValue("@Resource", resource);
            command.Parameters.AddWithValue("@Count", count);
            command.Parameters.AddWithValue("@Price", price);
            command.ExecuteNonQuery();
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
    }
}
