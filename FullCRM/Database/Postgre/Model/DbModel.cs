using LinqToDB.Mapping;

namespace FullCRM.Database.Postgre.Model
{
    public class DbModel 
    {
        [Identity]
        [PrimaryKey]
        public virtual long Id { get; set; }
    }
}
