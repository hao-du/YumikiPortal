using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Business.Shopper.Interfaces
{
    public interface IAdditionalFeeService
    {
        List<TB_AdditionalFee> GetAdditionalFees(bool showInactive);

        TB_AdditionalFee GetAdditionalFee(string additionalFeeID);

        void SaveAdditionalFee(TB_AdditionalFee additionalFee);
    }
}
