namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_TransactionType : IEntity
    {
        public TB_TransactionType()
        {
            TB_Category = new HashSet<TB_Category>();
        }

        public Guid ID { get; set; }

        public string TransactionTypeName { get; set; }

        public bool IsIncome { get; set; }

        public bool IsTransfer { get; set; }

        public Guid? UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<TB_Category> TB_Category { get; set; }

        public virtual TB_User TB_User { get; set; }
    }
}
