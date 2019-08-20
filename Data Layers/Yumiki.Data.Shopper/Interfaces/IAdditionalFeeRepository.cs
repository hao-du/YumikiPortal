using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Data.Base;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IAdditionalFeeRepository
    {
        GetResponse<TB_AdditionalFee> GetAdditionalFees(GetShopperRequest<TB_AdditionalFee> request);

        TB_AdditionalFee GetAdditionalFee(Guid additionalFeeID);

        void SaveAdditionalFee(TB_AdditionalFee additionalFee);
    }
}
