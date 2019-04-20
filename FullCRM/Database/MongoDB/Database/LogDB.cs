using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.MongoDB.Database
{
    /// <summary>
    /// База данных логирования
    /// </summary>
    public class LogDB : DB
    {
        public LogDB(IOptions<Config.Section> Settings) : base(Settings.Value.Log.Mongo) { }
    }

}
