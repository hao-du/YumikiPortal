namespace Yumiki.Entity.Administration
{
    using System;
    using System.Collections.Generic;

    public partial class TB_User
    {
        public TB_User()
        {
            TB_UserAddress = new HashSet<TB_UserAddress>();
            TB_Group = new HashSet<TB_Group>();
        }

        public Guid ID { get; set; }
        public string UserLoginName { get; set; }
        public string CurrentPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual ICollection<TB_UserAddress> TB_UserAddress { get; set; }
        public virtual ICollection<TB_Group> TB_Group { get; set; }
    }
}
