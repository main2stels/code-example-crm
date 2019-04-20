using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.Model
{
    /// <summary>
    /// Признак удаления
    /// </summary>
    public enum Condition
    {
        [Description("Активен")]
        Live = 0,
        [Description("Удален")]
        Delete = 1
    }
}
