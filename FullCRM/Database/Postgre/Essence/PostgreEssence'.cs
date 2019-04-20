using FullCRM.Database.Postgre.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.PostgreEssence
{
    public class PostgreEssence<T> : PostgreEssence<InventoryDb, T> where T : DbModel
    {
        public PostgreEssence(InventoryDb db)
            : base(db)
        {
        }
    }
}
