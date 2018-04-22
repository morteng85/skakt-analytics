using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace SkaktAnalytics.Models
{
    public class User : TableEntity
    {
        public string UserName { get; set; }
        public string Version { get; set; }

        public User()
        {

        }

        public User(string userName, string version)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = UserName = Utils.Reverse(userName);
            Version = version;
        }
    }
}