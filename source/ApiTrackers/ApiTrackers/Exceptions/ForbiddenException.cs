using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Exceptions
{
    public class ForbiddenException : Exception
    {
        public int code = 401;

        public ForbiddenException() : base("Forbidden access to the data.")
        {

        }

    }
}
