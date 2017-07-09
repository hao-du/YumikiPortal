namespace Yumiki.Entity.WellCovered
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WellCoveredModel : DbContext
    {
        public WellCoveredModel()
            : base("name=WellCoveredModel")
        {
        }

        public virtual DbSet<TB_App> TB_App { get; set; }
        public virtual DbSet<TB_Field> TB_Field { get; set; }
        public virtual DbSet<TB_Object> TB_Object { get; set; }
        public virtual DbSet<TB_User> TB_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_App>()
                .HasMany(e => e.Objects)
                .WithOptional(e => e.App)
                .HasForeignKey(e => e.AppID);

            modelBuilder.Entity<TB_Field>()
                .Property(e => e.ApiName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Object>()
                .Property(e => e.ApiName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Object>()
                .HasMany(e => e.Fields)
                .WithRequired(e => e.Object)
                .HasForeignKey(e => e.ObjectID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.UserLoginName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.TimeZone)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Apps)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Fields)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);
        }
    }
}
