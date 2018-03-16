using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Dictionaries;

namespace Yumiki.Commons.Settings
{
    /// <summary>
    /// This is used for Background service such as Windows Services.
    /// </summary>
    public class AppSettings
    {
        public static string FilePath
        {
            get {
                return System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
            }
        }

        public static double Interval
        {
            get
            {
                string stringValue = System.Configuration.ConfigurationManager.AppSettings[SettingNames.Interval];
                if (!string.IsNullOrWhiteSpace(stringValue))
                {
                    double intValue;
                    if (double.TryParse(stringValue, out intValue)) {
                        return intValue;
                    }
                }
                return 60000d; //1 minute
            }
        }

        public static string ConnectionString
        {
            get
            {
                string connectionString = System.Configuration.ConfigurationManager.AppSettings[SettingNames.ConnectionString];
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    return connectionString;
                }
                return null;
            }
        }

        /// <summary>
        /// For only SyncService to get all store procedures need to be executed.
        /// </summary>
        public static string ExecutionListPath
        {
            get
            {
                string executionListPath = System.Configuration.ConfigurationManager.AppSettings[SettingNames.ExecutionListPath];
                if (!string.IsNullOrWhiteSpace(executionListPath))
                {
                    return executionListPath;
                }
                return null;
            }
        }

        /// <summary>
        /// To get the Default Folder Path in server. E.g. D:\Database\MediaFiles
        /// </summary>
        public static string UploadFolderPath
        {
            get
            {
                string uploadFolderPath = System.Configuration.ConfigurationManager.AppSettings[SettingNames.UploadFolderPath];
                if (!string.IsNullOrWhiteSpace(uploadFolderPath))
                {
                    return uploadFolderPath;
                }
                return null;
            }
        }

        /// <summary>
        /// Backup Database Folder Path. E.g. D:\Database\Backup
        /// </summary>
        public static string BackupFolderPath
        {
            get
            {
                string backupFolderPath = System.Configuration.ConfigurationManager.AppSettings[SettingNames.BackupFolderPath];
                if (!string.IsNullOrWhiteSpace(backupFolderPath))
                {
                    return backupFolderPath;
                }
                return null;
            }
        }
    }
}
