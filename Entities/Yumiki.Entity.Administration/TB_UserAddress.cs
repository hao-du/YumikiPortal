namespace Yumiki.Entity.Administration
{
    using System;
    using System.Collections.Generic;

    public partial class TB_UserAddress
    {
        public Guid ID { get; set; }
        public string UserAddress { get; set; }
        public int? AddressType { get; set; }
        public Guid UserID { get; set; }
        public bool IsPrimary { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual TB_User TB_User { get; set; }
    }
}
