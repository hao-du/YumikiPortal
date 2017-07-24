using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Data.WellCovered.Repositories;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Business.WellCovered.Services
{
    public class LiveService : BaseService<ILiveRepository>, ILiveService
    {
        /// <summary>
        /// Publish App and its related objects and fields to Live.
        /// </summary>
        /// <param name="objectID">Id of App need to be gone live.</param>
        public void PublishApp(string appID)
        {
            Guid convertedID = Guid.Empty;
            Guid.TryParse(appID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "App ID must be GUID type.", Logger);
            }

            if (!Repository.IsAppValidToPublish(convertedID))
            {
                throw new YumikiException(ExceptionCode.E_INVALID_VALUE, "App can only be published if it has objects and fields.", Logger);
            }

            Repository.PublishApp(convertedID);
        }

        /// <summary>
        /// Publish object and its related fields to Live.
        /// </summary>
        /// <param name="objectID">Id of Object need to be gone live.</param>
        public void PublishObject(string objectID)
        {
            Guid convertedID = Guid.Empty;
            Guid.TryParse(objectID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Object ID must be GUID type.", Logger);
            }

            if (!Repository.IsObjectValidToPublish(convertedID))
            {
                throw new YumikiException(ExceptionCode.E_INVALID_VALUE, "Object can only be published if it has fields.", Logger);
            }

            Repository.PublishObject(convertedID);
        }
    }
}
