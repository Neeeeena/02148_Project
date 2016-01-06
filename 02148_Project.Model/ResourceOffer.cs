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

        public int SellerID { get; set; }

        public ResourceType Type { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }

        public int BuyerID { get; set; }

        public int HighestBid { get; set; }
    }
}
