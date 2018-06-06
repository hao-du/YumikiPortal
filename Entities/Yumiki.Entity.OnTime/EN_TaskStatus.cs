using System.ComponentModel;

namespace Yumiki.Entity.OnTime
{
    public enum EN_TaskStatus
    {
        [Description("Backlog")]
        E_BACKLOG = 1,
        [Description("In Progress")]
        E_IN_PROGRESS = 2,
        [Description("Code Completed")]
        E_CODE_COMPLETED = 3,
        [Description("Ready to Test")]
        E_READY_TO_TEST = 4,
        [Description("Failed Testing")]
        E_TESTING = 5,
        [Description("Testing")]
        E_FAILED_TESTING = 6,
        [Description("Ready to Release")]
        E_READY_TO_RELEASED = 7,
        [Description("Released")]
        E_RELEASED = 8,
        [Description("On Hold")]
        E_ON_HOLD = 9
    }
}
