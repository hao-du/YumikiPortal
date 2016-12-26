namespace Yumiki.Data.MoneyTrace
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entity.MoneyTrace;

    public partial class MoneyTraceModel : DbContext
    {
        public MoneyTraceModel()
            : base("name=MoneyTraceModel")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<TB_Currency> TB_Currency { get; set; }
        public virtual DbSet<TB_Tag> TB_Tag { get; set; }
        public virtual DbSet<TB_Trace> TB_Trace { get; set; }
        public virtual DbSet<TB_User> TB_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_Currency>()
                .HasMany(e => e.TB_Trace)
                .WithRequired(e => e.TB_Currency)
                .HasForeignKey(e => e.CurrencyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.UserLoginName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.TB_Currency)
                .WithRequired(e => e.TB_User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.TB_Tag)
                .WithOptional(e => e.TB_User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.TB_Trace)
                .WithRequired(e => e.TB_User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);
        }
    }
}
