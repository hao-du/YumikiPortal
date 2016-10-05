using System.Data.Entity;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration
{
    /// <summary>
    /// Entity Framework Context for all CRUD task with "DB_Administration" database.
    /// </summary>
    public partial class AdministrationModel : DbContext
    {
        public AdministrationModel()
            : base("name=AdministrationModel")
        {
        }

        public virtual DbSet<TB_User> TB_User { get; set; }
        public virtual DbSet<TB_Group> TB_Group { get; set; }
    }

    /// <summary>
    /// Administration Database base class which defined all common methods and properties.
    /// </summary>
    public abstract class AdministrationRepository
    {
        private AdministrationModel context;
        protected AdministrationModel Context
        {
            get
            {
                if (context == null)
                    context = new AdministrationModel();
                return context;
            }
        }
    }
}