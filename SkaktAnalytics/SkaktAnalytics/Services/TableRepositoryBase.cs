using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SkaktAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SkaktAnalytics.Services
{
    public abstract class TableRepositoryBase<TEntity> where TEntity : TableEntity, new ()
    {
        protected CloudTable _cloudTable;

        public TableRepositoryBase(string tableName)
        {
            var connStr = CloudConfigurationManager.GetSetting(Constants.TableConnectionString);
            var storageAccount = CloudStorageAccount.Parse(connStr);
            var tableClient = storageAccount.CreateCloudTableClient();

            _cloudTable = tableClient.GetTableReference(tableName);
        }

        public void Add(TEntity entity)
        {
            _cloudTable.Execute(TableOperation.Insert(entity));
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> whereExpr)
        {
            return _cloudTable.CreateQuery<TEntity>().AsQueryable().Where(whereExpr);
        }

        public List<TEntity> Get(Expression<Func<TEntity, bool>> whereExpr)
        {
            return GetQueryable(whereExpr).ToList();
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> whereExpr)
        {
            var result = Get(whereExpr);

            if (result.Count() > 0)
            {
                return Get(whereExpr).First();
            }

            return null;
        }

        public bool Exists(Expression<Func<TEntity, bool>> whereExpr)
        {
            return Get(whereExpr).Count > 0;
        }
    }
}