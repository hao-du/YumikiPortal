using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Business.Shopper.Services
{
    public class FeeTypeService : BaseService<IFeeTypeRepository>, IFeeTypeService
    {
        public List<TB_FeeType> GetFeeTypes(bool showInactive)
        {
            return Repository.GetFeeTypes(showInactive);
        }

        public TB_FeeType GetFeeType(Guid feeTypeID)
        {
            return Repository.GetFeeType(feeTypeID);
        }

        public void SaveFeeType(TB_FeeType feeType)
        {
            Repository.SaveFeeType(feeType);
        }
    }
}
