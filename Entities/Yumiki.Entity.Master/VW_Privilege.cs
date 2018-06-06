using System;

namespace Yumiki.Entity.Master
{
    public partial class VW_Privilege
    {
        public Guid ID { get; set; }
        public string PrivilegeName { get; set; }
        public string PagePath { get; set; }
        public bool IsDisplayable { get; set; }
        public Guid? ParentPrivilegeID { get; set; }
        public Guid UserID { get; set; }
        public int PrivilegeOrder { get; set; }
    }
}
