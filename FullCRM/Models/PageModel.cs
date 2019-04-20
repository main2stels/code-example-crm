using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Models
{
    /// <summary>
    /// Модель страницы со списком
    /// </summary>
    /// <typeparam name="T">Тип элемента списка</typeparam>
    public class PageModel<T>
    {
        public PageModel()
        {
            Items = new T[0];
            Count = 0;
        }

        public PageModel(T[] items)
        {
            Items = items;
            Count = items.LongLength;
        }

        /// <summary>
        /// Список
        /// </summary>
        public IEnumerable<T> Items
        {
            get;
            set;
        }

        /// <summary>
        /// Количество записей в бд
        /// </summary>
        public long Count
        {
            get;
            set;
        }

        public T Total
        {
            get;
            set;
        }
    }
}
