using Microsoft.SqlServer.Management.Smo;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class QueueService : BaseService<IQueueRepository>, IQueueService
    {
        /// <summary>
        /// Add or update queue.
        /// </summary>
        /// <param name="queue">Queue need to be saved.</param>
        public void SaveQueue(TB_Queue queue)
        {
            Repository.SaveQueue(queue);
        }

        /// <summary>
        /// Get the first record from queue.
        /// </summary>
        public void ExecuteQueue()
        {
            //Check whether queue has been set the status or not.
            bool flagSet = false;
            TB_Queue queue = Repository.GetQueue();

            if (queue != null)
            {
                Logger.Infomation(string.Format("Start executing Queue: ID: {0} - Type: {1} - Value1: {2} - Value2: {3} - Value3: {4}", queue.ID, queue.QueueType.ToString(), queue.Value1, queue.Value2, queue.Value3));

                //Set Active Flag to false before shutdown server.
                Repository.SetQueueFlag(queue.ID, false);
                flagSet = true;

                switch (queue.QueueType)
                {
                    case EN_QueueType.E_SHUTDOWN_SERVER:
                        ShutdownServer();
                        break;
                    case EN_QueueType.E_BACKUP_SERVER:
                        //Value1 contains database names with ','
                        BackupDatabses(queue.Value1);
                        //Values2 contains media file zip name.
                        BackupMediaFiles(queue.Value2);
                        break;
                }

                if (flagSet)
                {
                    Repository.SetQueueFlag(queue.ID, false);
                }

                Logger.Infomation(string.Format("End executing Queue: ID: {0} - Type: {1} - Value1: {2} - Value2: {3} - Value3: {4}", queue.ID, queue.QueueType.ToString(), queue.Value1, queue.Value2, queue.Value3));
            }
            else
            {
                Logger.Infomation(string.Format("No Queue to be executed at {0}", DateTimeExtension.GetLocalSystemDatetime()));
            }
        }

        private void ShutdownServer()
        {
            ProcessStartInfo psi = new ProcessStartInfo("shutdown", "/s /f /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        /// <summary>
        /// Backup Databases
        /// </summary>
        /// <param name="databaseNames">List of DatabaseName with ',' separator.</param>
        private void BackupDatabses(string databaseNames)
        {
            if (string.IsNullOrWhiteSpace(databaseNames))
            {
                Logger.Infomation($"No Database to back up.");
                return;
            }

            Logger.Infomation($"Start to back up databases:{databaseNames}.");

            string backupPath = Path.Combine(AppSettings.BackupFolderPath, DateTime.Now.ToString(Formats.DateTime.yyyyMMdd));
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }

            foreach (string database in databaseNames.Split(','))
            {
                try
                {
                    Logger.Infomation($"Backing up database:{database}...");

                    string backupFullFilePath = Path.Combine(backupPath, $"{database}.bak");

                    //Open local sql server
                    Server server = new Server();
                    Backup backup = new Backup();
                    backup.Devices.AddDevice(backupFullFilePath, DeviceType.File);
                    backup.Database = database;
                    backup.Action = BackupActionType.Database;
                    backup.Initialize = true;
                    backup.SqlBackup(server);

                    if (!File.Exists(backupFullFilePath))
                    {
                        throw new YumikiException(ExceptionCode.E_ERROR, $"Cannot back up {database}", Logger);
                    }

                    Logger.Infomation($"Database {database} was backed up successfully at {backupFullFilePath}.");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error during backing up database:{database}.", ex);
                }
            }
        }

        /// <summary>
        /// Zip the media file folder and move it to backup folder
        /// </summary>
        /// <param name="zipFileName">Contains media file zip name.</param>
        private void BackupMediaFiles(string zipFileName)
        {
            if (string.IsNullOrWhiteSpace(zipFileName))
            {
                Logger.Infomation($"No given zip name to back up.");
                return;
            }

            try
            {
                ZipFile.CreateFromDirectory(AppSettings.UploadFolderPath
                                        , Path.Combine(AppSettings.BackupFolderPath, zipFileName)
                                        , CompressionLevel.Optimal
                                        , true);
            }
            catch (Exception ex)
            {
                Logger.Error("Error during backing up media file's folder.", ex);
            }
        }
    }
}
