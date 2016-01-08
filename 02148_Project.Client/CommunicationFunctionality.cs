using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02148_Project.Model;
using _02148_Project;

namespace _02148_Project.Client
{
    public static class CommunicationFunctionality
    {
        
        public static List<ResourceOffer> UpdateAllResourceOffers()
        {
            return DatabaseInterface.ReadAllResourceOffers();
        }

        public static List<TradeOffer> UpdateAlleTradeOffers(string reciever)
        {
            return DatabaseInterface.ReadAllTradeOffers(reciever);
        }


    }
}
