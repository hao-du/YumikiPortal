using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Repositories
{
    public class AppRepository : WellCoveredRepository, IAppRepository
    {
        /// <summary>
        /// Get all active Apps from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Apps or active Apps.</param>
        /// <returns>List of all active Apps.</returns>
        public IEnumerable<TB_App> GetAllApps(bool showInactive, Guid userID, bool getShareable)
        {
            IQueryable<TB_App> queryable = Context.TB_App.Where(c => c.IsActive == !showInactive);

            if (getShareable)
            {
                queryable = queryable.Where(c => c.UserID == userID || c.IsShareable == true);
            }
            else
            {
                queryable = queryable.Where(c => c.UserID == userID);
            }

            return queryable.AsEnumerable();
        }

        /// <summary>
        /// Return specific app by id
        /// </summary>
        /// <param name="id">TB_App Guid ID</param>
        /// <returns>Result with TB_App type</returns>
        public TB_App GetAppByID(Guid id)
        {
            return Context.TB_App.Where(c => c.ID == id).SingleOrDefault();
        }

        /// <summary>
        /// Check if App Name already existed in DB.
        /// </summary>
        /// <param name="app">app with name to check.</param>
        /// <returns></returns>
        public bool Any(TB_App app)
        {
            return Context.TB_App.Any(c => c.ID != app.ID && c.AppName == app.AppName);
        }

        /// <summary>
        /// Save app to DB
        /// </summary>
        /// <param name="app">If app id is empty, then this is new app. Otherwise, this needs to be updated</param>
        public void Save(TB_App app)
        {
            if (app.ID == Guid.Empty)
            {
                Context.TB_App.Add(app);
            }
            else
            {
                TB_App dbApp = Context.TB_App.Single(c => c.ID == app.ID);
                dbApp.AppName = app.AppName;
                dbApp.UserID = app.UserID;
                dbApp.IsShareable = app.IsShareable;
                dbApp.Descriptions = app.Descriptions;
                dbApp.IsActive = app.IsActive;
            }

            Save();
        }
    }
}
