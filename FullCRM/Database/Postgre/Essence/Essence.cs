using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqToDB.Data;
using LinqToDB;
using LinqToDB.Linq;


namespace FullCRM.Database.Postgre.PostgreEssence
{
    public abstract class Essence<T> : ReadEssence<T> where T : class
    {
        public Essence(DataConnection dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Вставить запись
        /// </summary>
        /// <param name="model">Запись</param>
        public virtual void Insert(T model)
        {
            _dbContext.Insert(model);
        }

        /// <summary>
        /// Вставить запись ассинхронно
        /// </summary>
        /// <param name="model">Запись</param>
        public async virtual Task InsertAsync(T model)
        {
            await Task.Factory.StartNew(() => _dbContext.Insert(model));
        }

        /// <summary>
        /// Вставить несколько записей
        /// </summary>
        /// <param name="models">Список записей</param>
        public virtual void InsertBatch(IEnumerable<T> models)
        {
            _dbContext.BulkCopy(models);
        }

        /// <summary>
        /// Вставить несколько записей ассинхронно
        /// </summary>
        /// <param name="models">Список записей</param>
        public async virtual Task InsertBatchAsync(IEnumerable<T> models)
        {
            await Task.Factory.StartNew(() => _dbContext.BulkCopy(models));
        }

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="model">Запись</param>
        public virtual void Update(T model)
        {
            _dbContext.Update(model);
        }

        /// <summary>
        /// Обновить поля в записи
        /// </summary>
        /// <param name="model">Запись</param>
        /// <param name="fields">Список полей</param>
        /// <exception cref="ArgumentException">Обновляемое поле не может быть типа enum</exception>
        public virtual void UpdateFields(T model, params Expression<Func<T, object>>[] fields)
        {
            IUpdatable<T> update = _table.Where(IdQuery(model)).AsUpdatable();

            foreach (var field in fields)
            {
                var value = field.Compile()(model);

                // Так как для драйвера npgsql не всё равно передается ли ему Enum или (object)Enum,
                // а так как expression как раз возвращает (object)Enum и не нашлось способа присести его к Enum
                // выбрасываем исключение при попытки обновить поле типа Enum
                if (value is Enum)
                {
                    var message =
@"Обновляемое поле не может быть типа enum

Для обновление поля типа enum используйте конструкцию:
Essence.AsQuerible()
.Where(x => x.Id == DbModel.Id)
.Set(x => x.Enum, DbModel.Enum)
...
.Update();";
                    throw new ArgumentException(message, "fields");
                }

                update = update.Set(field, value);
            }

            update.Update();
        }

        public abstract void Save(T model, Expression<Func<T, bool>> query);

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="model">Запись</param>
        public virtual void Delete(T model)
        {
            _dbContext.Delete(model);
        }

        public void BeginTransaction()
        {
            _dbContext.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            _dbContext.RollbackTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.CommitTransaction();
        }

        protected abstract Expression<Func<T, bool>> IdQuery(T model);
    }
}
