using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Exceptions
{
    public class InvalidMailAddressClientException : Exception
    {
        public int code = 400;

        public InvalidMailAddressClientException() : base("Mail Adress is invalid.")
        {

        }
    }
}
