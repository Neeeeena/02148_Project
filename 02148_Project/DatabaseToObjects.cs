using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using _02148_Project.Model;

namespace _02148_Project
{
    public class DatabaseToObjects
    {
        public List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>();
            DatabaseInterface.OpenConnection();




            return players;
        }
    }
}
