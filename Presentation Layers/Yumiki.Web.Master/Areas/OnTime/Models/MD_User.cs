using System.Collections.Generic;
using Yumiki.Entity.OnTime;

namespace Yumiki.Web.OnTime.Models
{
    public class MD_User : MD_OnTimeBase<TB_User>
    {
        public MD_User()
        {
            this._internalItem = new TB_User();
        }

        public MD_User(TB_User user)
        {
            _internalItem = user;
        }

        public string UserName
        {
            get
            {
                return _internalItem.UserLoginName;
            }
        }

        public IEnumerable<MD_Project> ProjectAssignments
        {
            get; set;
        }

        public IEnumerable<MD_Phase> PhaseAssignments
        {
            get; set;
        }

        public string FullName
        {
            get
            {
                return $"{_internalItem.FirstName} {_internalItem.LastName}".Trim();
            }
        }
    }
}