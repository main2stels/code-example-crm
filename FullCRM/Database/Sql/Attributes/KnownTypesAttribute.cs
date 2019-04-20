using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Sql.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class KnownTypesAttribute : Attribute
    {
        public Type[] Types { get; private set; }

        public KnownTypesAttribute(params Type[] types)
        {
            Types = types;
        }
    }
}
