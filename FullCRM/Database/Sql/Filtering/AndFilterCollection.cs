using FullCRM.Database.Sql.Filtering.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FullCRM.Database.Sql.Filtering
{
    public class AndFilterCollection<T> : FilterCollection<T>
    {
        protected override BinaryExpression BinaryOperation(Expression left, Expression right)
        {
            return Expression.AndAlso(left, right);
        }
    }
}
