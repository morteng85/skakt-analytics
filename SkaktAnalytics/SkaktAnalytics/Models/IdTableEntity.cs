using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkaktAnalytics.Models
{
    public class IdTableEntity : TableEntity
    {
        public IdTableEntity()
        {
            RowKey = Guid.NewGuid().ToString();
        }
    }
}