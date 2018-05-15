using System;

namespace Yumiki.Web.Ontime.Models
{
    public class MD_ProjectAssignment
    {
        public string UserID { get; set; }
        public string ProjectID { get; set; }
        public bool IsAssigned { get; set; }
    }
}