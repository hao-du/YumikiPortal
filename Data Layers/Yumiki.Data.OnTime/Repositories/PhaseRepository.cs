using System;
using System.Collections.Generic;
using System.Linq;
using Yumiki.Data.OnTime.Interfaces;
using Yumiki.Entity.OnTime;

namespace Yumiki.Data.OnTime.Repositories
{
    public class PhaseRepository : OnTimeRepository, IPhaseRepository
    {
        /// <summary>
        /// Get all active/Inactive phase
        /// </summary>
        public List<TB_Phase> GetAllPhases(bool isActive)
        {
            return Context.TB_Phase.Where(c => c.IsActive == isActive).ToList();
        }

        /// <summary>
        /// Get a phase by id
        /// </summary>
        /// <param name="id">Phase ID</param>
        public TB_Phase GetPhase(Guid id)
        {
            return Context.TB_Phase.SingleOrDefault(c => c.ID == id);
        }

        /// <summary>
        /// Create/Update a Phase
        /// </summary>
        /// <param name="phase">If Phase id is empty, then this is new Phase. Otherwise, this needs to be updated</param>
        public void SavePhase(TB_Phase phase)
        {
            if (phase.ID == Guid.Empty)
            {
                Context.TB_Phase.Add(phase);
            }
            else
            {
                TB_Phase dbPhase = Context.TB_Phase.Single(c => c.ID == phase.ID);
                dbPhase.PhaseName = phase.PhaseName;
                dbPhase.ActualEndDate = phase.ActualEndDate;
                dbPhase.ActualStartDate = phase.ActualStartDate;
                dbPhase.EstimatedEndDate = phase.EstimatedEndDate;
                dbPhase.EstimatedStartDate = phase.EstimatedStartDate;
                dbPhase.ReleaseVersion = phase.ReleaseVersion;
                dbPhase.Status = phase.Status;
                dbPhase.UserID = phase.UserID;
                dbPhase.Descriptions = phase.Descriptions;
                dbPhase.IsActive = phase.IsActive;
            }

            Save();
        }
    }
}
