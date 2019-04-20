using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FullCRM.Database.Sql.Filtering.Abstract
{
    public abstract class FilterCollection<T> : Filter<T>
    {
        public List<Filter<T>> Filters { get; set; }

        internal override Expression ToInnerExpression(ParameterExpression param)
        {
            var expressions = Filters.Select(x => x.ToInnerExpression(param));

            Expression exp = expressions.Aggregate((current, item) =>
            {
                var temp = BinaryOperation(current, item);
                return temp;
            });

            return exp;
        }

        protected abstract BinaryExpression BinaryOperation(Expression left, Expression right);
    }
}
