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
            TransferredTraces = new HashSet<TB_Trace>();
            BankAccounts = new HashSet<TB_BankAccount>();
        }

        public Guid ID { get; set; }

        public string UserLoginName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string TimeZone { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<TB_Currency> Currency { get; set; }

        public virtual ICollection<TB_Tag> Tags { get; set; }

        public virtual ICollection<TB_Trace> Traces { get; set; }

        public virtual ICollection<TB_BankAccount> BankAccounts { get; set; }

        public virtual ICollection<TB_Trace> TransferredTraces { get; set; }
    }
}
