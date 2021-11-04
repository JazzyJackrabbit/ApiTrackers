﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest_APITrackers
{
    public static class BpmExtensions
    {
        const long SecondsPerMinute = TimeSpan.TicksPerMinute / TimeSpan.TicksPerSecond;

        public static int ToBpm(this TimeSpan timeSpan)
        {
            var seconds = 1 / timeSpan.TotalSeconds;
            return (int)Math.Round(seconds * SecondsPerMinute);
        }

        public static TimeSpan ToInterval(this int bpm)
        {
            var bps = (double)bpm / SecondsPerMinute;
            var interval = 1 / bps;
            return TimeSpan.FromSeconds(interval);
        }
    }
}
