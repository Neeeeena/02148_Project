using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model.Exceptions
{
    public class MessageException : Exception
    {
        public MessageException(string message) : base(message)
        {
        }
    }
}
