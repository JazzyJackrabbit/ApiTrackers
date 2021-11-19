using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Exceptions
{
    public class DatabaseRequestException : Exception
    {
        public int code = 500;

        public DatabaseRequestException() : base("Internal error on database.")
        {

        }
        public DatabaseRequestException(string _message) : base("Internal error on database. \n"+ _message)
        {

        }

    }
}
