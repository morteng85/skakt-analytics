using SkaktAnalytics.Models;
using System;

namespace SkaktAnalytics.Services
{
    public class EntryRepository : TableRepositoryBase<Entry>
    {
        public EntryRepository() : base(Constants.EntryTableName)
        {
        }
    }
}