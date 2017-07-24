using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Helpers;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Repositories
{
    public class GoLiveRepository : WellCoveredRepository
    {
        public void PublishObject(Guid objectID)
        {
            TB_Object obj = Context.TB_Object.Include(TB_Object.FieldName.Fields).Where(c => c.ID == objectID).SingleOrDefault();

            if(obj != null)
            {
                if (CheckExist(obj.ApiName))
                {
                    CreateTable(obj);
                }
                else
                {
                    AlterTable(obj);
                }
            }
        }

        private bool CheckExist(string tableName)
        {
            return Context.Database.SqlQuery<int?>(@"SELECT 1 FROM sys.tables WHERE name = @tableName", new SqlParameter("@tableName", tableName)).SingleOrDefault() != null;
        }

        private void CreateTable(TB_Object obj)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("CREATE TABLE {0} (", obj.ApiName);
            foreach(TB_Field field in obj.Fields)
            {
                string fieldName = field.ApiName;
                string fieldType = EnumHelper.GetMappingValue(field.FieldType);
                string fieldLength = field.FieldLength.HasValue ? string.Format("({0})", field.FieldLength.Value.ToString()) : string.Empty;

                sqlBuilder.AppendFormat("{0} {1}{2} NULL, ", fieldName, fieldType, fieldLength);
            }

            sqlBuilder.Append("ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, ");
            sqlBuilder.Append("Descriptions VARCHAR(255) NULL, ");
            sqlBuilder.Append("IsActive BIT NOT NULL, ");
            sqlBuilder.Append("CreateDate DATETIME NOT NULL, ");
            sqlBuilder.Append("LastUpdateDate DATETIME NULL");

            sqlBuilder.Append(")");
        }

        private void AlterTable(TB_Object obj)
        {
            throw new NotImplementedException();
        }
    }
}
