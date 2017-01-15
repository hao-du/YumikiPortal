using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Interfaces
{
    public interface IQueueService
    {
        /// <summary>
        /// Add or update queue.
        /// </summary>
        /// <param name="queue">Queue need to be saved.</param>
        void SaveQueue(TB_Queue queue);

        /// <summary>
        /// Execute queue stored in database.
        /// </summary>
        /// <returns>True if execute successfully.</returns>
        void ExecuteQueue();
    }
}
