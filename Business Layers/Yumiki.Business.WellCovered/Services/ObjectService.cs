using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Business.WellCovered.Services
{
    public class ObjectService : BaseService<IObjectRepository>, IObjectService
    {
        /// <summary>
        /// Get all active Objects from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Objects or active Objects.</param>
        /// <returns>List of all active Objects.</returns>
        public IEnumerable<TB_Object> GetAllObjects(bool showInactive, Guid userID, string appID)
        {
            Guid? convertedAppID = null;

            if (!string.IsNullOrWhiteSpace(appID))
            {
                Guid convertedID = Guid.Empty;
                Guid.TryParse(appID, out convertedID);
                convertedAppID = convertedID;
            }

            return Repository.GetAllObjects(showInactive, userID, convertedAppID);
        }

        /// <summary>
        /// Return specific Object by id.
        /// </summary>
        /// <param name="id">TB_Object Guid ID.</param>
        /// <returns>Result with TB_Object type.</returns>
        public TB_Object GetObjectByID(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Object ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(id, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Object ID must be GUID type.", Logger);
            }

            return  Repository.GetObjectByID(convertedID);
        }

        /// <summary>
        /// Save Object to DB
        /// </summary>
        /// <param name="obj">If Object id is empty, then this is new Object. Otherwise, this needs to be updated</param>
        public void Save(TB_Object obj)
        {
            if (string.IsNullOrWhiteSpace(obj.ObjectName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Object Name cannot be empty.", Logger);
            }

            if (string.IsNullOrWhiteSpace(obj.DisplayName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Display Name cannot be empty.", Logger);
            }

            if (string.IsNullOrWhiteSpace(obj.ApiName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Api Name cannot be empty.", Logger);
            }

            if (Repository.Any(obj))
            {
                throw new YumikiException(ExceptionCode.E_DUPLICATED, "Api Name have already been used.", Logger);
            }

            if(obj.AppID == Guid.Empty)
            {
                obj.AppID = null;
            }

            Repository.Save(obj);
        }

        /// <summary>
        /// Get all active and sharable app to be a datasource
        /// </summary>
        public IEnumerable<TB_App> GetApps(string userID)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "User ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(userID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "User ID must be GUID type.", Logger);
            }

            return Repository.GetApps(convertedID);
        }
    }
}
