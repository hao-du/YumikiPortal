namespace Yumiki.Entity.OnTime
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_Phase : IEntity
    {
        public TB_Phase()
        {
            Histories = new HashSet<TB_History>();
            PhaseAssignments = new HashSet<TB_PhaseAssignment>();
            Tasks = new HashSet<TB_Task>();
        }

        public Guid ID { get; set; }

        public string PhaseName { get; set; }

        public DateTime? EstimatedStartDate { get; set; }

        public DateTime? EstimatedEndDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        public string ReleaseVersion { get; set; }

        public int Status { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<TB_History> Histories { get; set; }

        public virtual TB_User User { get; set; }

        public virtual ICollection<TB_PhaseAssignment> PhaseAssignments { get; set; }

        public virtual ICollection<TB_Task> Tasks { get; set; }
    }
}
