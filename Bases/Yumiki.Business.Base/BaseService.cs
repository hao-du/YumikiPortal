using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Common.Helper;

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
                //Get domain name such as "SampleService" in "Yumiki.Business.SampleService" (index = 2)
                string containerName = this.GetType().FullName.Split('.')[2];
                return DependencyHelper.GetService(containerName);
            }
        }
    }
}
