namespace Yumiki.Entity.Administration
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_UserAddress : IEntity
    {
        public TB_UserAddress()
        {
            TB_UserAddress1 = new HashSet<TB_UserAddress>();
        }

        public Guid ID { get; set; }
        public string UserAddress { get; set; }
        public Guid UserAddressTypeID { get; set; }
        public Guid UserID { get; set; }
        public bool IsPrimary { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual TB_User TB_User { get; set; }
        public virtual ICollection<TB_UserAddress> TB_UserAddress1 { get; set; }
        public virtual TB_UserAddress TB_UserAddress2 { get; set; }
    }
}
