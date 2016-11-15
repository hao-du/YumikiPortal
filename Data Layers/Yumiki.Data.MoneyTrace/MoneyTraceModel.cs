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
        }

        public virtual DbSet<TB_Category> TB_Category { get; set; }
        public virtual DbSet<TB_Currency> TB_Currency { get; set; }
        public virtual DbSet<TB_Trace> TB_Trace { get; set; }
        public virtual DbSet<TB_TransactionType> TB_TransactionType { get; set; }
        public virtual DbSet<TB_User> TB_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_Category>()
                .HasMany(e => e.TB_Trace)
                .WithRequired(e => e.TB_Category)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Currency>()
                .HasMany(e => e.TB_Trace)
                .WithRequired(e => e.TB_Currency)
                .HasForeignKey(e => e.CurrencyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_TransactionType>()
                .HasMany(e => e.TB_Category)
                .WithRequired(e => e.TB_TransactionType)
                .HasForeignKey(e => e.TransactionTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.UserLoginName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.TB_Category)
                .WithOptional(e => e.TB_User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.TB_Currency)
                .WithOptional(e => e.TB_User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.TB_Trace)
                .WithRequired(e => e.TB_User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.TB_TransactionType)
                .WithOptional(e => e.TB_User)
                .HasForeignKey(e => e.UserID);
        }
    }
}
