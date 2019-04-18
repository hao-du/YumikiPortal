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
    public class AdditionalFeeService : BaseService<IAdditionalFeeRepository>, IAdditionalFeeService
    {
        public List<TB_AdditionalFee> GetAdditionalFees(bool showInactive)
        {
            return Repository.GetAdditionalFees(showInactive);
        }

        public TB_AdditionalFee GetAdditionalFee(Guid additionalFeeID)
        {
            return Repository.GetAdditionalFee(additionalFeeID);
        }

        public void SaveAdditionalFee(TB_AdditionalFee additionalFee)
        {
            Repository.SaveAdditionalFee(additionalFee);
        }
    }
}
