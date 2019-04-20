using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.Essence
{
    public interface IPostgreEssence<TModel>
    {
        long InsertWithIdentity(TModel model);
    }
}
