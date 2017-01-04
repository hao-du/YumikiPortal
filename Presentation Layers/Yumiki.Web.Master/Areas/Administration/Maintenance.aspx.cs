using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Web.Base;

namespace Yumiki.Web.Administration
{
    public partial class Maintenance : BasePage<IUserService>
    {
        protected void btnShutdownServer_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo("shutdown", "/s /f /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }
    }
}