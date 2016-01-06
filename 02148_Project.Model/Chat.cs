using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    class Chat
    {
        public string Message { get; set; }

        public int SenderID { get; set; }

        public int RecieverID { get; set; }

        public Boolean ToAll { get; set; }
    }
}
