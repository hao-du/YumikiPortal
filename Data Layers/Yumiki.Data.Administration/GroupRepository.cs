using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration
{
    public class GroupRepository : AdministrationRepository
    {
        public List<TB_Group> GetAllGroups()
        {
            return Context.TB_Group.Where(c => c.IsActive).ToList();
        }
    }
}
