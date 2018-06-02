namespace Yumiki.Entity.OnTime
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_Project : IEntity
    {
        public TB_Project()
        {
            ProjectAssignments = new HashSet<TB_ProjectAssignment>();
            Tasks = new HashSet<TB_Task>();
        }

        public Guid ID { get; set; }

        public string ProjectName { get; set; }

        public string Prefix { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_User User { get; set; }

        public virtual ICollection<TB_ProjectAssignment> ProjectAssignments { get; set; }

        public virtual ICollection<TB_Task> Tasks { get; set; }
    }
}
