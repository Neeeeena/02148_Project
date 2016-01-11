using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using _02148_Project.Model;

namespace _02148_Project
{
    class Advert
    {



        int id;
        int stopwatchThreshold = 50;

        //The timer is used for calling the event "closeAdvert(..)"
        System.Timers.Timer timer;
        //The stopwatch is used for determing if the auction is almost running out, when a bid is made. 
        Stopwatch stopwatch;

        public Advert(int id)
        {

            this.id = id;
            timer = new System.Timers.Timer();
            timer.Interval = 60000;
            timer.AutoReset = false;
            timer.Elapsed += closeAdvert;
            timer.Enabled = true;

            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        private void closeAdvert(Object source, System.Timers.ElapsedEventArgs e)
        {
            ResourceOffer ro;
            ro = DatabaseInterface.GetResourceOffer(id);

            if (ro.HighestBidder != null)
                sellToPlayer(ro);
            timer.Close();
            stopwatch.Stop();
        }

        private void sellToPlayer(ResourceOffer ro)
        {
            //Take the gold
            DatabaseInterface.UpdatePlayerResource(ro.HighestBidder, ResourceType.Gold, - ro.Price);

            //Give the ware
            DatabaseInterface.UpdatePlayerResource(ro.HighestBidder, ro.Type, ro.Count);
        }

        //This should be called every time a bid is accepted, in order to possibly extend the remaining time.
        public void bidAccepted()
        {
            double elapsedms = stopwatch.ElapsedMilliseconds;
            if(elapsedms > stopwatchThreshold)
            {
                timer.Interval = elapsedms - stopwatchThreshold;
                stopwatchThreshold = 0;
                stopwatch.Reset();
            }
        }
    }
}
