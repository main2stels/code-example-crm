using FullCRM.Database.Sql.Filtering.Abstract;
using System.Linq.Expressions;

namespace FullCRM.Database.Sql.Filtering
{
    public class OrFilterCollection<T> : FilterCollection<T>
    {
        protected override BinaryExpression BinaryOperation(Expression left, Expression right)
        {
            return Expression.Or(left, right);
        }
    }
}
