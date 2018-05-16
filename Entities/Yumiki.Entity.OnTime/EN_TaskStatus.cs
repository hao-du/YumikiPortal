using System.ComponentModel;

namespace Yumiki.Entity.OnTime
{
    public enum EN_TaskStatus
    {
        [Description("Backlog")]
        E_BACKLOG = 1,
        [Description("In Progress")]
        E_IN_PROGRESS = 2,
        [Description("Ready to Test")]
        E_READY_TO_TEST = 3,
        [Description("Failed Testing")]
        E_FAILED_TESTING = 4,
        [Description("Ready to Release")]
        E_READY_TO_RELEASED = 5,
        [Description("On Hold")]
        E_ON_HOLD = 6
    }
}
