using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Unity;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Settings;
using System.Web;
using System.Web.Routing;
using System.Web.Compilation;

namespace Yumiki.Web.Base
{
    /// <summary>
    /// Used for Custom ashx handler routing.
    /// If route Ihandler directly, we get "Type '' cannot inherit from 'System.Web.Page'"
    /// More info: https://stackoverflow.com/questions/3359816/can-asp-net-routing-be-used-to-create-clean-urls-for-ashx-ihttphander-handl
    /// </summary>
    public class HttpHandlerRoute : IRouteHandler
    {
        private string _virtualPath = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath">Is not physical file path, the path is URL showing in Browser</param>
        public HttpHandlerRoute(string virtualPath)
        {
            _virtualPath = virtualPath;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            IHttpHandler httpHandler = (IHttpHandler)BuildManager.CreateInstanceFromVirtualPath(_virtualPath, typeof(IHttpHandler));
            return httpHandler;
        }
    }
}
