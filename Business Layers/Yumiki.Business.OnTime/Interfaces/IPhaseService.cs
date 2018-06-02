using System.Collections.Generic;
using Yumiki.Entity.OnTime;

namespace Yumiki.Business.OnTime.Interfaces
{
    public interface IPhaseService
    {
        /// <summary>
        /// Get all active/Inactive phase
        /// </summary>
        List<TB_Phase> GetAllPhases(bool isActive);

        /// <summary>
        /// Get a phase by id
        /// </summary>
        /// <param name="id">Phase ID</param>
        TB_Phase GetPhase(string id);

        /// <summary>
        /// Create/Update a Phase
        /// </summary>
        /// <param name="phase">If Phase id is empty, then this is new Phase. Otherwise, this needs to be updated</param>
        void SavePhase(TB_Phase phase);
    }
}
