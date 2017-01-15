namespace Yumiki.Entity.Administration
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_Queue : IEntity
    {
        public TB_Queue()
        {
        }

        public Guid ID { get; set; }
        public EN_QueueType QueueType { get; set; }
        public Guid UserID { get; set; }
        public virtual TB_User User { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
