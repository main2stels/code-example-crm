using System.ComponentModel;

namespace FullCRM.Models.WebSocket
{
    public enum Mode
    {
        [Description("Добавлен")]
        Create = 1,

        [Description("Удален")]
        Delete = 2,

        [Description("Изменен")]
        Edit = 3
    }
}
