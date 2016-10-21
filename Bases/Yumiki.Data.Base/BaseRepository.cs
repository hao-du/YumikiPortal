using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
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
                entity.CreateDate = DateTime.Now;
                entity.IsActive = true;
            }
            else
            {
                entity.LastUpdateDate = DateTime.Now;
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

            context.SaveChanges();
        }
    }
}
