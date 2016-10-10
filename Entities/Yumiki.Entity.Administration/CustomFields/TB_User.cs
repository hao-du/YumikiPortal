﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Common.Dictionary;

namespace Yumiki.Entity.Administration
{
    public partial class TB_User
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeFormat.ShortDateTime) : CreateDate.ToString(DateTimeFormat.ShortDateTime);
            }
        }

        public string FullName
        {
            get
            {
                string lastName = string.IsNullOrEmpty(LastName) ? string.Empty : LastName;

                return string.IsNullOrEmpty(FirstName) ? lastName : FirstName + " " + LastName;
            }
        }
    }
}