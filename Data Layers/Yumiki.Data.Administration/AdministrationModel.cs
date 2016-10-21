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

        public virtual DbSet<TB_Group> TB_Group { get; set; }
        public virtual DbSet<TB_Privilege> TB_Privilege { get; set; }
        public virtual DbSet<TB_User> TB_User { get; set; }
        public virtual DbSet<TB_UserAddress> TB_UserAddress { get; set; }
        public virtual DbSet<TB_ContactType> TB_ContactType { get; set; }
        public virtual DbSet<TB_PasswordHistory> TB_PasswordHistory { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //modelBuilder.Entity<TB_Group>()
        //    .HasMany(e => e.TB_Privilege)
        //    .WithMany(e => e.TB_Group)
        //    .Map(m => m.ToTable("TB_GroupPrivilege").MapLeftKey("GroupID").MapRightKey("PrivilegeID"));

        //modelBuilder.Entity<TB_Group>()
        //    .HasMany(e => e.TB_User)
        //    .WithMany(e => e.TB_Group)
        //    .Map(m => m.ToTable("TB_UserGroup").MapLeftKey("GroupID").MapRightKey("UserID"));

        //modelBuilder.Entity<TB_Privilege>()
        //    .Property(e => e.PagePath)
        //    .IsUnicode(false);

        //modelBuilder.Entity<TB_Privilege>()
        //    .HasMany(e => e.TB_Privilege1)
        //    .WithOptional(e => e.TB_Privilege2)
        //    .HasForeignKey(e => e.ParentPrivilegeID);

        //modelBuilder.Entity<TB_User>()
        //    .Property(e => e.UserLoginName)
        //    .IsUnicode(false);

        //modelBuilder.Entity<TB_User>()
        //    .Property(e => e.CurrentPassword)
        //    .IsUnicode(false);

        //modelBuilder.Entity<TB_User>()
        //    .HasMany(e => e.TB_UserAddress)
        //    .WithOptional(e => e.TB_User)
        //    .HasForeignKey(e => e.UserID);
        //}
    }
}