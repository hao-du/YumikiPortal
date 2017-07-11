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
    public class AppService : BaseService<IAppRepository>, IAppService
    {
        /// <summary>
        /// Get all active Apps from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Apps or active Apps.</param>
        /// <returns>List of all active Apps.</returns>
        public IEnumerable<TB_App> GetAllApps(bool showInactive, Guid userID, bool getShareable)
        {
            return Repository.GetAllApps(showInactive, userID, getShareable);
        }

        /// <summary>
        /// Return specific app by id.
        /// </summary>
        /// <param name="id">TB_App Guid ID.</param>
        /// <returns>Result with TB_App type.</returns>
        public TB_App GetAppByID(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Application ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(id, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Application ID must be GUID type.", Logger);
            }

            return  Repository.GetAppByID(convertedID);
        }

        /// <summary>
        /// Save app to DB
        /// </summary>
        /// <param name="app">If app id is empty, then this is new app. Otherwise, this needs to be updated</param>
        public void Save(TB_App app)
        {
            Repository.Save(app);
        }
    }
}
