using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace SkaktAnalytics.Models
{
    public class Entry : TableEntity
    {
        public string UserName { get; set; }
        public string Url { get; set; }
        public string Version { get; set; }
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string Agent { get; set; }

        public Entry()
        {

        }

        public Entry(string userName, string url)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Utils.SanitizeForParitionKey(userName);
            UserName = userName;
            Url = url;
        }
    }
}