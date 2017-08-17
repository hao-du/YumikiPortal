namespace Yumiki.Entity.WellCovered
{
    using System;
    using Yumiki.Entity.Base;

    public partial class TB_Field : IEntity
    {
        public Guid ID { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public string ApiName { get; set; }
        public Guid ObjectID { get; set; }
        /// <summary>
        /// Source is from EN_DataType
        /// </summary>
        public EN_DataType FieldType { get; set; }
        public int? FieldLength { get; set; }
        public bool IsRequired { get; set; }

        /// <summary>
        /// If FieldType is E_DATASOURCE, then Datasource is required.
        /// Format:
        /// 1. Simple Datasource: <<list>>A,B,C,D,E,F
        /// 2. Link Datasource: <<link>>TB_A
        /// </summary>
        public string Datasource { get; set; }
        public string Descriptions { get; set; }
        public Guid UserID { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemField { get; set; }
        public int? FieldOrder { get; set; }
        public bool IsDisplayable { get; set; }

        /// <summary>
        /// Make the field searchable by add it to search index.
        /// </summary>
        public bool CanIndex { get; set; }
        /// <summary>
        /// To tokenize the field's value to temrs or search as it is.
        /// </summary>
        public int? Analyzer { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual TB_Object Object { get; set; }
        public virtual TB_User User { get; set; }
    }
}
