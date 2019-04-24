namespace Yumiki.Entity.Shopper
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopperModel : DbContext
    {
        public ShopperModel()
            : base("name=ShopperModel")
        {
        }

        public virtual DbSet<TB_AdditionalFee> TB_AdditionalFee { get; set; }
        public virtual DbSet<TB_FeeType> TB_FeeType { get; set; }
        public virtual DbSet<TB_Invoice> TB_Invoice { get; set; }
        public virtual DbSet<TB_InvoiceDetail> TB_InvoiceDetail { get; set; }
        public virtual DbSet<TB_InvoiceExtraFee> TB_InvoiceExtraFee { get; set; }
        public virtual DbSet<TB_Product> TB_Product { get; set; }
        public virtual DbSet<TB_Receipt> TB_Receipt { get; set; }
        public virtual DbSet<TB_ReceiptDetail> TB_ReceiptDetail { get; set; }
        public virtual DbSet<TB_ReceiptExtraFee> TB_ReceiptExtraFee { get; set; }
        public virtual DbSet<TB_Stock> TB_Stock { get; set; }
        public virtual DbSet<TB_User> TB_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_FeeType>()
                .HasMany(e => e.AdditionalFees)
                .WithRequired(e => e.FeeType)
                .HasForeignKey(e => e.FeeTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_FeeType>()
                .HasMany(e => e.InvoiceExtraFees)
                .WithRequired(e => e.FeeType)
                .HasForeignKey(e => e.FeeTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_FeeType>()
                .HasMany(e => e.ReceiptExtraFees)
                .WithRequired(e => e.FeeType)
                .HasForeignKey(e => e.FeeTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Invoice>()
                .Property(e => e.InvoiceNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Invoice>()
                .Property(e => e.CustomerPhone)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Invoice>()
                .Property(e => e.CustomerEmail)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Invoice>()
                .HasMany(e => e.InvoiceDetails)
                .WithRequired(e => e.Invoice)
                .HasForeignKey(e => e.InvoiceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Invoice>()
                .HasMany(e => e.InvoiceExtraFees)
                .WithRequired(e => e.Invoice)
                .HasForeignKey(e => e.InvoiceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_InvoiceDetail>()
                .HasMany(e => e.Stocks)
                .WithOptional(e => e.InvoiceDetail)
                .HasForeignKey(e => e.InvoiceDetailID)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TB_Product>()
                .HasMany(e => e.InvoiceDetails)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Product>()
                .HasMany(e => e.ReceiptDetails)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Product>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Receipt>()
                .HasMany(e => e.ReceiptDetails)
                .WithRequired(e => e.Receipt)
                .HasForeignKey(e => e.ReceiptID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Receipt>()
                .HasMany(e => e.ReceiptExtraFees)
                .WithRequired(e => e.Receipt)
                .HasForeignKey(e => e.ReceiptID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_ReceiptDetail>()
                .HasMany(e => e.Stocks)
                .WithOptional(e => e.ReceiptDetail)
                .HasForeignKey(e => e.ReceiptDetailID)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.UserLoginName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.TimeZone)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.AdditionalFees)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.FeeTypes)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Invoices)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.InvoiceDetails)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.InvoiceExtraFees)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Receipts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.ReceiptDetails)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.ReceiptExtraFees)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);
        }
    }
}