using FullCRM.Database.Sql.Filtering.Abstract;
using FullCRM.Database.Sql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace FullCRM.Database.Sql.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IOrderedQueryable<T> current, int page, int limit)
        {
            int skip = page > 0 ? page * limit : 0;

            return current.Skip(skip)
                          .Take(limit);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> current, SortDirectionType sortDirect, string field)
        {
            var modelType = typeof(T);
            var methodName = GetSortMethodName(sortDirect);

            // Генерируем x => x.Name
            var param = Expression.Parameter(modelType, "x");
            var property = Expression.Property(param, field);
            var lambdaProperty = Expression.Lambda(property, param);

            var methodInfo = typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(x => x.Name == methodName &&
                                                                                                                         x.GetParameters().Length == 2 &&
                                                                                                                         x.GetGenericArguments().Length == 2);
            var genericMethod = methodInfo.MakeGenericMethod(modelType, property.Type);

            var commonParam = Expression.Parameter(typeof(IQueryable<T>), "y");

            // Генерируем вызов метода OrderBy или OrderByDescending
            var exp = Expression.Call(genericMethod, commonParam, lambdaProperty);

            var a = Expression.Lambda<Func<IQueryable<T>, IOrderedQueryable<T>>>(exp, commonParam);

            return a.Compile()(current);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> current, Filter<T> filter)
        {
            if (filter != null)
            {
                return current.Where(filter.ToExpression());
            }

            return current;
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> current, IPageCondition<T> condition)
        {
            return current.Where(condition.Filter)
                          .OrderBy(condition.SortDirect, condition.SortField ?? typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault().Name)
                          .Page(condition.Page, condition.Limit);
        }

        private static string GetSortMethodName(SortDirectionType sortDirect)
        {
            switch (sortDirect)
            {
                case SortDirectionType.Ascending:
                    return "OrderBy";
                case SortDirectionType.Descending:
                    return "OrderByDescending";
                default:
                    return "";
            }
        }
    }
}
