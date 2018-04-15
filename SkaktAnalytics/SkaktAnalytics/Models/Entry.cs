using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkaktAnalytics.Models
{
    public class Entry : TableEntity
    {
        public Entry(string name, string url, string version, string userHostAddress)
        {
            PartitionKey = Utils.Reverse(name);
            Url = Utils.Reverse(url);
            RowKey = Guid.NewGuid().ToString();
            Version = version;
            UserHostAddress = userHostAddress;
        }

        public Entry()
        {

        }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Version { get; set; }
        public string UserHostAddress { get; set; }
    }
}