using FullCRM.Database.Postgre.Essence;
using FullCRM.Database.Postgre.Model;
using FullCRM.Database.Postgre.PostgreEssence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.Extensions
{
    public static class EssenseExtensions
    {
        public static long InsertWithIdentity<TModel>(this Essence<TModel> essence, TModel model) where TModel : DbModel
        {
            return ((IPostgreEssence<TModel>)essence).InsertWithIdentity(model);
        }
    }
}
