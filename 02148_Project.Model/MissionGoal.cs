using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    public class MissionGoal
    {
        public string Id { get; set; }
        public Construction Type { get; set; }
        public string ImageSrc { get; set; }
        public MissionGoal(Construction type)
        {
            Type = type;
            ImageSrc = Type.GetImageSrc();
        }


        //.
    }
}
