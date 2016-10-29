using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Common.Helper;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class PrivilegeService : BaseService, IPrivilegeService
    {
        private IPrivilegeRepository repository;
        private IPrivilegeRepository Repository
        {
            get
            {
                if (repository == null)
                {
                    repository = Service.GetInstance<IPrivilegeRepository>();
                }
                return repository;
            }
        }

        /// <summary>
        /// Get all active privileges from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive privileges or active privileges</param>
        /// <param name="parentID">Get privileges from Parent Privilege - Guid.Empty is Root, NULL to ignore this filter</param>
        /// <returns>List of all active privilege</returns>
        public List<TB_Privilege> GetAllPrivileges(bool showInactive, string parentID)
        {
            Guid? parentGUID = null;

            if (!string.IsNullOrEmpty(parentID))
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
            if (string.IsNullOrEmpty(id))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "Privilege ID cannot be empty.", null);
            }

            Guid privilegeID = Guid.Empty;
            Guid.TryParse(id, out privilegeID);
            if (privilegeID == Guid.Empty)
            {
                throw new AdvanceException(ExceptionCode.E_WRONG_TYPE, "Privilege ID must be GUID type.", null);
            }

            return Repository.GetPrivilege(privilegeID);
        }

        /// <summary>
        /// Create/Update a privilege
        /// </summary>
        /// <param name="privilege">If privilege id is empty, then this is new privilege. Otherwise, this needs to be updated</param>
        public void SavePrivilege(TB_Privilege privilege)
        {
            if (string.IsNullOrEmpty(privilege.PrivilegeName))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "Privilege Name cannot be empty.", null);
            }
            if (string.IsNullOrEmpty(privilege.PagePath))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "Page Path cannot be empty.", null);
            }

            if (!Repository.CheckValidPrivilegeName(privilege.PrivilegeName, privilege.PagePath, privilege.ID))
            {
                throw new AdvanceException(ExceptionCode.E_DUPLICATED, "Privilege Name or Page Path was used.", null);
            }

            Repository.SavePrivilege(privilege);
        }
    }
}
