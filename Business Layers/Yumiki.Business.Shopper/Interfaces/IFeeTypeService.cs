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
    public interface IFeeTypeService
    {
        List<TB_FeeType> GetFeeTypes(bool showInactive);

        List<TB_FeeType> GetFeeTypes(string term);

        TB_FeeType GetFeeType(string feeTypeID);

        void SaveFeeType(TB_FeeType feeType);
    }
}
