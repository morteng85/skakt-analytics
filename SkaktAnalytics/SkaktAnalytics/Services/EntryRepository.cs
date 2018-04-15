using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SkaktAnalytics.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SkaktAnalytics.Services
{
    public class EntryRepository
    {
        private CloudTable _table;

        public EntryRepository()
        {
            var connStr = CloudConfigurationManager.GetSetting("skakt_AzureStorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(connStr);
            var tableClient = storageAccount.CreateCloudTableClient();

            _table = tableClient.GetTableReference("skakt");
        }

        public void Add(Entry entry)
        {
            _table.Execute(TableOperation.Insert(entry));
        }

        public IQueryable<Entry> Find(Expression<Func<Entry, bool>> expression)
        {
            return _table.CreateQuery<Entry>().AsQueryable().Where(expression);
        }
    }
}