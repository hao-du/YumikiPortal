using System;

namespace Yumiki.Web.OnTime.Models
{
    public class MD_PhaseAssignment
    {
        public string UserID { get; set; }
        public string PhaseID { get; set; }
        public bool IsAssigned { get; set; }
    }
}