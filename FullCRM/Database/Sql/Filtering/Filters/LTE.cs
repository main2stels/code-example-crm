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
    public class LTE<T, TField> : FilterCondition<T, TField>
    {
        internal override Expression ToInnerExpression(ParameterExpression param)
        {
            var constant = Expression.Constant(Value, typeof(TField));

            return Expression.LessThanOrEqual(Expression.Property(param, Field), constant);
        }
    }
}