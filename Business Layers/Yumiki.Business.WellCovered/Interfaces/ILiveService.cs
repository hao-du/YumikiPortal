using System;
using System.Collections.Generic;
using System.Data;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Business.WellCovered.Interfaces
{
    public interface ILiveService
    {
        /// <summary>
        /// Publish App and its related objects and fields to Live.
        /// </summary>
        /// <param name="objectID">Id of App need to be gone live.</param>
        void PublishApp(string appID);

        /// <summary>
        /// Publish object and its related fields to Live.
        /// </summary>
        /// <param name="objectID">Id of Object need to be gone live.</param>
        void PublishObject(string objectID);

        /// <summary>
        /// Fetch all data from Object
        /// </summary>
        /// <param name="objectID">Object ID need to fetch data</param>
        MD_Live FetchObjectData(string objectID, bool isActive);
    }
}
