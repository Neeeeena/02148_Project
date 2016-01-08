using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model.Exceptions
{
    public class ConnectionException : Exception
    {
        public enum Error
        {
            UnableToLocateDatabase,
            LoginFailure,
        }

        public Error Type { get; set; }


        public object Object { get; set; }

        public ConnectionException(string message, object obj) : base(message)
        {
            this.Object = obj;
        }

        public ConnectionException(string message) : base(message)
        {
        }

        public ConnectionException(Error type)
        {
            this.Type = type;
        }
    }
}
