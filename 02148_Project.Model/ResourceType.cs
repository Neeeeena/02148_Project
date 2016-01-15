using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    public enum ResourceType
    {
        Wood, 
        Clay, 
        Wool,
        Stone, 
        Iron, 
        Straw, 
        Food,
        Gold
    }

    public static class ResTypeMethods
    {
        public static string GetImageSrc(this ResourceType res)
        {
            switch (res)
            {
                case ResourceType.Clay:
                    return "Images/mursten.png";
                case ResourceType.Food:
                    return "Images/Food.png";
                case ResourceType.Wool:
                    return "Images/får.png";
                case ResourceType.Straw:
                    return "Images/Straw.png";
                case ResourceType.Stone:
                    return "Images/stone.png";
                case ResourceType.Iron:
                    return "Images/anvil.png";
                default:
                    return "Images/firewood.png";
            }
        }
    }
}
