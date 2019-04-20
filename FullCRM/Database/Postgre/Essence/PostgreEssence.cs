using FullCRM.Database.Postgre.Essence;
using FullCRM.Database.Postgre.Model;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.PostgreEssence
{
    public class PostgreEssence<TDb, TModel> : Essence<TModel>, IPostgreEssence<TModel>
        where TModel : DbModel
        where TDb : DataConnection
    {
        public PostgreEssence(TDb db)
            : base(db)
        {
        }

        public virtual long InsertWithIdentity(TModel model)
        {
            return (long)_dbContext.InsertWithIdentity(model);
        }

        public override void Save(TModel model, Expression<Func<TModel, bool>> query)
        {
            var exist = _table.FirstOrDefault(query);
            if (exist != null)
            {
                model.Id = exist.Id;
                base.Update(model);
            }
            else
            {
                base.Insert(model);
            }
        }

        protected override Expression<Func<TModel, bool>> IdQuery(TModel model)
        {
            return x => x.Id == model.Id;
        }
    }
}
