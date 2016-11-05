using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;

namespace Yumiki.Data.Master
{
    /// <summary>
    /// Administration Database base class which defined all common methods and properties.
    /// </summary>
    public class MasterRepository : BaseRepository<MasterModel>
    {
        protected MasterModel Context
        {
            get
            {
                if (context == null)
                    context = new MasterModel();
                return context;
            }
        }
    }
}
