using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.Model
{
    public enum PaymentStatus
    {
        [Description("Оплачен")]
        Paid = 0,

        [Description("Не оплачен")]
        NotPaid = 1,

        [Description("Частично оплачен")]
        PartiallyPaid = 2,

        [Description("Не выставлен")]
        NotSet = 3,

        [Description("Ожидание оплаты")]
        Expects = 4,

        [Description("Отменен")]
        Cancelled = 5,

    }
}
