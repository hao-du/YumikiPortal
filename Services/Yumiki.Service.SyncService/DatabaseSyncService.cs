using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceProcess;
using System.Timers;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Settings;

namespace Yumiki.Service.SyncService
{
    public partial class DatabaseSyncService : ServiceBase
    {
        private Timer _timer;
        private bool _finishCurrentTask = false;
        private Logger _logger;

        public DatabaseSyncService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _logger = new Logger(this.GetType());
            _logger.Infomation("Start DatabaseSyncService Service");
            _logger.Infomation(string.Format("App.Config located at : {0}", AppSettings.FilePath));

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
                _logger.Infomation(string.Format("Process Sync start at {0}", e.SignalTime.ToString(DateTimeHelper.ShortDateTime)));

                _finishCurrentTask = false;
                OnProcess();
                _finishCurrentTask = true;

                _logger.Infomation(string.Format("Process Sync end at {0}", DateTimeHelper.GetLocalSystemDatetime().ToString(DateTimeHelper.ShortDateTime)));
            }
        }

        private void OnProcess()
        {
            if (string.IsNullOrWhiteSpace(AppSettings.ConnectionString))
            {
                _logger.Warning("No connection string setting in app.config. Process ended.");
            }
            else
            {
                SqlConnection sqlConnection = null;
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(AppSettings.ExecutionListPath);
                    _logger.Warning(string.Format("Total Commands need to be executed: {0}.", lines.Length));

                    sqlConnection = new SqlConnection(AppSettings.ConnectionString);
                    sqlConnection.Open();
                    _logger.Warning("Opened SQL Connection.");

                    foreach (string line in lines)
                    {
                        SqlCommand sqlCommand = new SqlCommand(line, sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.ExecuteNonQuery();
                        _logger.Warning(string.Format("Executed Line: '{0}'.", line));
                    }

                    _logger.Warning("Executed all lines.");
                }
                catch (Exception ex)
                {
                    _logger.Error("Error duing process...", ex);
                }
                finally
                {
                    if (sqlConnection != null)
                    {
                        sqlConnection.Close();
                        _logger.Warning("Closed SQL Connection.");
                    }
                }
            }
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
        }
    }
}
