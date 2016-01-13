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

        //Resources START
        public int Wood { get; set; }

        public int Clay { get; set; }

        public int Wool { get; set; }

        public int Stone { get; set; }

        public int Iron { get; set; }

        public int Straw { get; set; }

        public int Food { get; set; }

        public int Gold { get; set; }
        //Resources END

        //Constructions START
        public int Cottage { get; set; }
        public int Forge { get; set; }
        public int Mason { get; set; }
        public int Mill { get; set; }
        public int Farm { get; set; }
        public int Townhall { get; set; }
        public int Goldmine { get; set; }
        //Constructions END


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
            this.Wool = wool;
            this.Stone = stone;
            this.Iron = iron;
            this.Straw = straw;
            this.Food = food;
            this.Gold = gold;
            this.Cottage = 0;
            this.Forge = 0;
            this.Mason = 0;
            this.Mill = 0;
            this.Farm = 0;
            this.Townhall = 0;
            this.Goldmine = 0;
        }
    }
}
