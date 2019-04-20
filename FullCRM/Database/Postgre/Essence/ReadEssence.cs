using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.PostgreEssence
{
    public abstract class ReadEssence<T> where T : class
    {
        protected DataConnection _dbContext;
        protected ITable<T> _table;

        public ReadEssence(DataConnection dbContext)
        {
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
            _dbContext = dbContext;
            _table = _dbContext.GetTable<T>();
        }

        public IQueryable<T> AsQueryable()
        {

            return _table;
        }

        public IQueryable<T> AsQueryableLoadWith(Expression<Func<T, object>> selector)
        {
            return _table.LoadWith(selector);
        }

        /// <summary>
        /// Выбрать все записи
        /// </summary>
        /// <returns></returns>
        public virtual IReadOnlyCollection<T> GetAll()
        {
            return new ReadOnlyCollection<T>(_table.ToList());
        }


        /// <summary>
        /// Выбираем все записи с подгрузкой зависимостей 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual IReadOnlyCollection<T> GetAllLoadWith(Expression<Func<T, object>> selector)
        {
            return new ReadOnlyCollection<T>(_table.LoadWith(selector).ToList());
        }
    }
}
