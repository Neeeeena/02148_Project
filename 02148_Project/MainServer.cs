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

        //Initializing game
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

        
        static int MAX_MARKET_ADVERTS = 8;
        private static void createAdvert(Object source, System.Timers.ElapsedEventArgs e)
        {
            
            //offercount = amount of adverts the market is currently offering
            int offercount = DatabaseInterface.ReadAllResourceOffers().FindAll(s => s.SellerName.Equals("Market")).Count;
            Random r = new Random();

            //TEMP
            Console.WriteLine("MIGHT CREATE ADVERT");

            //possibly creating new advert from marketplace
            //chance is (100 - (offercount / MAX_MARKET_ADVERTS) * 100)%
            //The fewer market adverts, the higher chance of creating advert)
            if (r.Next(MAX_MARKET_ADVERTS) >= offercount)
            {
                //TEMP
                Console.WriteLine("CREATING ADVERT!!!!");

                //Setting random price and count
                int count = new Random().Next(3);
                int price = (new Random().Next(4, 8)) * (count);

                //Creating array of the enum values
                Array resTypes = Enum.GetValues(typeof(ResourceType));

                //Picking a random resource from the array, except the last, which is gold
                ResourceType res = (ResourceType)resTypes.GetValue(r.Next(resTypes.Length-1));
                ResourceOffer ro = new ResourceOffer("Market", res, count, price);
                DatabaseInterface.PutResourceOfferOnMarket(ro);
            }



        }

        //Creating timer, that calls create advert every 10 seconds.
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
