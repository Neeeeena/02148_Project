using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02148_Project.Model.Exceptions
{
    public class PlayerException : Exception
    {
        public string Name { get; set; }

        public PlayerException(string message, string name) : base(message)
        {
            this.Name = name;
        }
    }
}
