using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Exceptions
{
    public enum ExceptionCode
    {
        E_EMPTY_VALUE,
        E_WRONG_TYPE,
        E_HTTP_ERROR,
        E_DUPLICATED,
        E_INVALID_LENGTH,
        E_SECURITY_ERROR,
        E_NO_SESSION,
        E_WRONG_VALUE,
        E_INVALID_VALUE,
    }

    public class YumikiException : Exception
    {
        public ExceptionCode ExCode { get; set; }

        /// <summary>
        /// Used to pass over the DLLs to track exception value.
        /// </summary>
        public object ReferenceObject { get; set; }

        /// <summary>
        /// Constructor of Advance Exception class.
        /// </summary>
        /// <param name="exCode">ExceptionCode enum type</param>
        /// <param name="errorMessage">Error message from Exception base class</param>
        /// <param name="logger">Log exception to log file.</param>
        public YumikiException(ExceptionCode exCode, string errorMessage, Logging.Logger logger = null) : base(errorMessage)
        {
            this.ExCode = exCode;
            AppendLog(logger);
        }

        /// <summary>
        /// Constructor of Advance Exception class.
        /// </summary>
        /// <param name="exCode">ExceptionCode enum type</param>
        /// <param name="errorMessage">Error message from Exception base class</param>
        /// <param name="innerException">Inner exception from Exception base class</param>
        /// <param name="logger">Log exception to log file.</param>
        public YumikiException(ExceptionCode exCode, string errorMessage, Exception innerException, Logging.Logger logger = null) : base(errorMessage, innerException)
        {
            this.ExCode = exCode;
            AppendLog(logger);
        }

        /// <summary>
        /// Constructor of Advance Exception class.
        /// </summary>
        /// <param name="exCode">ExceptionCode enum type</param>
        /// <param name="referenceObject">Used to pass over the DLLs to track exception value.</param>
        /// <param name="errorMessage">Error message from Exception base class</param>
        /// <param name="innerException">Inner exception from Exception base class</param>
        /// <param name="logger">Log exception to log file.</param>
        public YumikiException(ExceptionCode exCode, object referenceObject ,string errorMessage, Exception innerException, Logging.Logger logger = null) : base(errorMessage, innerException)
        {
            this.ExCode = exCode;
            this.ReferenceObject = referenceObject;
            AppendLog(logger);
        }

        /// <summary>
        /// Append exception detail to log file.
        /// </summary>
        /// <param name="logger">Only log if logger is not null.</param>
        private void AppendLog(Logging.Logger logger)
        {
            if(logger != null)
            {
                string message = string.Format("Error Code: {0}", this.ExCode.ToString());
                logger.Error(message, this);
            }
        }
    }
}
