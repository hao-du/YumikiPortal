namespace Yumiki.Data.OnTime
{
    using System.Data.Entity;
    using Yumiki.Entity.OnTime;

    public partial class OnTimeModel : DbContext
    {
        public OnTimeModel()
            : base("name=OnTimeModel")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<TB_History> TB_History { get; set; }
        public virtual DbSet<TB_Phase> TB_Phase { get; set; }
        public virtual DbSet<TB_PhaseAssignment> TB_PhaseAssignment { get; set; }
        public virtual DbSet<TB_Project> TB_Project { get; set; }
        public virtual DbSet<TB_ProjectAssignment> TB_ProjectAssignment { get; set; }
        public virtual DbSet<TB_Task> TB_Task { get; set; }
        public virtual DbSet<TB_User> TB_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_Phase>()
                .HasMany(e => e.Histories)
                .WithOptional(e => e.Phase)
                .HasForeignKey(e => e.PhaseID);

            modelBuilder.Entity<TB_Phase>()
                .HasMany(e => e.PhaseAssignments)
                .WithOptional(e => e.Phase)
                .HasForeignKey(e => e.PhaseID);

            modelBuilder.Entity<TB_Phase>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Phase)
                .HasForeignKey(e => e.PhaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Phase>()
                .Ignore(e => e.IsAssigned);

            modelBuilder.Entity<TB_Project>()
                .HasMany(e => e.ProjectAssignments)
                .WithOptional(e => e.Project)
                .HasForeignKey(e => e.ProjectID);

            modelBuilder.Entity<TB_Project>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.ProjectID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Project>()
                .Ignore(e => e.IsAssigned);

            modelBuilder.Entity<TB_Task>()
                .HasMany(e => e.Histories)
                .WithOptional(e => e.Task)
                .HasForeignKey(e => e.TaskID);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.UserLoginName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .Property(e => e.TimeZone)
                .IsUnicode(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Phases)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.PhaseAssignments)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.ProjectAssignments)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_User>()
                .HasMany(e => e.AssignedTasks)
                .WithRequired(e => e.AssignedUser)
                .HasForeignKey(e => e.AssignedUserID);
        }
    }
}
