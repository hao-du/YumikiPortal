﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Dictionaries
{
    public class HttpConstants
    {
        public class Session
        {
            public const string UserLoginName = "UserLoginName";
            public const string UserFullName = "UserFullName";
            public const string UserID = "UserID";
            public const string LastLoginTime = "LastLoginTime";
            public const string TimeZone = "TimeZone";
        }

        public class QueryStrings
        {
            public const string Path = "path";
        }

        public class RouteNames
        {
            public const string WebFormMasterPrefix = "Pages";
            public const string WebFormAreaPrefix = "Apps";
        }

        public class CommonPages
        {
            public const string AttachmentWidget = "/Apps/WellCovered/Attachment/Widget?recordID=";
        }
    }
}
