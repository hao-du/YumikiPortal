namespace Yumiki.Data.Administration
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AdministrationModel : DbContext
    {
        public AdministrationModel()
            : base("name=AdministrationModel")
        {
        }

        public virtual DbSet<TB_User> TB_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_User>()
                .Property(e => e.UserLoginName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.CurrentPassword)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.IsActive)
                .IsFixedLength();
        }
    }
}
