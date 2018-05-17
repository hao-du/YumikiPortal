using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.OnTime;

namespace Yumiki.Web.Ontime.Models
{
    public class MD_TaskDashBoard
    {
        public IEnumerable<MD_Task> MyTasks { get; set; }
        public IEnumerable<MD_Task> MyCreatedTasks { get; set; }
        public IEnumerable<MD_Task> LatestTasks { get; set; }
        public IEnumerable<MD_Task> UnassignedTasks { get; set; }
    }
}