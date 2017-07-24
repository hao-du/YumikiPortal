using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Logging;

namespace Yumiki.Commons.Entities
{
    public class Message
    {
        public string Msg { get; private set; }
        public string Details { get; private set; }
        public string LogType { get; private set; }

        public Message(string message)
        {
            InitMessage(message, string.Empty, LogLevel.INFO);
        }

        public Message(Exception ex)
        {
            string message = ex.Message;
            string details = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
            InitMessage(message, details, LogLevel.ERROR);
        }

        public Message(string message, LogLevel logType)
        {
            InitMessage(message, string.Empty, logType);
        }

        public Message(string message, string details, LogLevel logType)
        {
            InitMessage(message, details, logType);
        }

        private void InitMessage(string message, string details, LogLevel logType)
        {
            Msg = message;
            Details = details;
            LogType = logType.ToString();
        }
    }
}
