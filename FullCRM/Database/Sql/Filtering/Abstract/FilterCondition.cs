using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Sql.Filtering.Abstract
{
    public abstract class FilterCondition<T, TField> : Filter<T>
    {
        public string Field { get; set; }

        public TField Value { get; set; }
    }
}
