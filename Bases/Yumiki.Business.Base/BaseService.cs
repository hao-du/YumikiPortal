using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Business.Base
{
    /// <summary>
    /// Parent class for all Business Logic Layer classes to contains all common properties and methods.
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// Get Dependency Injection Service
        /// </summary>
        protected DependencyHelper Service
        {
            get
            {
                return DependencyHelper.GetService();
            }
        }
    }
}
