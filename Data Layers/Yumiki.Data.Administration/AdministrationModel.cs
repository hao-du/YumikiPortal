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
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<TB_Group> TB_Group { get; set; }
        public virtual DbSet<TB_Privilege> TB_Privilege { get; set; }
        public virtual DbSet<TB_User> TB_User { get; set; }
        public virtual DbSet<TB_UserAddress> TB_UserAddress { get; set; }
        public virtual DbSet<TB_ContactType> TB_ContactType { get; set; }
        public virtual DbSet<TB_PasswordHistory> TB_PasswordHistory { get; set; }
        public virtual DbSet<TB_Queue> TB_Queue { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_ContactType>()
                .HasMany(e => e.UserAddresses)
                .WithRequired(e => e.ContactType)
                .HasForeignKey(e => e.UserContactTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Group>()
                .HasMany(e => e.Privileges)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("TB_GroupPrivilege").MapLeftKey("GroupID").MapRightKey("PrivilegeID"));

            modelBuilder.Entity<TB_Group>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("TB_UserGroup").MapLeftKey("GroupID").MapRightKey("UserID"));

            modelBuilder.Entity<TB_PasswordHistory>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Privilege>()
                .Property(e => e.PagePath)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Privilege>()
                .HasMany(e => e.ChildPrivileges)
                .WithOptional(e => e.ParentPrivilege)
                .HasForeignKey(e => e.ParentPrivilegeID);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.UserLoginName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.CurrentPassword)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.PasswordHistories)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.UserAddresses)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);
        }
    }
}