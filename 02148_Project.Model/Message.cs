using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    class Message
    {
        public string Context { get; set; }

        public string SenderName { get; set; }

        public string RecieverName { get; set; }

        public Boolean ToAll { get; set; }
    }
}
