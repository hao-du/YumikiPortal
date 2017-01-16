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

        public virtual DbSet<TB_Bank> TB_Bank { get; set; }
        public virtual DbSet<TB_Currency> TB_Currency { get; set; }
        public virtual DbSet<TB_Tag> TB_Tag { get; set; }
        public virtual DbSet<TB_Trace> TB_Trace { get; set; }
        public virtual DbSet<TB_User> TB_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_Bank>()
                .HasMany(e => e.Traces)
                .WithOptional(e => e.Bank)
                .HasForeignKey(e => e.BankID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Currency>()
                .HasMany(e => e.Traces)
                .WithRequired(e => e.Currency)
                .HasForeignKey(e => e.CurrencyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.UserLoginName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Currency)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Tags)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Traces)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.TransferredTraces)
                .WithOptional(e => e.TransferredUser)
                .HasForeignKey(e => e.TransferredUserID)
                .WillCascadeOnDelete(false);

            //Ignore fields which not mapped to Database
            modelBuilder.Entity<TB_Trace>()
                .Ignore(e => e.ExchangeAmount)
                .Ignore(e => e.ExchangeCurrencyID);
        }
    }
}
