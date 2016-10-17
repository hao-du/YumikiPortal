using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;

namespace Yumiki.Data.Administration
{
    /// <summary>
    /// Administration Database base class which defined all common methods and properties.
    /// </summary>
    public class AdministrationRepository : BaseRepository<AdministrationModel>
    {
        protected AdministrationModel Context
        {
            get
            {
                if (context == null)
                    context = new AdministrationModel();
                return context;
            }
        }
    }
}
