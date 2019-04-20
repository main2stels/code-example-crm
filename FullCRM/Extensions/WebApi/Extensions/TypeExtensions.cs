using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FullCRM.Extensions.WebApi.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Создание Expression, получающего свойство модели,
        /// по типу свойства и типу результирующего Expression
        /// </summary>
        /// <param name="expressionType">Тип Expression, должен соответствовать шаблону Expression&lt;Func&lt;ModelType,PropertyType&gt;&gt;&gt;</param>
        /// <param name="propertyName">Название свойства</param>
        /// <returns></returns>
        public static Expression GetLambdaProperty(this Type expressionType, string propertyName)
        {
            // Получаем первый вложенный тип, предположительно Func
            Type func = expressionType.GetGenericArguments().FirstOrDefault();

            if (func == null)
            {
                throw new ArgumentException("Некорректный тип модели", "modelType");
            }

            Type[] elements = func.GetGenericArguments();

            if (elements.Length < 2)
            {
                throw new ArgumentException("Некорректный тип модели", "modelType");
            }

            return propertyName.GetPropertyExpression(elements[0], elements[1]);
        }
    }
}
