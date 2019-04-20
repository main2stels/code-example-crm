using MongoDB.Driver;
using System;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using FullCRM.Database.MongoDB.Extensions;

namespace FullCRM.Database.MongoDB
{
    /// <summary>
    /// База данных
    /// </summary>
    public abstract class DB
    {
        protected readonly IMongoClient _client;

        protected readonly IMongoDatabase _db;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="connectionString">Строка подключения к БД</param>
        protected DB(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            var url = MongoUrl.Create(connectionString);

            _client = new MongoClient(url);

            _db = _client.GetDatabase(url.DatabaseName);
        }

        /// <summary>
        /// Экземпляр клиента базы данных
        /// </summary>
        public IMongoClient Client
        {
            get { return _client; }
        }

        /// <summary>
        /// Экземпляр базы данных
        /// </summary>
        public IMongoDatabase Database
        {
            get { return _db; }
        }

        ///// <summary>
        ///// Получить коллекцию по типу модели
        ///// </summary>
        ///// <typeparam name="T">Тип модели</typeparam>
        ///// <returns></returns>
        public IMongoCollection<T> GetCollection<T>()
        {
            var modelType = typeof(T);

            var attributes = modelType.GetCustomAttributes<BsonCollectionAttribute>(false).ToList();

            string name = attributes.Count > 0 ? attributes[0].Name : modelType.Name;

            return GetCollection<T>(name);
        }

        /// <summary>
        /// Получить коллекцию по типу модели и названию коллекции
        /// </summary>
        /// <typeparam name="T">Тип модели</typeparam>
        /// <param name="name">Название коллекции</param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            return _db.GetCollection<T>(name);
        }
    }
}
