using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    public class Message
    {

        public int Id { get; set; }

        public string Content { get; set; }

        public string SenderName { get; set; }

        public string RecieverName { get; set; }

        public bool ToAll { get; set; }
        public string htmlClass { get; set; } = "otherMessage";

        public Message(int id, string content, string sender, string receiver, bool toAll = false)
            : this(content, sender, receiver, toAll)
        {
            this.Id = id;
        }
        
        /// <summary>
        /// Create a new message with all data objects
        /// </summary>
        /// <param name="content">Context of the message</param>
        /// <param name="sender"></param>
        /// <param name="reciever"></param>
        /// <param name="toAll">Flag to state wheneter or not the message is to everyone</param>
        public Message(string content, string sender, string reciever, bool toAll = false)
        {
            this.Content = content;
            this.SenderName = sender;
            this.RecieverName = reciever;
            this.ToAll = toAll;
        }
    }
}
