namespace Yumiki.Entity.Administration
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_UserAddress : IEntity
    {
        public TB_UserAddress()
        {
        }

        public Guid ID { get; set; }
        public string UserAddress { get; set; }
        public Guid UserContactTypeID { get; set; }
        public Guid UserID { get; set; }
        public bool IsPrimary { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual TB_User User { get; set; }
        public virtual TB_ContactType ContactType { get; set; }
    }
}
