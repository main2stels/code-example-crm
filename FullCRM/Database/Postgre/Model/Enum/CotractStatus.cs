using System.ComponentModel;

namespace FullCRM.Database.Postgre.Model.Enum
{
    public enum ContractStatus
    {
        [Description("Подготовка")]
        Preparation = 0,

        [Description("None")]
        None = 1,

        [Description("Подписан")]
        Signed = 2,

        [Description("Отменен")]
        Cancel = 3,

        [Description("Ожидает подписания")]
        Expects = 4,

    }
}
