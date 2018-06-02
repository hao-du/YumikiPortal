using System;

namespace Yumiki.Web.OnTime.Models
{
    public class MD_ProjectAssignment
    {
        public string UserID { get; set; }
        public string ProjectID { get; set; }
        public bool IsAssigned { get; set; }
    }
}