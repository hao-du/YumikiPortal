namespace Yumiki.Entity.Administration
{
    using Base;
    using System;

    public partial class TB_PasswordHistory : IEntity
    {
        public Guid ID { get; set; }
        public string Password { get; set; }
        public Guid UserID { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual TB_User User { get; set; }
    }
}
