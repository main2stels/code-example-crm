using System.ComponentModel;

namespace FullCRM.Database.Postgre.Model.Enum
{
    public enum ContractorType
    {
        [Description("Рекламное агенство")]
        AdvertisingAgency = 0,

        [Description("")]
        None = 1,

        [Description("Наша компания")]
        OurCompany = 2,

        [Description("Прямой клиент")]
        DirectClient = 3
    }
}
