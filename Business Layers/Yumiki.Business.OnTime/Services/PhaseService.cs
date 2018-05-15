using System;
using System.Collections.Generic;
using Yumiki.Business.Base;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.OnTime.Interfaces;
using Yumiki.Entity.OnTime;

namespace Yumiki.Business.OnTime.Services
{
    public class PhaseService : BaseService<IPhaseRepository>, IPhaseService
    {
        /// <summary>
        /// Get all active/Inactive phase
        /// </summary>
        public List<TB_Phase> GetAllPhases(bool isActive)
        {
            return Repository.GetAllPhases(isActive);
        }

        /// <summary>
        /// Get a phase by id
        /// </summary>
        /// <param name="id">Phase ID</param>
        public TB_Phase GetPhase(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Phase ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(id, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Phase ID must be GUID type.", Logger);
            }

            return Repository.GetPhase(convertedID);
        }

        /// <summary>
        /// Create/Update a Phase
        /// </summary>
        /// <param name="phase">If Phase id is empty, then this is new Phase. Otherwise, this needs to be updated</param>
        public void SavePhase(TB_Phase phase)
        {
            if (string.IsNullOrWhiteSpace(phase.PhaseName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Phase Name is required.", Logger);
            }

            if (phase.EstimatedStartDate.HasValue && phase.EstimatedEndDate.HasValue && phase.EstimatedStartDate > phase.EstimatedEndDate)
            {
                throw new YumikiException(ExceptionCode.E_INVALID_VALUE, "Estimated Start Date must be smaller than Estimated End Date.", Logger);
            }

            if (phase.ActualStartDate.HasValue && phase.ActualEndDate.HasValue && phase.ActualStartDate > phase.ActualEndDate)
            {
                throw new YumikiException(ExceptionCode.E_INVALID_VALUE, "Actual Start Date must be smaller than Actual End Date.", Logger);
            }

            if (phase.Status <= 0)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Status is required.", Logger);
            }

            Repository.SavePhase(phase);
        }
    }
}
