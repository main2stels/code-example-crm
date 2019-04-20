using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FullCRM.Database.MongoDB.Model
{
    /// <summary>
    /// Модель логирования
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Logs : DbModel
    {
        public string Identidication { get; set; }

        public string Message { get; set; }

        public string MachineName { get; set; }

        [BsonIgnoreIfNull]
        public object Exception { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Level { get; set; }
    }
}
