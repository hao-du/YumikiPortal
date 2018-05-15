using System;
using System.Collections.Generic;
using Yumiki.Entity.OnTime;

namespace Yumiki.Data.OnTime.Interfaces
{
    public interface IPhaseRepository
    {
        /// <summary>
        /// Get all active/Inactive phase
        /// </summary>
        List<TB_Phase> GetAllPhases(bool isActive);

        /// <summary>
        /// Get a phase by id
        /// </summary>
        /// <param name="id">Phase ID</param>
        TB_Phase GetPhase(Guid id);

        /// <summary>
        /// Create/Update a Project
        /// </summary>
        /// <param name="project">If Project id is empty, then this is new Project. Otherwise, this needs to be updated</param>
        void SavePhase(TB_Phase phase);
    }
}
