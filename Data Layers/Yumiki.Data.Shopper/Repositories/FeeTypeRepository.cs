using System;
using System.Collections.Generic;
using System.Linq;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Repositories
{
    public class FeeTypeRepository : ShopperRepository, IFeeTypeRepository
    {
        public List<TB_FeeType> GetFeeTypes(bool showInactive)
        {
            return Context.TB_FeeType.Where(c => c.IsActive == !showInactive).ToList();
        }

        public TB_FeeType GetFeeType(Guid feeTypeID)
        {
            return Context.TB_FeeType.SingleOrDefault(c => c.ID == feeTypeID);
        }

        public void SaveFeeType(TB_FeeType feeType)
        {
            if (feeType.ID == Guid.Empty)
            {
                Context.TB_FeeType.Add(feeType);
            }
            else
            {
                TB_FeeType dbFeeType = Context.TB_FeeType.Single(c => c.ID == feeType.ID);
                dbFeeType.FeeTypeName = feeType.FeeTypeName;
                dbFeeType.ShowInAdditionalFee = feeType.ShowInAdditionalFee;
                dbFeeType.ShowInReceipt = feeType.ShowInReceipt;
                dbFeeType.ShowInInvoice = feeType.ShowInInvoice;
                dbFeeType.Descriptions = feeType.Descriptions;
                dbFeeType.IsActive = feeType.IsActive;
            }

            Save();
        }
    }
}
