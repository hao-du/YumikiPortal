using System.ComponentModel;

namespace Yumiki.Entity.WellCovered
{
    public enum EN_DataType
    {
        [Description("Integer")]
        E_INT,
        [Description("Decimal")]
        E_DECIMAL,
        [Description("String")]
        E_STRING,
        [Description("Boolean")]
        E_BOOL,
        [Description("Date")]
        E_DATE,
        [Description("Date Time")]
        E_DATETIME,
        [Description("Time")]
        E_TIME,
        [Description("Datasource")]
        E_DATASOURCE
    }
}
