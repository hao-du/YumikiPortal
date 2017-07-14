using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Unity;
using Yumiki.Entity.Base;

namespace Yumiki.Data.Base
{
    /// <summary>
    /// Base repository class to contains all methods to interract with database throgth Entity Framework
    /// </summary>
    /// <typeparam name="T">Class which is inherited by DBContext</typeparam>
    public class BaseRepository<T> where T : DbContext
    {
        protected T context;

        private Logger logger;
        public Logger Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = new Logger(GetType());
                }
                return logger;
            }
        }

        private Dependency alternativeRepository;
        /// <summary>
        /// Get Dependency Injection Service
        /// </summary>
        protected Dependency AlternativeRepository
        {
            get
            {
                if (alternativeRepository == null)
                {
                    //Get domain name such as "SampleService" in "Yumiki.Business.SampleService" (index = 2)
                    string containerName = this.GetType().FullName.Split('.')[2];
                    alternativeRepository = Dependency.GetService(containerName);
                }
                return alternativeRepository;
            }
        }

        /// <summary>
        /// Create an Alternative Repository instance
        /// </summary>
        /// <typeparam name="E">Interface of Alternative Repository instance and it is inherited by IShareableRepository</typeparam>
        /// <typeparam name="T">EF Context Model</typeparam>
        /// <returns>Instance of Repository Interface </returns>
        protected E GetAlternativeRepository<E>() where E : IShareableRepository<T>
        {
            E instance = AlternativeRepository.GetInstance<E>();

            //Assgin current openning context to avoid creating multiple connections to DB
            instance.AssignContext(this.context);

            return instance;
        }

        /// <summary>
        /// To cross context among repositories to avoid open SQL Connection many times.
        /// </summary>
        /// <param name="context">Entity Framework context</param>
        public void AssignContext(T context)
        {
            this.context = context;
        }

        /// <summary>
        /// Assign the common properties to EF object.
        /// </summary>
        /// <param name="entity">EF object</param>
        /// <param name="isNew">Base on this condition to determine the appropriate values for new/exiting values</param>
        protected void AssginBaseProperties(IEntity entity)
        {
            if (entity.ID == Guid.Empty)
            {
                entity.ID = Guid.NewGuid();
                entity.LastUpdateDate = entity.CreateDate = DateTimeExtension.GetSystemDatetime();
                entity.IsActive = true;
            }
            else
            {
                entity.LastUpdateDate = DateTimeExtension.GetSystemDatetime();
            }
        }

        /// <summary>
        /// Track all changes to assign appropriate values and save.
        /// </summary>
        protected void Save()
        {
            IEnumerable<DbEntityEntry> entries = context.ChangeTracker.Entries();

            foreach (DbEntityEntry entry in entries)
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    IEntity entity = (IEntity)entry.Entity;
                    AssginBaseProperties(entity);
                }
            }

            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw ex;
                }
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
