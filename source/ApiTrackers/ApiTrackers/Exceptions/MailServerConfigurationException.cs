using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.Exceptions
{
    public class MailServerConfigurationException : Exception
    {
        public int code = 500;

        public MailServerConfigurationException() : base("Mail Server conflict.")
        {

        }
    }
}
