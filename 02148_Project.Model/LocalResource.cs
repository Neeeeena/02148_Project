using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    public class LocalResource
    {
        public string Id { get; set; }
        public ResourceType Type { get; set; }
        public string ImageSrc { get; set; }
        public LocalResource(ResourceType type)
        {
            Type = type;
            ImageSrc = Type.GetImageSrc();
        }
    }
}
