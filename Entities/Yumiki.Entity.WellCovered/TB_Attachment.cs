namespace Yumiki.Entity.WellCovered
{
    using System;
    using Yumiki.Entity.Base;

    public partial class TB_Attachment : IEntity
    {
        public Guid ID { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public Guid ReferenceObjectID { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
