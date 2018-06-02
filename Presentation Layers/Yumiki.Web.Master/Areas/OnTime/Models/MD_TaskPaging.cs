using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;

namespace Yumiki.Web.OnTime.Models
{
    public class MD_PagingTask : PagingEntity<MD_Task>
    {
        public MD_PagingTask(GetTaskResponse response)
        {
            this.Records = response.Records.Select(c => new MD_Task(c));
            this.CurrentPage = response.CurrentPage;
            this.ItemsPerPage = response.ItemsPerPage;
            this.TotalItems = response.TotalItems;
            this.DefaultPhaseID = string.Empty;
            this.DefaultProjectID = string.Empty;
        }

        public string DefaultPhaseID { get; set; }
        public string DefaultProjectID { get; set; }
    }
}