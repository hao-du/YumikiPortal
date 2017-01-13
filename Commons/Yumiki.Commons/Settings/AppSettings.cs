using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Dictionaries;

namespace Yumiki.Commons.Settings
{
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
    }
}
