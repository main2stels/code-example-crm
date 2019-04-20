using FullCRM.Database.Postgre.Model.Enum;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.Model
{
    [Table("NextNumberEssence", Schema = "fullCRM")]
    public class NextNumberEssence : DbModel
    {
        [Column]
        public IncrementType IncrementType { get; set; }
        
        [Column]
        public string NextNumber { get; set; }

        [Column]
        public string Prefix { get; set; }
    }
}
