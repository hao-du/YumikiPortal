using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.OnTime.ServiceObjects
{
    public class GetUserWithAssignmentResponse
    {
        public GetUserWithAssignmentResponse()
        {
            UserWithAssignments = null;
            ProjectAssignments = new List<TB_Project>();
            PhaseAssignments = new List<TB_Phase>();
        }

        public TB_User UserWithAssignments { get; set; }
        public List<TB_Project> ProjectAssignments { get; set; }
        public List<TB_Phase> PhaseAssignments { get; set; }
    }
}
