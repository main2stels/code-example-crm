using FullCRM.Database.MongoDB.Database;
using FullCRM.Database.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.MongoDB.Repository
{
    public class VersionsLogRepository : MongoRepository<VersionsLog>
    {
        /// <summary>
        /// Репозиторий журнала изменений
        /// </summary>
        /// <param name="db">Используемая база данных</param>
        /// <param name="logName">Название коллеции с логом изменений</param>
        public VersionsLogRepository(VersionHistoryDB db) : base(db)
        {
            _collection = _db.GetCollection<VersionsLog>("fullCRM");
        }
    }
}
