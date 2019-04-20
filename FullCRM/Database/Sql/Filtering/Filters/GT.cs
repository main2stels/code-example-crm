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
    public class GT<T, TField> : FilterCondition<T, TField>
    {
        internal override Expression ToInnerExpression(ParameterExpression param)
        {
            var constant = Expression.Constant(Value, typeof(TField));

            return Expression.GreaterThan(Expression.Property(param, Field), constant);
        }
    }
}