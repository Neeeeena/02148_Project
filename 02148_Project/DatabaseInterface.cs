using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace _02148_Project
{
    public class DatabaseInterface
    {
        private const string connectionString = @"Data Source=DESKTOP-E0GOLC2\SQLEXPRESS;Initial Catalog=nacmo_db;User ID=oliver;Password=zaq1xsw2";
        private SqlConnection connection;

        /// <summary>
        /// Create a new SqlConnection with the given connection string and open it
        /// </summary>
        public void OpenConnection()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
            connection = null;
        }

        public SqlDataReader GetPlayers()
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            OpenConnection();

            cmd.CommandText = "SELECT * FROM Players";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;

            reader = cmd.ExecuteReader();

            return reader;
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlaceResources(int sellerID, int resource, int count, int price)
        {
            string query = string.Format("INSERT INTO Market (SellerID, ResourceType, Count, Price) " +
                "VALUES ({0}, {1}, {2}, {3});", sellerID, resource, count, price);

            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public SqlDataReader ReadResourcesOnMarket()
        {
            string query = "SELECT Market.Id, Market.SellerId, Market.ResourceType, Market.Count, Market.Price, Players.Name "
                + "FROM Market "
                + "LEFT JOIN Players On Market.SellerID = Players.Id;";
            SqlCommand command = new SqlCommand(query, connection);

            return command.ExecuteReader();
        }
    }
}
