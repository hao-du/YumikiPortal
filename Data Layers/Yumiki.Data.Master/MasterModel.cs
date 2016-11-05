namespace Yumiki.Data.Master
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entity.Master;

    public partial class MasterModel : DbContext
    {
        public MasterModel()
            : base("name=MasterModel")
        {
        }

        public virtual DbSet<TB_User> TB_User { get; set; }
        public virtual DbSet<VW_Privilege> VW_Privilege { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_User>()
                .Property(e => e.UserLoginName)
                .IsUnicode(false);

            modelBuilder.Entity<VW_Privilege>()
                .Property(e => e.PagePath)
                .IsUnicode(false);
        }
    }
}
