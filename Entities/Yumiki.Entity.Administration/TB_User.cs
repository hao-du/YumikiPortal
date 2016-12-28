namespace Yumiki.Entity.Administration
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_User : IEntity
    {
        public TB_User()
        {
            PasswordHistories = new HashSet<TB_PasswordHistory>();
            UserAddresses = new HashSet<TB_UserAddress>();
            Groups = new HashSet<TB_Group>();
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
        public virtual ICollection<TB_PasswordHistory> PasswordHistories { get; set; }
        public virtual ICollection<TB_UserAddress> UserAddresses { get; set; }
        public virtual ICollection<TB_Group> Groups { get; set; }
    }
}
