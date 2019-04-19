using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Repositories
{
    public class AdditionalFeeRepository : ShopperRepository, IAdditionalFeeRepository
    {
        public List<TB_AdditionalFee> GetAdditionalFees(bool showInactive)
        {
            return Context.TB_AdditionalFee.Include(TB_AdditionalFee.FieldName.FeeType).Where(c => c.IsActive == !showInactive).ToList();
        }

        public TB_AdditionalFee GetAdditionalFee(Guid additionalFeeID)
        {
            return Context.TB_AdditionalFee.Include(TB_AdditionalFee.FieldName.FeeType).SingleOrDefault(c => c.ID == additionalFeeID);
        }

        public void SaveAdditionalFee(TB_AdditionalFee additionalFee)
        {
            if (additionalFee.ID == Guid.Empty)
            {
                Context.TB_AdditionalFee.Add(additionalFee);
            }
            else
            {
                TB_AdditionalFee dbAdditionalFee = Context.TB_AdditionalFee.Single(c => c.ID == additionalFee.ID);
                dbAdditionalFee.FeeTypeID = additionalFee.FeeTypeID;
                dbAdditionalFee.Amount = additionalFee.Amount;
                dbAdditionalFee.FeeIssueDate = additionalFee.FeeIssueDate;
                dbAdditionalFee.Descriptions = additionalFee.Descriptions;
                dbAdditionalFee.IsActive = additionalFee.IsActive;
            }

            Save();
        }
    }
}
