using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using _02148_Project.Model;
namespace _02148_Project
{
    class MainServer
    {
        public static void initGame(List<Player> players)
        {
            foreach(Player p in players)
            {
                p.Gold = 15;
                p.Iron = 0;
                p.Straw = 0;
                p.Wood = 0;
                p.Wool = 0;
                p.Stone = 0;
                p.Clay = 0;
                p.Food = 0;
            }
            Thread thread = new Thread(advertTimer);


        }

        

        private static void createAdvert(Object source, System.Timers.ElapsedEventArgs e)
        {

            int offercount = DatabaseInterface.ReadAllResourceOffers().Count;
            Random r = new Random();
            if (r.Next(12) >= offercount)
            {
                int count = new Random().Next(3);
                int price = new Random().Next(4, 8);
            
                Array resTypes = Enum.GetValues(typeof(ResourceType));
                ResourceType res = (ResourceType)resTypes.GetValue(r.Next(resTypes.Length-1));
                ResourceOffer ro = new ResourceOffer("Market", res, count, price);
                DatabaseInterface.PutResourceOfferOnMarket(ro);
            }


        }
        public static void advertTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 10000;
            timer.AutoReset = true;
            timer.Elapsed += createAdvert;
            timer.Start();
        }

    }
}
