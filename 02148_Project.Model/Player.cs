using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Player(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
