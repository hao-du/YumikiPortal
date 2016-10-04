﻿using System;
using Yumiki.Base;

namespace Yumiki.Entity.Administration
{
    public partial class TB_User : IEntity
    {
        public Guid ID { get; set; }
        public string UserLoginName { get; set; }
        public string CurrentPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
