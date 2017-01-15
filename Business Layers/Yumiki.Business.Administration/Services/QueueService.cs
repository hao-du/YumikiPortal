using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Security;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class QueueService : BaseService<IQueueRepository>, IQueueService
    {
        /// <summary>
        /// Add or update queue.
        /// </summary>
        /// <param name="queue">Queue need to be saved.</param>
        public void SaveQueue(TB_Queue queue)
        {
            Repository.SaveQueue(queue);
        }

        /// <summary>
        /// Get the first record from queue.
        /// </summary>
        public void ExecuteQueue()
        {
            //Check whether queue has been set the status or not.
            bool flagSet = false;
            TB_Queue queue = Repository.GetQueue();

            if (queue != null)
            {
                Logger.Infomation(string.Format("Start executing Queue: ID: {0} - Type: {1} - Value1: {2} - Value2: {3} - Value3: {4}", queue.ID, queue.QueueType.ToString(), queue.Value1, queue.Value2, queue.Value3));

                switch (queue.QueueType)
                {
                    case EN_QueueType.E_SHUTDOWN_SERVER:
                        //Set Active Flag to false before shutdown server.
                        Repository.SetQueueFlag(queue.ID, false);
                        flagSet = true;

                        var psi = new ProcessStartInfo("shutdown", "/s /f /t 0");
                        psi.CreateNoWindow = true;
                        psi.UseShellExecute = false;
                        //Process.Start(psi);
                        break;
                }

                if (flagSet)
                {
                    Repository.SetQueueFlag(queue.ID, false);
                }

                Logger.Infomation(string.Format("End executing Queue: ID: {0} - Type: {1} - Value1: {2} - Value2: {3} - Value3: {4}", queue.ID, queue.QueueType.ToString(), queue.Value1, queue.Value2, queue.Value3));
            }
            else
            {
                Logger.Infomation(string.Format("No Queue to be executed at {0}", DateTimeHelper.GetLocalSystemDatetime()));
            }
        }
    }
}
