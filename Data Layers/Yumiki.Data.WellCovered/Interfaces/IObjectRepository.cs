using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Interfaces
{
    public interface IObjectRepository
    {
        /// <summary>
        /// Get all active Objects from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Objects or active Objects.</param>
        /// <returns>List of all active Objects.</returns>
        IEnumerable<TB_Object> GetAllObjects(bool showInactive);

        /// <summary>
        /// Return specific Object by id.
        /// </summary>
        /// <param name="id">TB_Object Guid ID.</param>
        /// <returns>Result with TB_Object type.</returns>
        TB_Object GetObjectByID(Guid id);

        /// <summary>
        /// Check if ApiName already existed in DB.
        /// </summary>
        /// <param name="obj">Object's ApiName to check.</param>
        /// <returns></returns>
        bool Any(TB_Object obj);

        /// <summary>
        /// Save Object to DB
        /// </summary>
        /// <param name="obj">If Object id is empty, then this is new Object. Otherwise, this needs to be updated</param>
        void Save(TB_Object obj);

        /// <summary>
        /// Get all active and sharable app to be a datasource
        /// </summary>
        IEnumerable<TB_App> GetApps(Guid userID);
    }
}
