namespace Yumiki.Entity.WellCovered
{
    using System;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_Field
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
            }
        }

        public object Value
        {
            get; set;
        }

        public bool DataSortByASC {
            get
            {
                if(this.DataSortByOrder < 0)
                {
                    return false;
                }
                return true;
            }
        }

        public int DataSortByPriority
        {
            get
            {
                return Math.Abs(this.DataSortByOrder.HasValue ? this.DataSortByOrder.Value : 0);
            }
        }

        public class FieldNames
        {
            public const string FieldType = "FieldType";
            public const string Value = "Value";
        }
    }
}
