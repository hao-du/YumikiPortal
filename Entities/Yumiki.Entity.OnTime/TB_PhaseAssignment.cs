namespace Yumiki.Entity.OnTime
{
    using System;
    using Yumiki.Entity.Base;

    public partial class TB_PhaseAssignment : IEntity
    {
        public Guid ID { get; set; }

        public Guid? PhaseID { get; set; }

        public Guid? UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_Phase Phase { get; set; }

        public virtual TB_User User { get; set; }
    }
}
