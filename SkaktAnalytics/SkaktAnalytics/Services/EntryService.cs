using SkaktAnalytics.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SkaktAnalytics.Services
{
    public class EntryService
    {
        private static Timer _timer;

        public void AddEntry(Entry entry)
        {
            var repo = new EntryRepository();

            repo.Add(entry);

            if (_timer == null)
            {
                //_timer = new Timer(AddEntryInternal, entry, 2000, Timeout.Infinite);
            }
        }

        public void AddEntryInternal(object state)
        {
            var repo = new EntryRepository();

            repo.Add((Entry)state);

            _timer = null;
        }

        private bool EntryExists(Entry entry)
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("skakt_AzureStorageConnectionString"));

            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("skakt");

            var query = table.CreateQuery<Entry>()
                .Where($"PartitionKey eq '${entry.PartitionKey}' and Url eq '${entry.Url}'");

            return table.ExecuteQuery(query).GetEnumerator().Current != null;
        }

        internal IEnumerable<Entry> GetEntries(int count)
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("skakt_AzureStorageConnectionString"));

            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("skakt");

            var query = new TableQuery<Entry>().Take(count);
            
            return table.ExecuteQuery(query);

        }
    }
}