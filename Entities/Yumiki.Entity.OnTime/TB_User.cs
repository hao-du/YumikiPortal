namespace Yumiki.Entity.OnTime
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_User : IEntity
    {
        public TB_User()
        {
            Phases = new HashSet<TB_Phase>();
            PhaseAssignments = new HashSet<TB_PhaseAssignment>();
            Projects = new HashSet<TB_Project>();
            ProjectAssignments = new HashSet<TB_ProjectAssignment>();
            Tasks = new HashSet<TB_Task>();
            AssignedTasks = new HashSet<TB_Task>();
        }

        public Guid ID { get; set; }

        public string UserLoginName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string TimeZone { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<TB_Phase> Phases { get; set; }

        public virtual ICollection<TB_PhaseAssignment> PhaseAssignments { get; set; }

        public virtual ICollection<TB_Project> Projects { get; set; }

        public virtual ICollection<TB_ProjectAssignment> ProjectAssignments { get; set; }

        public virtual ICollection<TB_Task> Tasks { get; set; }

        public virtual ICollection<TB_Task> AssignedTasks { get; set; }
    }
}
