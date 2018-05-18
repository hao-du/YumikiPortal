using System.ComponentModel;

namespace Yumiki.Entity.OnTime
{
    public enum EN_Priority
    {
        [Description("Low")]
        E_LOW = 1,
        [Description("Normal")]
        E_NORMAL = 2,
        [Description("High")]
        E_HIGH = 3,
        [Description("Urgent")]
        E_URGENT = 4
    }
}
