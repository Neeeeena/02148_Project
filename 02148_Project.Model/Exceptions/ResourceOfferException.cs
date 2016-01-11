using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model.Exceptions
{
    public class ResourceOfferException : Exception
    {
        public ResourceOffer Offer { get; set; }

        public ResourceOfferException(string message, ResourceOffer offer) : base(message)
        {
            this.Offer = offer;
        }
    }
}
