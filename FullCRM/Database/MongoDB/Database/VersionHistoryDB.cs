using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FullCRM.Database.MongoDB.Database
{
    /// <summary>
    /// База данных с историей версий
    /// </summary>
    public class VersionHistoryDB : DB
    {
        public VersionHistoryDB(IOptions<Config.Section> Settings) : base(Settings.Value.DB.MongoVersionHistory) { }
    }
}
