
namespace Yumiki.Entity.OnTime
{
    public partial class TB_Phase
    {
        /// <summary>
        /// For User Assignment Purpose to check if user is assigned to specific phase
        /// </summary>
        public bool IsAssigned
        {
            get; set;
        } = false;

        public class FieldName
        {
            public const string IsAssigned = "IsAssigned";
        }
    }
}
