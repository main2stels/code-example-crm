using System;
using System.Linq.Expressions;
using System.Linq;
using FullCRM.Database.Sql.Filtering.Abstract;

namespace FullCRM.Database.Sql.Filtering.Filters
{
    /// <summary>
    /// Равно
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GTE<T, TField> : FilterCondition<T, TField>
    {
        internal override Expression ToInnerExpression(ParameterExpression param)
        {
            var constant = Expression.Constant(Value, typeof(TField));

            return Expression.GreaterThanOrEqual(Expression.Property(param, Field), constant);
        }
    }
}
