using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Logging;
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
                entity.LastUpdateDate = entity.CreateDate = DateTimeHelper.GetSystemDatetime();
                entity.IsActive = true;
            }
            else
            {
                entity.LastUpdateDate = DateTimeHelper.GetSystemDatetime();
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
    }
}
