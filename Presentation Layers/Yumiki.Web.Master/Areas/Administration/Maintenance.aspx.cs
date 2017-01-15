﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Administration;
using Yumiki.Web.Base;

namespace Yumiki.Web.Administration
{
    public partial class Maintenance : BasePage<IQueueService>
    {
        protected void btnShutdownServer_Click(object sender, EventArgs e)
        {
            TB_Queue queue = new TB_Queue();
            queue.QueueType = EN_QueueType.E_SHUTDOWN_SERVER;
            queue.UserID = HttpSession.UserID;

            BusinessService.SaveQueue(queue);

            SendInformation("Shutdown job has been queued.");
        }
    }
}