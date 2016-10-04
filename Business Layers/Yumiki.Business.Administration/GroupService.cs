using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Administration;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration
{
    public class GroupService
    {
        public List<TB_Group> GetAllGroups()
        {
            GroupRepository groupRepository = new GroupRepository();
            return groupRepository.GetAllGroups();
        }
    }
}
