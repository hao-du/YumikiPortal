namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Entity.Base;

    public partial class TB_AdditionalFee : IEntity
    {
        public Guid ID { get; set; }

        public Guid FeeTypeID { get; set; }

        public decimal Amount { get; set; }

        public DateTime FeeIssueDate { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_FeeType FeeType { get; set; }

        public virtual TB_User User { get; set; }
    }
}
