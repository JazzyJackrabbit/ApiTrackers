using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Exceptions
{
    public class TODOEXCEPTION : Exception
    {
        public int code = 500;

        public TODOEXCEPTION() : base("TODO")
        {

        }

    }
}
