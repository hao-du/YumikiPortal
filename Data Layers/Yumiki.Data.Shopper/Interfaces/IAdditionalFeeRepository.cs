using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IAdditionalFeeRepository
    {
        List<TB_AdditionalFee> GetAdditionalFees(bool showInactive);

        TB_AdditionalFee GetAdditionalFee(Guid additionalFeeID);

        void SaveAdditionalFee(TB_AdditionalFee additionalFee);
    }
}
