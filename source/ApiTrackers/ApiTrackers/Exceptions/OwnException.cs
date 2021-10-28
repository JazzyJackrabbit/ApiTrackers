using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Exceptions
{
    public class OwnException : Exception
    {
        public int code = 500;

        public OwnException() : base("Internal Error")
        {
            #if (DEBUG)
                  Console.WriteLine("Internal Error: " + Message);
            #endif
            #if (RELEASE)
                  Console.WriteLine("Internal Error." + Message);
            #endif
        }

    }
}
