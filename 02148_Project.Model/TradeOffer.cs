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

        public string SellerName { get; set; }

        public string RecieverName { get; set; }

        public ResourceType Type { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }

        /// <summary>
        /// Create a trade offer object with all the data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sellerName"></param>
        /// <param name="recieverName"></param>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <param name="price"></param>
        public TradeOffer(int id, string sellerName, string recieverName, 
            ResourceType type, int count, int price)
        {
            Id = id;
            SellerName = sellerName;
            RecieverName = recieverName;
            Type = type;
            Count = count;
            Price = price;
        }
    }
}
