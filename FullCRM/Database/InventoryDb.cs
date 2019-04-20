using FullCRM.Config.Elements;
using FullCRM.Services;
using LinqToDB.Data;
using LinqToDB.DataProvider.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database
{
    public class InventoryDb : DataConnection
    {
        public InventoryDb(IOptions<Config.Section> Settings) 
            : base(new PostgreSQLDataProvider(), Settings.Value.DB.PostgreSQL)
        {
        }
        
    }
}
