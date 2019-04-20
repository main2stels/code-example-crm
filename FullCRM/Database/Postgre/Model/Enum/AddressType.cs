using System.ComponentModel;

namespace FullCRM.Database.Postgre.Model.Enum
{
    public enum AddressType
    {
        [Description("Actual")]
        Actual = 0,
        [Description("Legal")]
        Legal = 1,
        [Description("Delivery")]
        Delivery = 2,
    }
}
