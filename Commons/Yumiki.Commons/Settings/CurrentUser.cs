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
    /// <summary>
    /// Current Session of Logged on user.
    /// </summary>
    public static class CurrentUser
    {
        private static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static bool IsAuthenticated
        {
            get
            {
                if (Session == null || Session[HttpConstants.Session.UserID] == null)
                {
                    return false;
                }
                return true;
            }
        }

        public static Guid UserID
        {
            get
            {
                if (Session == null || Session[HttpConstants.Session.UserID] == null)
                {
                    return Guid.Empty;
                }
                return Guid.Parse(Session[HttpConstants.Session.UserID].ToString());
            }
            set
            {
                Session[HttpConstants.Session.UserID] = value;
            }
        }

        public static string UserLoginName
        {
            get
            {
                if (Session == null && Session[HttpConstants.Session.UserLoginName] == null)
                {
                    return null;
                }
                return Session[HttpConstants.Session.UserLoginName].ToString();
            }
            set
            {
                Session[HttpConstants.Session.UserLoginName] = value;
            }
        }

        public static string UserFullName
        {
            get
            {
                if (Session == null && Session[HttpConstants.Session.UserLoginName] == null)
                {
                    return null;
                }
                return Session[HttpConstants.Session.UserLoginName].ToString();
            }
            set
            {
                Session[HttpConstants.Session.UserLoginName] = value;
            }
        }

        public static DateTime LastLoginTime
        {
            get
            {
                if (Session == null && Session[HttpConstants.Session.LastLoginTime] == null)
                {
                    return DateTime.MinValue;
                }
                return DateTime.Parse(Session[HttpConstants.Session.LastLoginTime].ToString());
            }
            set
            {
                Session[HttpConstants.Session.LastLoginTime] = value;
            }
        }

        public static SystemTimeZone TimeZone
        {
            get
            {
                if (Session == null && Session[HttpConstants.Session.TimeZone] == null)
                {
                    return DateTimeExtension.GetDefaultTimeZone();
                }
                return (SystemTimeZone)Session[HttpConstants.Session.TimeZone];
            }
            set
            {
                Session[HttpConstants.Session.TimeZone] = value;
            }
        }
    }
}
