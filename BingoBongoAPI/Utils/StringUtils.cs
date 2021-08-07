using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Utils
{
    public static class StringUtils
    {
        private static Random _random = new Random();
        public static List<string> randomWords = new List<string>()
        {
            "world",
            "paradise",
            "chat",
            "messaging",
            "channel",
            "galaxy",
            "environment",
            "universe",
            "proxy",
            "playground"
        };

        public static string GetRandomWord()
        {
            int num = _random.Next(0, randomWords.Count());
            return randomWords[num];
        }
    }
}
