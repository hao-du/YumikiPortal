using Yumiki.Data.Base;

namespace Yumiki.Data.OnTime
{
    public class OnTimeRepository : BaseRepository<OnTimeModel>
    {
        protected OnTimeModel Context
        {
            get
            {
                if (context == null)
                    context = new OnTimeModel();
                return context;
            }
        }
    }
}
