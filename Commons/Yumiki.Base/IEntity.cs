﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Base
{
    public interface IEntity
    {
        string Descriptions { get; set; }
        bool IsActive { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? LastUpdateDate { get; set; }
    }
}
