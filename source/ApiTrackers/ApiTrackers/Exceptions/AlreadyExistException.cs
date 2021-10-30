using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public int code = 303;

        public AlreadyExistException(Type _type) : base(_type.ToString() + " object already exist on database.")
        {
            
        }

    }
}
