using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Logging
{
    /// <summary>
    /// NOTE: Remember to change value in 'site.js' if any enum items changed.
    /// </summary>
    public enum LogLevel
    {
        //Turn off logging.
        OFF,
        //Highest level: important stuff down.
        FATAL,
        //For example application crashes / exceptions.
        ERROR,
        //Incorrect behavior but the application can continue
        WARN,
        //Normal behavior like mail sent, user updated profile etc.
        INFO,
        //Executed queries, user authenticated, session expired.
        DEBUG,
        //Begin method X, end method X etc
        TRACE
    }
}
