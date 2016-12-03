using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Logging
{
    public class Logger
    {
        NLog.Logger logger;
        public Logger()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public Logger(Type type)
        {
            logger = LogManager.GetLogger(type.FullName, type);
        }

        public void Fatal(string message, Exception ex)
        {
            Append(LogLevel.FATAL, message, ex);
        }

        public void Fatal(string message)
        {
            Fatal(message, null);
        }

        public void Error(string message, Exception ex)
        {
            Append(LogLevel.ERROR, message, ex);
        }

        public void Error(string message)
        {
            Error(message, null);
        }

        public void Warning(string message, Exception ex)
        {
            Append(LogLevel.WARN, message, ex);
        }

        public void Warning(string message)
        {
            Warning(message, null);
        }

        public void Infomation(string message, Exception ex)
        {
            Append(LogLevel.INFO, message, ex);
        }

        public void Infomation(string message)
        {
            Infomation(message, null);
        }

        public void Debug(string message, Exception ex)
        {
            Append(LogLevel.DEBUG, message, ex);
        }

        public void Debug(string message)
        {
            Debug(message, null);
        }

        public void Trace(string message, Exception ex)
        {
            Append(LogLevel.TRACE, message, ex);
        }

        public void Trace(string message)
        {
            Trace(message, null);
        }

        public void Append(LogLevel logLevel, string message, Exception ex)
        {
            switch (logLevel)
            {
                case LogLevel.FATAL:
                    logger.Fatal(ex, message);
                    break;
                case LogLevel.ERROR:
                    logger.Error(ex, message);
                    break;
                case LogLevel.WARN:
                    logger.Warn(ex, message);
                    break;
                case LogLevel.INFO:
                    logger.Info(ex, message);
                    break;
                case LogLevel.DEBUG:
                    logger.Debug(ex, message);
                    break;
                case LogLevel.TRACE:
                    logger.Trace(ex, message);
                    break;
            }
        }

        public void Append(LogLevel logLevel, string message)
        {
            Append(logLevel, message, null);
        }
    }
}
