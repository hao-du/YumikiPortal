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

namespace Yumiki.Web.Ontime.Models
{
    public class MD_TaskPaging : PagingEntity<MD_Task>
    {
        public MD_TaskPaging(GetTaskResponse response)
        {
            this.Records = response.Records.Select(c => new MD_Task(c));
            this.CurrentPage = response.CurrentPage;
            this.ItemsPerPage = response.ItemsPerPage;
            this.TotalItems = response.TotalItems;
        }
    }
}