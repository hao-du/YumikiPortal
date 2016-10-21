namespace Yumiki.Entity.Administration
{
    using Base;
    using System;

    public partial class TB_ContactType : IEntity
    {
        public Guid ID { get; set; }
        public string ContactTypeName { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
