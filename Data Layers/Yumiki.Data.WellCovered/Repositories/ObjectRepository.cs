using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Repositories
{
    public class ObjectRepository : WellCoveredRepository, IObjectRepository
    {
        /// <summary>
        /// Get all active Objects from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Objects or active Objects.</param>
        /// <returns>List of all active Objects.</returns>
        public IEnumerable<TB_Object> GetAllObjects(bool showInactive, Guid? appID)
        {
            IQueryable<TB_Object> objectQueryable = Context.TB_Object.Where(c => c.IsActive == !showInactive && (c.App == null || c.App.IsActive));

            if(appID.HasValue)
            {
                if (appID.Value.Equals(Guid.Empty))
                {
                    objectQueryable = objectQueryable.Where(c => !c.AppID.HasValue);
                }
                else
                {
                    objectQueryable = objectQueryable.Where(c => c.AppID == appID.Value);
                }
            }

            IEnumerable<TB_Object> objects = objectQueryable.ToList();

            return objects;
        }

        /// <summary>
        /// Return specific Object by id.
        /// </summary>
        /// <param name="id">TB_Object Guid ID.</param>
        /// <returns>Result with TB_Object type.</returns>
        public TB_Object GetObjectByID(Guid id)
        {
            return Context.TB_Object.Where(c => c.ID == id).SingleOrDefault();
        }

        /// <summary>
        /// Check if ApiName already existed in DB.
        /// </summary>
        /// <param name="obj">Object's ApiName to check.</param>
        /// <returns></returns>
        public bool Any(TB_Object obj)
        {
            return Context.TB_Object.Any(c => c.ApiName == obj.ApiName && c.ID != obj.ID);
        }

        /// <summary>
        /// Save Object to DB
        /// </summary>
        /// <param name="obj">If Object id is empty, then this is new Object. Otherwise, this needs to be updated</param>
        public void Save(TB_Object obj)
        {
            if (obj.ID == Guid.Empty)
            {
                Context.TB_Object.Add(obj);
            }
            else
            {
                TB_Object dbObject = Context.TB_Object.Single(c => c.ID == obj.ID);
                dbObject.ObjectName = obj.ObjectName;
                dbObject.DisplayName = obj.DisplayName;
                dbObject.ApiName = obj.ApiName;
                dbObject.AppID = obj.AppID;
                dbObject.UserID = obj.UserID;
                dbObject.Descriptions = obj.Descriptions;
                dbObject.IsActive = obj.IsActive;
            }

            Save();
        }

        /// <summary>
        /// Get all active and sharable app to be a datasource
        /// </summary>
        public IEnumerable<TB_App> GetApps(Guid userID)
        {
            IAppRepository appRepository = this.GetAlternativeRepository<IAppRepository>();
            return appRepository.GetAllApps(false, userID, true);
        }
    }
}
