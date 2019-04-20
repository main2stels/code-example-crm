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
    public class Contains<T> : StringFilterCondition<T>
    {
        internal override Expression ToInnerExpression(ParameterExpression param)
        {
            var toLowerExpression = Expression.Call(Expression.Property(param, Field), _toLowerMethod);

            return Expression.Call(toLowerExpression, typeof(String).GetMethod("Contains", BindingFlags.Public | BindingFlags.Instance), Expression.Constant(Value.ToLower()));
        }
    }
}
