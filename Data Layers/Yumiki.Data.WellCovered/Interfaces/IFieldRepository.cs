using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Interfaces
{
    public interface IFieldRepository : IShareableRepository<WellCoveredModel>
    {
        /// <summary>
        /// Get all active Fields from specific Object in Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Fields or active Fields.</param>
        /// <returns>List of all active Fields.</returns>
        IEnumerable<TB_Field> GetAllFields(bool showInactive, Guid objectID);

        /// <summary>
        /// Return specific Field by id.
        /// </summary>
        /// <param name="id">TB_Field Guid ID.</param>
        /// <returns>Result with TB_Field type.</returns>
        TB_Field GetFieldByID(Guid id);

        /// <summary>
        /// Check if ApiName already existed within Object in DB.
        /// </summary>
        /// <param name="field">Field's ApiName to check.</param>
        /// <returns></returns>
        bool Any(TB_Field field);

        /// <summary>
        /// Save Field to DB
        /// </summary>
        /// <param name="field">If Field id is empty, then this is new Field. Otherwise, this needs to be updated</param>
        void Save(TB_Field field);
    }
}
