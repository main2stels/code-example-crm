using FullCRM.Database.MongoDB.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace FullCRM.Database.MongoDB.Repository
{
    public abstract class MongoRepository<T> where T : IDbModel
    {
        protected readonly DB _db;

        protected IMongoCollection<T> _collection;

        protected MongoRepository(DB db)
        {
            _db = db;
            _collection = _db.GetCollection<T>();
        }

        #region Выборка

        /// <summary>
        /// Выбрать первый объект по его Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public T GetById(ObjectId id)
        {
            return _collection.Find(Builders<T>.Filter.Eq(e => e.Id, id)).Limit(1).FirstOrDefault();
        }

        /// <summary>
        /// Выбрать записи по списку идентификаторов
        /// </summary>
        /// <param name="ids">Список идентификаторов</param>
        /// <returns></returns>
        public IFindFluent<T, T> GetByIds(params ObjectId[] ids)
        {
            return _collection.Find(Builders<T>.Filter.In(e => e.Id, ids));
        }

        #endregion

        #region Обновление

        /// <summary>
        /// Вставить или обновить запись в коллекции
        /// </summary>
        /// <param name="model">Объект для вставки или модификации</param>
        /// <returns>Результат операции</returns>
        public ReplaceOneResult Save(T model)
        {
            return _collection.ReplaceOne(Builders<T>.Filter.Eq(el => el.Id, model.Id), model, new UpdateOptions { IsUpsert = true });
        }

        #endregion

        #region Удаление

        /// <summary>
        /// Удалить запись из коллекции
        /// </summary>
        /// <param name="model">Объект для удаления</param>
        /// <returns>Результат операции</returns>
        public DeleteResult Delete(T model)
        {
            return DeleteById(model.Id);
        }

        /// <summary>
        /// Удалить запись из коллекции по Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Результат операции</returns>
        public DeleteResult DeleteById(ObjectId id)
        {
            return _collection.DeleteOne(Builders<T>.Filter.Eq(e => e.Id, id));
        }

        /// <summary>
        /// Удалить записи из коллекции по списку идентификаторов
        /// </summary>
        /// <param name="ids">Список идентификаторов</param>
        /// <returns>Результат операции</returns>
        public DeleteResult DeleteByIds(params ObjectId[] ids)
        {
            return _collection.DeleteMany(Builders<T>.Filter.In(e => e.Id, ids));
        }

        #endregion

        #region Вставка

        /// <summary>
        /// Вставить запись в коллекцию
        /// </summary>
        /// <param name="model">Объект для вставки</param>
        public void Insert(T model)
        {
            _collection.InsertOne(model);
        }

        /// <summary>
        /// Вставить несколько записей в коллекцию
        /// </summary>
        /// <param name="models">Объекты для вставки</param>
        public void InsertBatch(IEnumerable<T> models)
        {
            _collection.InsertMany(models);
        }

        #endregion
    }
}
