namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_Category : IEntity
    {
        public TB_Category()
        {
            TB_Trace = new HashSet<TB_Trace>();
        }

        public Guid ID { get; set; }

        public string CategoryName { get; set; }

        public string Descriptions { get; set; }

        public Guid? UserID { get; set; }

        public Guid TransactionTypeID { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_TransactionType TB_TransactionType { get; set; }

        public virtual TB_User TB_User { get; set; }

        public virtual ICollection<TB_Trace> TB_Trace { get; set; }
    }
}
