using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkaktAnalytics
{
    public static class Utils
    {
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string SanitizeForParitionKey(string s)
        {
            return Constants.DisallowedCharsInTableKeys.Replace(s, "");
        }
    }
}