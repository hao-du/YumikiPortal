using System;
using System.Collections.Generic;
using System.Data;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Business.WellCovered.Interfaces
{
    public interface ILiveService
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
        IEnumerable<TB_Field> GetFields(string objectID);

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
        /// Get Datasource from Datasource Field
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="displayFieldName"></param>
        /// <returns></returns>
        IEnumerable<MD_Datasource> GetDataSource(string dataSource);

        /// <summary>
        /// Fetch all data from Object
        /// </summary>
        /// <param name="objectID">Object ID need to fetch data</param>
        MD_Live FetchViewObjectData(string objectID, bool isActive);

        /// <summary>
        /// Fetch record from Object by ID
        /// </summary>
        /// <param name="objectID">Object ID need to fetch data</param>
        IEnumerable<TB_Field> GetFieldsByID(string objectID, string recordID);

        /// <summary>
        /// Save an record of object to DB
        /// </summary>
        /// <param name="record"></param>
        void Add(string objectID, Dictionary<string, string> fields);

        /// <summary>
        /// Save an record of object to DB
        /// </summary>
        /// <param name="record"></param>
        void Update(string objectID, string recordID, Dictionary<string, string> inputFields);
    }
}
