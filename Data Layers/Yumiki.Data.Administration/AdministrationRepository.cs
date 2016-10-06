using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Data.Administration
{
    /// <summary>
    /// Administration Database base class which defined all common methods and properties.
    /// </summary>
    public abstract class AdministrationRepository
    {
        private AdministrationModel context;
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
