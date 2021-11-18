using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
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

        public static Stream PostUrlWithoutData(string _url)
        {
            return PostUrlWithoutDataAsync(_url).Result;
        }

        internal static async Task<Stream> PostUrlWithoutDataAsync(string _url)
        {
            return await _url
            .PostUrlEncodedAsync(new {})
            .ReceiveStream();
        }

    }
}