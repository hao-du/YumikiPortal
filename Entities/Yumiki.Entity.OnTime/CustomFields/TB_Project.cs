namespace Yumiki.Entity.OnTime
{
    public partial class TB_Project
    {
        /// <summary>
        /// For User Assignment Purpose to check if user is assigned to specific project
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
