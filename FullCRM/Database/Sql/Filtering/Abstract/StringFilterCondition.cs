using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FullCRM.Database.Sql.Filtering.Abstract
{
    public abstract class StringFilterCondition<T> : FilterCondition<T, string>
    {
        protected static MethodInfo _toLowerMethod = typeof(String).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                                   .FirstOrDefault(x => x.Name == "ToLower" && x.GetParameters().Length == 0);
    }
}
