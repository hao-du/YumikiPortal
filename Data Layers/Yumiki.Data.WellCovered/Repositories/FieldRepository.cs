using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Repositories
{
    public class FieldRepository : WellCoveredRepository, IFieldRepository
    {
        /// <summary>
        /// Get all active Fields from specific Object in Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Fields or active Fields.</param>
        /// <returns>List of all active Fields.</returns>
        public IEnumerable<TB_Field> GetAllFields(bool showInactive, Guid objectID)
        {
            IEnumerable<TB_Field> fields = Context.TB_Field.Where(c => c.IsActive == !showInactive && c.ObjectID == objectID).OrderBy(c=>c.FieldOrder).ToList();

            return fields;
        }

        /// <summary>
        /// Return specific Field by id.
        /// </summary>
        /// <param name="id">TB_Field Guid ID.</param>
        /// <returns>Result with TB_Field type.</returns>
        public TB_Field GetFieldByID(Guid id)
        {
            return Context.TB_Field.Where(c => c.ID == id).SingleOrDefault();
        }

        /// <summary>
        /// Check if ApiName already existed within Object in DB.
        /// </summary>
        /// <param name="field">Field's ApiName to check.</param>
        /// <returns></returns>
        public bool Any(TB_Field field)
        {
            return Context.TB_Field.Any(c => c.ApiName == field.ApiName && c.ID != field.ID && c.ObjectID == field.ObjectID);
        }

        /// <summary>
        /// Save Field to DB
        /// </summary>
        /// <param name="field">If Field id is empty, then this is new Field. Otherwise, this needs to be updated</param>
        public void Save(TB_Field field)
        {
            if (field.ID == Guid.Empty)
            {
                field.IsSystemField = false;

                Context.TB_Field.Add(field);
            }
            else
            {
                TB_Field dbField = Context.TB_Field.Single(c => c.ID == field.ID);
                dbField.FieldName = field.FieldName;
                dbField.DisplayName = field.DisplayName;
                dbField.ApiName = field.ApiName;
                dbField.ObjectID = field.ObjectID;

                dbField.IsDisplayable = field.IsDisplayable;
                dbField.FieldOrder = field.FieldOrder;
                dbField.DataSortByOrder = field.DataSortByOrder;
                dbField.CanIndex = field.CanIndex;

                dbField.FieldType = field.FieldType;
                dbField.IsRequired = field.IsRequired;
                dbField.FieldLength = field.FieldLength;
                dbField.Datasource = field.Datasource;

                dbField.UserID = field.UserID;
                dbField.Descriptions = field.Descriptions;
                dbField.IsActive = field.IsActive;
            }

            Save();
        }
    }
}
