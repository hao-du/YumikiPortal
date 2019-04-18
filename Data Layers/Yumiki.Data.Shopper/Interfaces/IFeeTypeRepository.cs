using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IFeeTypeRepository
    {
        List<TB_FeeType> GetFeeTypes(bool showInactive);

        TB_FeeType GetFeeType(Guid feeTypeID);

        void SaveFeeType(TB_FeeType feeType);
    }
}
