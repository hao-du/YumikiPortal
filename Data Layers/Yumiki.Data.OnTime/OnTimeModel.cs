namespace Yumiki.Data.OnTime
{
    using System.Data.Entity;

    public partial class OnTimeModel : DbContext
    {
        public OnTimeModel()
            : base("name=OnTimeModel")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }
    }
}
