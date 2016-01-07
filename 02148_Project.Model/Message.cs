﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model
{
    public class Message
    {
        public string Context { get; set; }

        public string SenderName { get; set; }

        public string RecieverName { get; set; }

        public bool ToAll { get; set; }

        /// <summary>
        /// Create a new message with all data objects
        /// </summary>
        /// <param name="context">Context of the message</param>
        /// <param name="sender"></param>
        /// <param name="reciever"></param>
        /// <param name="toAll">Flag to state wheneter or not the message is to everyone</param>
        public Message(string context, string sender, string reciever, bool toAll)
        {
            this.Context = context;
            this.SenderName = sender;
            this.RecieverName = reciever;
            this.ToAll = toAll;
        }
    }
}
