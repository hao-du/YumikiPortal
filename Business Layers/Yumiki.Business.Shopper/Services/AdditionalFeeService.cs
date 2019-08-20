using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Business.Shopper.Services
{
    public class AdditionalFeeService : BaseService<IAdditionalFeeRepository>, IAdditionalFeeService
    {
        public GetResponse<TB_AdditionalFee> GetAdditionalFees(GetShopperRequest<TB_AdditionalFee> request)
        {
            return Repository.GetAdditionalFees(request);
        }

        public TB_AdditionalFee GetAdditionalFee(string additionalFeeID)
        {
            if (string.IsNullOrWhiteSpace(additionalFeeID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Additional Fee ID cannot be empty.", Logger);
            }

            Guid convertedAdditionalFeeID = Guid.Empty;
            Guid.TryParse(additionalFeeID, out convertedAdditionalFeeID);
            if (convertedAdditionalFeeID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Additional Fee ID must be GUID type.", Logger);
            }

            return Repository.GetAdditionalFee(convertedAdditionalFeeID);
        }

        public void SaveAdditionalFee(TB_AdditionalFee additionalFee)
        {
            if (additionalFee.FeeTypeID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Fee Type is required.", Logger);
            }

            if (additionalFee.Amount <= decimal.Zero)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Amount must be greater than 0.", Logger);
            }


            if (additionalFee.FeeIssueDate == DateTime.MinValue)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Issued Date is required.", Logger);
            }

            Repository.SaveAdditionalFee(additionalFee);
        }
    }
}
