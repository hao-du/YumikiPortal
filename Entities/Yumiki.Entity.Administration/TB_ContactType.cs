namespace Yumiki.Entity.Administration
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_ContactType : IEntity
    {
        public TB_ContactType()
        {
            UserAddresses = new HashSet<TB_UserAddress>();
        }

        public Guid ID { get; set; }
        public string ContactTypeName { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual ICollection<TB_UserAddress> UserAddresses { get; set; }
    }
}
