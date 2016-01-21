using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    public enum Construction
    {
        Cottage,
        Forge,
        Mill,
        Farm,
        Townhall,
        Goldmine
    }

    public static class ConstructionMethods
    {
        public static string GetImageSrc(this Construction res)
        {
            switch (res)
            {
                case Construction.Cottage:
                    return "Images/Cottage.png";
                case Construction.Farm:
                    return "Images/Farm.png";
                case Construction.Forge:
                    return "Images/forge.png";
                case Construction.Goldmine:
                    return "Images/Goldmine.png";
                case Construction.Mill:
                    return "Images/Mill.png";
                case Construction.Townhall:
                    return "Images/TownHall.png";
                default:
                    return "Images/Horse.png"; //If a horse appears, we cry
            }
        }
    }

}
