using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Exceptions
{
    public class InvalidMailAddressServerException : Exception
    {
        public int code = 510;

        public InvalidMailAddressServerException() : base("Mail Adress is invalid.")
        {

        }
    }
}
