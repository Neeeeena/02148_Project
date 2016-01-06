using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    class TradeOffer
    {
        public int Id { get; set; }

        public int SellerId { get; set; }

        public int RecieverId { get; set; }

        public int ResourceType { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }
    }
}
