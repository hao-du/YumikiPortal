namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_User : IEntity
    {
        public TB_User()
        {
            Currency = new HashSet<TB_Currency>();
            Tags = new HashSet<TB_Tag>();
            Traces = new HashSet<TB_Trace>();
        }

        public Guid ID { get; set; }

        public string UserLoginName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<TB_Currency> Currency { get; set; }

        public virtual ICollection<TB_Tag> Tags { get; set; }

        public virtual ICollection<TB_Trace> Traces { get; set; }
    }
}
