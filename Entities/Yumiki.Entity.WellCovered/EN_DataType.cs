using System.ComponentModel;
using Yumiki.Commons.Entities;

namespace Yumiki.Entity.WellCovered
{
    public enum EN_DataType
    {
        [Description("Integer")]
        [MappingValue("int")]
        E_INT,

        [Description("Decimal")]
        [MappingValue("decimal")]
        E_DECIMAL,

        [Description("String")]
        [MappingValue("nvarchar")]
        E_STRING,

        [Description("Boolean")]
        [MappingValue("bit")]
        E_BOOL,

        [Description("Date")]
        [MappingValue("date")]
        E_DATE,

        [Description("Date Time")]
        [MappingValue("datetime")]
        E_DATETIME,

        [Description("Time")]
        [MappingValue("time")]
        E_TIME,

        [Description("Datasource")]
        [MappingValue("nvarchar")]
        E_DATASOURCE
    }
}
