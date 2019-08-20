using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Business.Shopper.Interfaces
{
    public interface IAdditionalFeeService
    {
        GetResponse<TB_AdditionalFee> GetAdditionalFees(GetShopperRequest<TB_AdditionalFee> request);

        TB_AdditionalFee GetAdditionalFee(string additionalFeeID);

        void SaveAdditionalFee(TB_AdditionalFee additionalFee);
    }
}
