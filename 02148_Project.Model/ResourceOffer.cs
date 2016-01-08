using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    public class ResourceOffer
    {
        public int Id { get; set; }

        public string SellerName { get; set; }

        public ResourceType Type { get; set; }

        public int Count { get; set; }

        public ResourceType PriceType { get; set; }

        public int Price { get; set; }

        public string HighestBidder { get; set; }

        public int HighestBid { get; set; }

        public ResourceOffer(string name, ResourceType type, int count, ResourceType priceType, int price) 
            : this(0, name, type, count, priceType, price) { }

        public ResourceOffer(int id, string sellerName, ResourceType type, int count, ResourceType priceType, int price) 
            : this(id, sellerName, type, count, priceType, price, null, 0) { }

        /// <summary>
        /// Create a new resource offer object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sellerName"></param>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <param name="price"></param>
        /// <param name="highestBidder"></param>
        /// <param name="highestBid"></param>
        public ResourceOffer(int id, string sellerName, ResourceType type, 
            int count, ResourceType priceType, int price, string highestBidder, int highestBid)
        {
            Id = id;
            SellerName = sellerName;
            Type = type;
            Count = count;
            PriceType = priceType;
            Price = price;
            HighestBidder = highestBidder;
            HighestBid = highestBid;
        }
    }
}
