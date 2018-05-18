namespace Yumiki.Entity.OnTime
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_Task : IEntity
    {
        public TB_Task()
        {
            Histories = new HashSet<TB_History>();
            Comments = new HashSet<TB_Comment>();
        }

        public Guid ID { get; set; }

        public string TaskName { get; set; }

        public Guid ProjectID { get; set; }

        public Guid PhaseID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public EN_TaskStatus Status { get; set; }

        public EN_Priority Priority { get; set; }

        public string TaskDescriptions { get; set; }

        public Guid? AssignedUserID { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<TB_History> Histories { get; set; }

        public virtual TB_Phase Phase { get; set; }

        public virtual TB_Project Project { get; set; }

        public virtual TB_User User { get; set; }

        public virtual TB_User AssignedUser { get; set; }

        public virtual ICollection<TB_Comment> Comments { get; set; }
    }
}
