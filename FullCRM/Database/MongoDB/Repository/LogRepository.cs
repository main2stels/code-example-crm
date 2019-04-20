using FullCRM.Database.MongoDB.Database;
using FullCRM.Database.MongoDB.Model;

namespace FullCRM.Database.MongoDB.Repository
{
    public class LogRepository : MongoRepository<Logs>
    {
        public LogRepository(LogDB db) : base(db)
        {
            var collectionName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            _collection = _db.GetCollection<Logs>(collectionName);
        }
    }
}
