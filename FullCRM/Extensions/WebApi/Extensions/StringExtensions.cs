using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FullCRM.Extensions.WebApi.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Получить лямбда-выражение для свойства по названию свойства и его типу
        /// </summary>
        /// <param name="propertyName">Название свойства</param>
        /// <param name="modelType">Тип модели</param>
        /// <param name="propertyType">Тип свойства</param>
        /// <returns></returns>
        public static Expression GetPropertyExpression(this string propertyName, Type modelType, Type propertyType)
        {
            // Объект модели
            ParameterExpression el = Expression.Parameter(modelType, "el");

            // Получение свойства модели
            MemberExpression property = Expression.Property(el, propertyName);

            // Приведение свойства к указанному типу
            Expression typedProperty = Expression.Convert(property, propertyType);

            // Создание lambda-выражения
            return Expression.Lambda(typedProperty, new ParameterExpression[] { el });
        }

        /// <summary>
        /// Приводит первый символ строки в нижний регистр (ModelType => modelType)
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <returns>Преобразованная строка</returns>
        public static string ToLowerFirstSymbol(this string str)
        {
            return !string.IsNullOrEmpty(str) ? char.ToLowerInvariant(str[0]) + str.Substring(1) : str;
        }
    }
}
