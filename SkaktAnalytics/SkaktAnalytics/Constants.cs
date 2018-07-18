using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SkaktAnalytics
{
    public class Constants
    {
        public static string TableConnectionString = "skakt_AzureStorageConnectionString";
        public static string UserTableName = "users";
        public static string EntryTableName = "entries";
        public static Regex DisallowedCharsInTableKeys = new Regex(@"[\\\\#%+/?\u0000-\u001F\u007F-\u009F]");
    }
}