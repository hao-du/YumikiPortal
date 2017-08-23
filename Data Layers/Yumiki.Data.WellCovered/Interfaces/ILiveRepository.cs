using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Helpers;
using Yumiki.Data.Base;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Interfaces
{
    public interface ILiveRepository
    {
        /// <summary>
        /// Perform Full text search for all content.
        /// </summary>
        /// <param name="keywords">Search keywords like google.</param>
        /// <returns>List of search result in TB_Index format.</returns>
        IEnumerable<TB_Index> Search(string keywords);

        /// <summary>
        /// Get fields from ObjectID
        /// </summary>
        IEnumerable<TB_Field> GetFields(Guid objectID);

        /// <summary>
        /// Check if Apps is ready for publish (Has objects and fields)
        /// </summary>
        /// <param name="objectID">Apps need to be checked the validity</param>
        /// <returns>True if Valid</returns>
        bool IsAppValidToPublish(Guid appID);

        /// <summary>
        /// Publish App and its related objects and fields to Live.
        /// </summary>
        /// <param name="objectID">Id of App need to be gone live.</param>
        void PublishApp(Guid appID);

        /// <summary>
        /// Check if Object is ready for publish (Has object and fields)
        /// </summary>
        /// <param name="objectID">Object need to be checked the validity</param>
        /// <returns>True if Valid</returns>
        bool IsObjectValidToPublish(Guid objectID);

        /// <summary>
        /// Publish object and its related fields to Live.
        /// </summary>
        /// <param name="objectID">Id of Object need to be gone live.</param>
        void PublishObject(Guid objectID);

        /// <summary>
        /// Get Datasource from Datasource Field
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="displayFieldName"></param>
        /// <returns></returns>
        IEnumerable<MD_Datasource> GetDataSource(string tableName, string displayFieldName);

        /// <summary>
        /// Fetch all data from Object
        /// </summary>
        /// <param name="objectID">Object ID need to fetch data</param>
        MD_Live FetchViewObjectData(Guid objectID, bool isActive);

        /// <summary>
        /// Fetch record from Object by ID
        /// </summary>
        /// <param name="objectID">Object ID need to fetch data</param>
        MD_Live FetchViewObjectDataByID(Guid objectID, Guid recordID);

        /// <summary>
        /// Save an record of object to DB
        /// </summary>
        /// <param name="record"></param>
        void Add(Guid objectID, IEnumerable<TB_Field> record);

        /// <summary>
        /// Save an record of object to DB
        /// </summary>
        /// <param name="record"></param>
        void Update(Guid objectID, Guid recordID, IEnumerable<TB_Field> record);
    }
}
