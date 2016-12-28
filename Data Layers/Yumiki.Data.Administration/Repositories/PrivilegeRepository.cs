using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration.Repositories
{
    public class PrivilegeRepository : AdministrationRepository, IPrivilegeRepository
    {
        /// <summary>
        /// Get all active privileges from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive privileges or active privileges</param>
        /// <param name="parentID">Get privileges from Parent Privilege - Guid.Empty is Root</param>
        /// <returns>List of all active privilege</returns>
        public List<TB_Privilege> GetAllPrivileges(bool showInactive, Guid? parentID)
        {
            IQueryable<TB_Privilege> queryable = Context.TB_Privilege.Where(c => c.IsActive == !showInactive);

            if (parentID.HasValue)
            {
                if (parentID.Value.Equals(Guid.Empty))
                {
                    queryable = queryable.Where(c => c.ParentPrivilegeID == null);
                }
                else
                {
                    queryable = queryable.Where(c => c.ParentPrivilegeID == parentID.Value);
                }
            }

            return queryable.OrderBy(c => c.PagePath).ToList();
        }

        /// <summary>
        /// Get specific privilege from Database
        /// </summary>
        /// <param name="id">Privilege ID - Must be Guid value</param>
        /// <returns>Privilege object</returns>
        public TB_Privilege GetPrivilege(Guid id)
        {
            return Context.TB_Privilege.SingleOrDefault(c => c.ID == id);
        }

        /// <summary>
        /// Check duplicate privilege name and path between the values in UI and database
        /// </summary>
        /// <param name="privilegeName">Privilege Name will be saved into database</param>
        /// <param name="pagePath">Privilege Path will be saved into database</param>
        /// <param name="excludedPrivilegeID">ID which need to be excluded to check</param>
        /// <returns></returns>
        public bool CheckValidPrivilegeName(string privilegeName, string pagePath, Guid excludedPrivilegeID)
        {
            int countPrivilegeName = Context.TB_Privilege.Count(c => c.ID != excludedPrivilegeID
                                                        && (
                                                            c.PrivilegeName.ToLower() == privilegeName.ToLower()
                                                            || c.PagePath.ToLower() == pagePath.ToLower()
                                                            )
                                                        && c.IsActive);
            if (countPrivilegeName > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Create/Update a privilege
        /// </summary>
        /// <param name="privilege">If privilege id is empty, then this is new privilege. Otherwise, this needs to be updated</param>
        public void SavePrivilege(TB_Privilege privilege)
        {
            if (privilege.ID == Guid.Empty)
            {
                Context.TB_Privilege.Add(privilege);
            }
            else
            {
                TB_Privilege dbPrivilege = Context.TB_Privilege.Single(c => c.ID == privilege.ID);
                dbPrivilege.PrivilegeName = privilege.PrivilegeName;
                dbPrivilege.PagePath = privilege.PagePath;
                dbPrivilege.ParentPrivilegeID = privilege.ParentPrivilegeID;
                dbPrivilege.IsDisplayable = privilege.IsDisplayable;
                dbPrivilege.Descriptions = privilege.Descriptions;
                dbPrivilege.IsActive = privilege.IsActive;
            }

            Save();
        }
    }
}
