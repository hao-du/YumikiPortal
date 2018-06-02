using System.ComponentModel;

namespace Yumiki.Entity.OnTime
{
    public enum EN_PhaseStatus
    {
        [Description("Backlog")]
        E_BACKLOG = 1,
        [Description("In Progress")]
        E_IN_PROGRESS = 2,
        [Description("Finished")]
        E_FINISHED = 3,
        [Description("Released")]
        E_RELEASED = 4
    }
}
