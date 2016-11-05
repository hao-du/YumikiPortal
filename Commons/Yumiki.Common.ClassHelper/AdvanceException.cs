using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Common.Helper
{
    public enum ExceptionCode
    {
        E_EMPTY_VALUE,
        E_WRONG_TYPE,
        E_HTTP_ERROR,
        E_DUPLICATED,
        E_SECURITY_ERROR
    }

    public class AdvanceException : Exception
    {
        public ExceptionCode ExCode { get; set; }

        /// <summary>
        /// Used to pass over the DLLs to track exception value.
        /// </summary>
        public object ReferenceObject { get; set; }

        public AdvanceException(ExceptionCode exCode, string errorMessage, Exception innerException) : base(errorMessage, innerException)
        {
            this.ExCode = exCode;
        }

        /// <summary>
        /// Constructor of Advance Exception class.
        /// </summary>
        /// <param name="exCode">ExceptionCode enum type</param>
        /// <param name="referenceObject">Used to pass over the DLLs to track exception value.</param>
        /// <param name="errorMessage">Error message from Exception base class</param>
        /// <param name="innerException">Inner exception from Exception base class</param>
        public AdvanceException(ExceptionCode exCode, object referenceObject ,string errorMessage, Exception innerException) : base(errorMessage, innerException)
        {
            this.ExCode = exCode;
            this.ReferenceObject = referenceObject;
        }
    }
}
