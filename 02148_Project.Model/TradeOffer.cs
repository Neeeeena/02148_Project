using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    public class TradeOffer
    {
        public int Id { get; set; }

        public string SellerName { get; set; }

        public string RecieverName { get; set; }

        public Dictionary<ResourceType,int> SellerResources { get; set; }

        public Dictionary<ResourceType, int> ReceiverResources { get; set; }

        /// <summary>
        /// Create a trade offer object with all the data
        /// </summary>
        /// <param name="id">Id of the Trade offer in the database</param>
        /// <param name="sellerName">Name of the seller</param>
        /// <param name="recieverName">Name of the reciever</param>
        /// <param name="type">Type of resource</param>
        /// <param name="count">Number of resources to sell</param>
        /// <param name="price">Price of the resources</param>
        public TradeOffer(int id, string sellerName, string recieverName, 
            Dictionary<ResourceType, int> resources, Dictionary<ResourceType, int> price)
            : this(sellerName, recieverName, resources, price)
        {
            Id = id;
        }

        public TradeOffer(string sellerName, string recieverName,
            Dictionary<ResourceType, int> resources, Dictionary<ResourceType, int> price)
        {
            this.SellerName = sellerName;
            this.RecieverName = recieverName;
            this.SellerResources = resources;
            this.ReceiverResources = price;
        }
    }
}
