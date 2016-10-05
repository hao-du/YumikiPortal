using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;

namespace Yumiki.Web.Base
{
    /// <summary>
    /// Parent class for all ASP.NET Webform pages.
    /// </summary>
    public class BasePage : System.Web.UI.Page
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
