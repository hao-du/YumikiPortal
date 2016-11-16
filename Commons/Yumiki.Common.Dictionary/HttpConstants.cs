using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Common.Dictionary
{
    public class HttpConstants
    {
        public class Session
        {
            public const string UserLoginName = "UserLoginName";
            public const string UserID = "UserID";
            public const string LastLoginTime = "LastLoginTime";
        }

        public class QueryStrings
        {
            public const string Path = "path";
        }

        public class Pages
        {
            public const string Login = "/Security/Login";
            public const string WebFormMasterPrefix = "Pages";
            public const string WebFormAreaPrefix = "Apps";
        }
    }
}
