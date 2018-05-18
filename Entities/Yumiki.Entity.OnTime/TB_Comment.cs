namespace Yumiki.Entity.OnTime
{
    using System;
    using Yumiki.Entity.Base;

    public partial class TB_Comment : IEntity
    {
        public Guid ID { get; set; }

        public string Comment { get; set; }

        public Guid TaskID { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_Task Task { get; set; }

        public virtual TB_User User { get; set; }
    }
}
