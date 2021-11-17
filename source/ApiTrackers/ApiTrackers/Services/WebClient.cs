using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;

namespace ApiTrackers.Services
{
    public class WebClient
    {
        Main main;

        public WebClient(Main _main)
        {
            main = _main;
        }

        public static Stream PostUrl(string _url)
        {
            return PostUrlAsync(_url).Result;
        }

        internal static async Task<Stream> PostUrlAsync(string _url)
        {
            return await _url
            .PostUrlEncodedAsync(new { thing1 = "hello", thing2 = "world" })
            .ReceiveStream();
        }

    }
}
