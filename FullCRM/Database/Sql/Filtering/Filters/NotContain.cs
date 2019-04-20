using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FullCRM.Database.Sql.Filtering.Filters
{
    /// <summary>
    /// Содержит (не чувствительно к регистру)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotContain<T> : Contains<T>
    {
        internal override Expression ToInnerExpression(ParameterExpression param)
        {
            var exp = base.ToInnerExpression(param);

            return Expression.Equal(exp, Expression.Constant(false));
        }
    }
}
