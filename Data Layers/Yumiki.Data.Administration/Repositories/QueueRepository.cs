using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data.SqlClient;
using System.Linq;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration.Repositories
{
    public class QueueRepository : AdministrationRepository, IQueueRepository
    {
        /// <summary>
        /// Add or update queue.
        /// </summary>
        /// <param name="queue">Queue need to be saved.</param>
        public void SaveQueue(TB_Queue queue)
        {
            if (queue.ID == Guid.Empty)
            {
                Context.TB_Queue.Add(queue);
            }
            else
            {
                TB_Queue dbQueue = Context.TB_Queue.Where(c => c.ID == queue.ID).Single();
                dbQueue.QueueType = queue.QueueType;
                dbQueue.Value1 = queue.Value1;
                dbQueue.Value2 = queue.Value2;
                dbQueue.Value3 = queue.Value3;
                dbQueue.UserID = queue.UserID;
                dbQueue.Descriptions = queue.Descriptions;
                dbQueue.IsActive = queue.IsActive;
            }

            Save();
        }

        /// <summary>
        /// Change active status of Queue.
        /// </summary>
        /// <param name="id">Queue ID need to be changed the status.</param>
        /// <param name="isActive">Activation Status of Queue.</param>
        public void SetQueueFlag(Guid id, bool isActive)
        {
            TB_Queue dbQueue = Context.TB_Queue.Where(c => c.ID == id).Single();
            dbQueue.IsActive = isActive;

            Save();
        }

        /// <summary>
        /// Get the first record from queue.
        /// </summary>
        /// <returns>Signle Queue returned after sorting by create date.</returns>
        public TB_Queue GetQueue()
        {
            return Context.TB_Queue.Where(c => c.IsActive).OrderBy(c => c.CreateDate).FirstOrDefault();
        }

        /// <summary>
        /// Back up database to bak file
        /// </summary>
        /// <param name="database">Name of DB need to be backd up.</param>
        /// <param name="backupPath">Backup file path.</param>
        public void BackupServer(string database, string backupPath)
        {
            ServerConnection serverConnection = new ServerConnection((SqlConnection)Context.Database.Connection);

            //Open sql server instance
            Server server = new Server(serverConnection);

            //Create backup settings
            Backup backup = new Backup();
            backup.Devices.AddDevice(backupPath, DeviceType.File);
            backup.Database = database;
            backup.Action = BackupActionType.Database;
            backup.Initialize = true;

            backup.SqlBackup(server);
        }
    }
}
