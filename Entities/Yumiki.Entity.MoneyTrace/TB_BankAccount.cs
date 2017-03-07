namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_BankAccount : IEntity
    {
        public TB_BankAccount()
        {
            Traces = new HashSet<TB_Trace>();
        }

        public Guid ID { get; set; }

        public string AccountNumber { get; set; }

        public DateTime DepositDate { get; set; }

        public DateTime WithdrawDate { get; set; }

        public decimal Amount { get; set; }

        public decimal? Interest { get; set; }

        public Guid UserID { get; set; }

        public Guid BankID { get; set; }

        public Guid CurrencyID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_Bank Bank { get; set; }

        public virtual TB_User User { get; set; }

        public virtual TB_Currency Currency { get; set; }

        public virtual ICollection<TB_Trace> Traces { get; set; }
    }
}
