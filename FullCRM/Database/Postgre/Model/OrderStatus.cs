using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.Model
{
    public enum OrderStatus
    {
        [Description("В исполнении")]
        Executable = 0,

        [Description("Завершен")]
        Completed = 1,

        [Description("Отменен")]
        Cancelled = 2
    }
}
