using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration.Interfaces
{
    public interface IQueueRepository
    {
        /// <summary>
        /// Add or update queue.
        /// </summary>
        /// <param name="queue">Queue need to be saved.</param>
        void SaveQueue(TB_Queue queue);

        /// <summary>
        /// Change active status of Queue.
        /// </summary>
        /// <param name="id">Queue ID need to be changed the status.</param>
        /// <param name="isActive">Activation Status of Queue.</param>
        void SetQueueFlag(Guid id, bool isActive);

        /// <summary>
        /// Get the first record from queue.
        /// </summary>
        /// <returns>Signle Queue returned after sorting by create date.</returns>
        TB_Queue GetQueue();

        /// <summary>
        /// Back up database to bak file
        /// </summary>
        /// <param name="database">Name of DB need to be backd up.</param>
        /// <param name="backupPath">Backup file path.</param>
        void BackupServer(string database, string backupPath);
    }
}
