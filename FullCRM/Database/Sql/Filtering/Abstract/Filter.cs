using FullCRM.Database.Sql.Attributes;
using FullCRM.Database.Sql.Filtering.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FullCRM.Database.Sql.Filtering.Abstract
{
    [KnownTypes(typeof(AndFilterCollection<>),
                typeof(OrFilterCollection<>),
                typeof(EQ<,>),
                typeof(NEQ<,>),
                typeof(Contains<>),
                typeof(NotContain<>),
                typeof(StartsWith<>),
                typeof(EndsWith<>),
                typeof(GT<,>),
                typeof(GTE<,>),
                typeof(LT<,>),
                typeof(LTE<,>))]
    public abstract class Filter<T>
    {
        [ModelType]
        public string ModelType { get; set; }

        public Expression<Func<T, bool>> ToExpression(ParameterExpression param = null)
        {
            return ToLambda(param ?? GetParameter());
        }

        protected ParameterExpression GetParameter()
        {
            return Expression.Parameter(typeof(T), "x");
        }

        protected Expression<Func<T, bool>> ToLambda(ParameterExpression param)
        {
            return Expression.Lambda<Func<T, bool>>(ToInnerExpression(param), param);
        }

        internal abstract Expression ToInnerExpression(ParameterExpression param);
    }
}
