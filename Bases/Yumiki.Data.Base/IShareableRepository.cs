using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Unity;
using Yumiki.Entity.Base;

namespace Yumiki.Data.Base
{
    /// <summary>
    /// Base repository class to contains all methods to interract with database throgth Entity Framework
    /// </summary>
    /// <typeparam name="T">Class which is inherited by DBContext</typeparam>
    public interface IShareableRepository<T> where T : DbContext
    {
        /// <summary>
        /// To cross context among repositories to avoid open SQL Connection many times.
        /// </summary>
        /// <param name="context">Entity Framework context</param>
        T AssignContext(T context);
    }
}
