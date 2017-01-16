namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_Trace : IEntity
    {
        public Guid ID { get; set; }

        public decimal Amount { get; set; }

        public DateTime TraceDate { get; set; }

        public Guid UserID { get; set; }

        public Guid? TransferredUserID { get; set; }

        public string Tags { get; set; }

        public Guid CurrencyID { get; set; }

        public EN_TransactionType TransactionType { get; set; }

        public string Descriptions { get; set; }

        public Guid? GroupTokenID { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public Guid? BankID { get; set; }

        public virtual TB_Bank Bank { get; set; }

        public virtual TB_Currency Currency { get; set; }

        public virtual TB_User User { get; set; }

        public virtual TB_User TransferredUser { get; set; }
    }
}
