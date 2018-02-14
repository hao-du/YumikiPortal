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
    public class BaseRepository<T> where T : DbContext, new()
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
            this.context = instance.AssignContext(this.context);

            return instance;
        }

        /// <summary>
        /// To cross context among repositories to avoid open SQL Connection many times.
        /// </summary>
        /// <param name="context">Entity Framework context</param>
        public T AssignContext(T context)
        {
            if (context == null)
            {
                this.context = new T();
            }
            else
            {
                this.context = context;
            }

            return this.context;
        }

        /// <summary>
        /// Assign the common properties to EF object.
        /// </summary>
        /// <param name="entity">EF object</param>
        /// <param name="isNew">Base on this condition to determine the appropriate values for new/exiting values</param>
        protected void AssginBaseProperties(IEntity entity, EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    if (entity.ID == Guid.Empty)
                    {
                        entity.ID = Guid.NewGuid();
                    }
                    entity.LastUpdateDate = entity.CreateDate = DateTimeExtension.GetSystemDatetime();
                    entity.IsActive = true;
                    break;
                case EntityState.Modified:
                    entity.LastUpdateDate = DateTimeExtension.GetSystemDatetime();
                    break;
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
                    AssginBaseProperties(entity, entry.State);
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
