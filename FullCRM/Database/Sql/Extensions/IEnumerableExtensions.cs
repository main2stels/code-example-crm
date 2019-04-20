using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Sql.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Разбиение последовательности на части.
        /// Примечение: последняя из полученных последовательностей может содержать меньшее количество значений, чем указано.
        /// </summary>
        /// <typeparam name="T">Тип элементов последовательности</typeparam>
        /// <param name="source">Последовательность</param>
        /// <param name="chunkSize">Размер части</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
        {
            var listOfLists = new List<IEnumerable<T>>();
            var enumerable = source as T[] ?? source.ToArray();

            for (int i = 0; i < enumerable.Count(); i += chunkSize)
            {
                listOfLists.Add(enumerable.Skip(i).Take(chunkSize));
            }

            return listOfLists;
        }
    }
}
