using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.MongoDB.Extensions
{
    /// <summary>
    /// Атрибут для задания названия коллекции
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BsonCollectionAttribute : Attribute
    {
        private readonly string _name;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Название коллекции</param>
        /// <exception cref="ArgumentNullException">Название коллекции не определено</exception>
        public BsonCollectionAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            _name = name;
        }

        /// <summary>
        /// Название коллекции
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
    }
}
