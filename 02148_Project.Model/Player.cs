using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    public class Player
    {
        public string Name { get; set; }

        public int Wood { get; set; }

        public int Clay { get; set; }

        public int Wool { get; set; }

        public int Stone { get; set; }

        public int Iron { get; set; }

        public int Straw { get; set; }

        public int Food { get; set; }

        public int Gold { get; set; }

        /// <summary>
        /// Create a player object with a name
        /// </summary>
        /// <param name="name">Name of the player/param>
        public Player(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Create a player with all his resources
        /// </summary>
        /// <param name="name"></param>
        /// <param name="wood"></param>
        /// <param name="clay"></param>
        /// <param name="wool"></param>
        /// <param name="stone"></param>
        /// <param name="iron"></param>
        /// <param name="straw"></param>
        /// <param name="food"></param>
        /// <param name="gold"></param>
        public Player(string name, int wood, int clay, int wool, int stone, int iron, int straw, int food, int gold)
        {
            this.Name = name;
            this.Wood = wood;
            this.Clay = clay;
            this.Stone = stone;
            this.Iron = iron;
            this.Straw = straw;
            this.Food = food;
            this.Gold = gold;
        }
    }
}
