using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;

namespace Yumiki.Entity.WellCovered
{
    public class WellCoveredRepository : BaseRepository<WellCoveredModel>
    {
        protected WellCoveredModel Context
        {
            get
            {
                if (context == null)
                    context = new WellCoveredModel();
                return context;
            }
        }

        /// <summary>
        ///     Executes the given DDL/DML command against the database. As with any API that
        ///     accepts SQL it is important to parameterize any user input to protect against
        ///     a SQL injection attack. You can include parameter place holders in the SQL query
        ///     string and then supply parameter values as additional arguments. Any parameter
        ///     values you supply will automatically be converted to a DbParameter. context.Database.ExecuteSqlCommand("UPDATE
        ///     dbo.Posts SET Rating = 5 WHERE Author = @p0", userSuppliedAuthor); Alternatively,
        ///     you can also construct a DbParameter and supply it to SqlQuery. This allows you
        ///     to use named parameters in the SQL query string. context.Database.ExecuteSqlCommand("UPDATE
        ///     dbo.Posts SET Rating = 5 WHERE Author = @author", new SqlParameter("@author",
        ///     userSuppliedAuthor));
        /// </summary>
        /// <param name="query">SQL Command Stament</param>
        /// <param name="pamameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        protected int ExecuteCommand(string query, params object[] pamameters)
        {
            return context.Database.ExecuteSqlCommand(query, pamameters);
        }

        /// <summary>
        /// Get dynamic data from sql.
        /// Usage: "SELECT * FROM \"User\" WHERE UserID = @UserID", new Dictionary<string, object> { { "@UserID", 1 } }
        /// </summary>
        /// <param name="sql">Query Stament to get records from DB</param>
        /// <param name="parameters"></param>
        /// <returns>DataTable object contains all results by SQL Stament</returns>
        protected DataTable GetDynamicRecords(string sql, Dictionary<string, object> parameters)
        {
            using (var command = context.Database.Connection.CreateCommand())
            {
                command.CommandText = sql;
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> param in parameters)
                    {
                        DbParameter dbParameter = command.CreateParameter();
                        dbParameter.ParameterName = param.Key;
                        dbParameter.Value = param.Value;
                        command.Parameters.Add(dbParameter);
                    }
                }

                using (var dataReader = command.ExecuteReader())
                {
                    DataTable table = new DataTable();
                    table.Load(dataReader);

                    return table;
                }
            }
        }

        protected DataTable GetDynamicRecords(string sql)
        {
            return GetDynamicRecords(sql, null);
        }
    }
}
