using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using FullCRM.Database.Sql.Filtering.Abstract;

namespace FullCRM.Database.Sql.Filtering.Filters
{
    /// <summary>
    /// Содержит (не чувствительно к регистру)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EndsWith<T> : StringFilterCondition<T>
    {
        internal override Expression ToInnerExpression(ParameterExpression param)
        {
            var methodInfo = typeof(String).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                           .FirstOrDefault(x => x.Name == "EndsWith" && x.GetParameters().Length == 1);

            var toLowerExpression = Expression.Call(Expression.Property(param, Field), _toLowerMethod);

            return Expression.Call(toLowerExpression, methodInfo, Expression.Constant(Value.ToLower()));
        }
    }
}