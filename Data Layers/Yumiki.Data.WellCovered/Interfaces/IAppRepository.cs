using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Interfaces
{
    public interface IAppRepository
    {
        /// <summary>
        /// Get all active Apps from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Apps or active Apps.</param>
        /// <returns>List of all active Apps.</returns>
        IEnumerable<TB_App> GetAllApps(bool showInactive, Guid userID, bool getShareable);

        /// <summary>
        /// Return specific app by id.
        /// </summary>
        /// <param name="id">TB_App Guid ID.</param>
        /// <returns>Result with TB_App type.</returns>
        TB_App GetAppByID(Guid id);

        /// <summary>
        /// Save app to DB
        /// </summary>
        /// <param name="app">If app id is empty, then this is new app. Otherwise, this needs to be updated</param>
        void Save(TB_App app);
    }
}
