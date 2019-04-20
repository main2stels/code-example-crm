using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.MongoDB.Model.Enums
{
    public enum OperationType
    {
        // Not = 0,

        Create = 1,

        Update = 2,

        Upsert = 3,

        Delete = 4
    }
}
