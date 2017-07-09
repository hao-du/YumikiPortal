using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceProcess;
using System.Timers;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Settings;
using Yumiki.Service.Base;

namespace Yumiki.Service.QueueService
{
    public partial class QueueService : BaseService<IQueueService>
    {
        private Timer _timer;
        private bool _finishCurrentTask = false;

        public QueueService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Infomation("Start QueueService Service.");
            Logger.Infomation(string.Format("App.Config located at : {0}.", AppSettings.FilePath));

            _finishCurrentTask = true;

            _timer = new Timer();
            _timer.Interval = AppSettings.Interval;
            _timer.Elapsed += timer_Elapsed;
            _timer.Enabled = true;
        }

        protected void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_finishCurrentTask)
            {
                Logger.Infomation(string.Format("Process Queue start at {0}.", e.SignalTime.ToString(Formats.DateTime.ShortDateTime)));

                _finishCurrentTask = false;
                OnProcess();
                _finishCurrentTask = true;

                Logger.Infomation(string.Format("Process Queue end at {0}.", DateTimeExtension.GetLocalSystemDatetime().ToString(Formats.DateTime.ShortDateTime)));
            }
        }

        private void OnProcess()
        {
            try
            {
                BusinessService.ExecuteQueue();
            }
            catch(Exception ex)
            {
                Logger.Error("Error duing process...", ex);
            }
        }

        protected override void OnStop()
        {
            Logger.Infomation("Stop QueueService Service.");
            _timer.Enabled = false;
        }
    }
}
