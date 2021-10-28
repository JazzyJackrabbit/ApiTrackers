using System;
using System.Runtime.Serialization;

namespace ApiSamples.Services
{
    [Serializable]
    internal class UnauthorisedException : Exception
    {
        public int code = 401;

        public UnauthorisedException() : base("Unauthorised access.")
        {

        }

    }
}