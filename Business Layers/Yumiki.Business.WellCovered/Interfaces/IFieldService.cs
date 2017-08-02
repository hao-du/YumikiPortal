using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Business.WellCovered.Interfaces
{
    public interface IFieldService
    {
        /// <summary>
        /// Get all active Fields from specific Object in Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Fields or active Fields.</param>
        /// <returns>List of all active Fields.</returns>
        IEnumerable<TB_Field> GetAllFields(bool showInactive, string objectID);

        /// <summary>
        /// Return specific Field by id.
        /// </summary>
        /// <param name="id">TB_Field Guid ID.</param>
        /// <returns>Result with TB_Field type.</returns>
        TB_Field GetFieldByID(string id);

        /// <summary>
        /// Save Field to DB
        /// </summary>
        /// <param name="field">If Field id is empty, then this is new Field. Otherwise, this needs to be updated</param>
        void Save(TB_Field field);

        /// <summary>
        /// Get datasource with EN_DataType type.
        /// </summary>
        /// <returns></returns>
        List<ExtendEnum> GetDataTypes();
    }
}
