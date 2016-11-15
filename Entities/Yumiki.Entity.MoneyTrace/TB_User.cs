namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_User : IEntity
    {
        public TB_User()
        {
            TB_Category = new HashSet<TB_Category>();
            TB_Currency = new HashSet<TB_Currency>();
            TB_Trace = new HashSet<TB_Trace>();
            TB_TransactionType = new HashSet<TB_TransactionType>();
        }

        public Guid ID { get; set; }

        public string UserLoginName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<TB_Category> TB_Category { get; set; }

        public virtual ICollection<TB_Currency> TB_Currency { get; set; }

        public virtual ICollection<TB_Trace> TB_Trace { get; set; }

        public virtual ICollection<TB_TransactionType> TB_TransactionType { get; set; }
    }
}
