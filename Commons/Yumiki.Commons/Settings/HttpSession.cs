using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Helpers;

namespace Yumiki.Commons.Settings
{
    public class HttpSession
    {
        private HttpSessionStateBase session;

        /// <summary>
        /// ApiController, WebForm page, Master page use HttpSessionState to handle session
        /// </summary>
        /// <param name="session">Session from Web Context.</param>
        public HttpSession(HttpSessionState session)
        {
            this.session = new HttpSessionStateWrapper(session);
        }

        /// <summary>
        /// MVC Controller uses HttpSessionStateBase to manage session.
        /// </summary>
        /// <param name="session">Session from Web Context.</param>
        public HttpSession(HttpSessionStateBase session)
        {
            this.session = session;
        }

        public bool IsAuthenticated
        {
            get
            {
                if(session == null || session[HttpConstants.Session.UserID] == null)
                {
                    return false;
                }
                return true;
            }
        }

        public Guid UserID
        {
            get
            {
                if (session == null || session[HttpConstants.Session.UserID] == null)
                {
                    return Guid.Empty;
                }
                return Guid.Parse(session[HttpConstants.Session.UserID].ToString());
            }
            set
            {
                session[HttpConstants.Session.UserID] = value;
            }
        }

        public string UserLoginName
        {
            get
            {
                if (session == null && session[HttpConstants.Session.UserLoginName] == null)
                {
                    return null;
                }
                return session[HttpConstants.Session.UserLoginName].ToString();
            }
            set
            {
                session[HttpConstants.Session.UserLoginName] = value;
            }
        }

        public DateTime LastLoginTime
        {
            get
            {
                if (session == null && session[HttpConstants.Session.LastLoginTime] == null)
                {
                    return DateTime.MinValue;
                }
                return DateTime.Parse(session[HttpConstants.Session.LastLoginTime].ToString());
            }
            set
            {
                session[HttpConstants.Session.LastLoginTime] = value;
            }
        }

        public SystemTimeZone TimeZone
        {
            get
            {
                if (session == null && session[HttpConstants.Session.TimeZone] == null)
                {
                    return DateTimeHelper.GetDefaultTimeZone();
                }
                return (SystemTimeZone)session[HttpConstants.Session.TimeZone];
            }
            set
            {
                session[HttpConstants.Session.TimeZone] = value;
            }
        }
    }
}
