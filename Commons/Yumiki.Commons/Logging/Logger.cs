using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Logging
{
    public class Logger
    {
        ILog logger;
        public Logger()
        {
            logger = LogManager.GetLogger(typeof(Logger));
        }

        public Logger(Type type)
        {
            logger = LogManager.GetLogger(type);
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

        public void Append(LogLevel logLevel, string message, Exception ex)
        {
            if (ex == null)
            {
                Append(logLevel, message);
            }
            else
            {
                switch (logLevel)
                {
                    case LogLevel.FATAL:
                        logger.Fatal(message, ex);
                        break;
                    case LogLevel.ERROR:
                        logger.Error(message, ex);
                        break;
                    case LogLevel.WARN:
                        logger.Warn(message, ex);
                        break;
                    case LogLevel.INFO:
                        logger.Info(message, ex);
                        break;
                    case LogLevel.DEBUG:
                        logger.Debug(message, ex);
                        break;
                }
            }
        }

        public void Append(LogLevel logLevel, string message)
        {
            switch (logLevel)
            {
                case LogLevel.FATAL:
                    logger.Fatal(message);
                    break;
                case LogLevel.ERROR:
                    logger.Error(message);
                    break;
                case LogLevel.WARN:
                    logger.Warn(message);
                    break;
                case LogLevel.INFO:
                    logger.Info(message);
                    break;
                case LogLevel.DEBUG:
                    logger.Debug(message);
                    break;
            }
        }
    }
}
