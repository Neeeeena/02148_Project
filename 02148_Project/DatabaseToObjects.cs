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
    }
}
