using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class PrivilegeService : BaseService<IPrivilegeRepository>, IPrivilegeService
    {
        /// <summary>
        /// Get all active privileges from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive privileges or active privileges</param>
        /// <param name="parentID">Get privileges from Parent Privilege - Guid.Empty is Root, NULL to ignore this filter</param>
        /// <returns>List of all active privilege</returns>
        public List<TB_Privilege> GetAllPrivileges(bool showInactive, string parentID)
        {
            Guid? parentGUID = null;

            if (!string.IsNullOrWhiteSpace(parentID))
            {
                parentGUID = Guid.Parse(parentID);
            }

            return Repository.GetAllPrivileges(showInactive, parentGUID);
        }

        /// <summary>
        /// Get specific privilege from Database
        /// </summary>
        /// <param name="id">Privilege ID - Must be Guid value</param>
        /// <returns>Privilege object</returns>
        public TB_Privilege GetPrivilege(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Privilege ID cannot be empty.", Logger);
            }

            Guid privilegeID = Guid.Empty;
            Guid.TryParse(id, out privilegeID);
            if (privilegeID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Privilege ID must be GUID type.", Logger);
            }

            return Repository.GetPrivilege(privilegeID);
        }

        /// <summary>
        /// Create/Update a privilege
        /// </summary>
        /// <param name="privilege">If privilege id is empty, then this is new privilege. Otherwise, this needs to be updated</param>
        public void SavePrivilege(TB_Privilege privilege)
        {
            if (string.IsNullOrWhiteSpace(privilege.PrivilegeName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Privilege Name cannot be empty.", Logger);
            }
            if (string.IsNullOrWhiteSpace(privilege.PagePath))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Page Path cannot be empty.", Logger);
            }

            if (!Repository.CheckValidPrivilegeName(privilege.PrivilegeName, privilege.PagePath, privilege.ID))
            {
                throw new YumikiException(ExceptionCode.E_DUPLICATED, "Privilege Name or Page Path was used.", Logger);
            }

            //If parent id is Guild.Empty,it means this is the root node, so need to assign NULL value.
            if(privilege.ParentPrivilegeID.HasValue && privilege.ParentPrivilegeID.Value == Guid.Empty)
            {
                privilege.ParentPrivilegeID = null;
            }

            Repository.SavePrivilege(privilege);
        }
    }
}
