using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace SkaktAnalytics.Models
{
    public class User : TableEntity
    {
        public string UserName { get; set; }
        public string Version { get; set; }
        public string Theme { get; set; }
        public string Lines { get; set; }
        public string Highlight { get; set; }
        public string VersionInstalled { get; set; }
        public string LastUsed { get; set; }

        public User()
        {

        }

        public User(string userName, string version, string theme, string lines, string highlight)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Utils.SanitizeForParitionKey(userName);
            UserName = userName;
            Version = version;
            Theme = theme;
            Lines = lines;
            Highlight = highlight;
        }
    }
}