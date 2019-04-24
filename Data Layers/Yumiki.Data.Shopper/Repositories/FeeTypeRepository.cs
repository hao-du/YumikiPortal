using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Repositories
{
    public class FeeTypeRepository : ShopperRepository, IFeeTypeRepository
    {
        public List<TB_FeeType> GetFeeTypes(bool showInactive, string term, bool forReceipt, bool forInvoice, bool forAdditionFee)
        {
            Expression<Func<TB_FeeType, bool>> expression = PredicateBuilder.New<TB_FeeType>(true);
            expression = expression.And(c => c.IsActive != showInactive);

            Expression<Func<TB_FeeType, bool>> displayExpression = PredicateBuilder.New<TB_FeeType>(false);
            bool hasDiplay = false;
            if (forReceipt) { displayExpression = displayExpression.Or(c => c.ShowInReceipt); hasDiplay = true; }
            if (forInvoice) { displayExpression = displayExpression.Or(c => c.ShowInInvoice); hasDiplay = true; }
            if (forAdditionFee) { displayExpression = displayExpression.Or(c => c.ShowInAdditionalFee); hasDiplay = true; }

            if (hasDiplay)
            {
                expression = expression.And(displayExpression);
            }

            if (!string.IsNullOrWhiteSpace(term))
            {
                expression = expression.And(c => c.FeeTypeName.Contains(term));
            }

            return Context.TB_FeeType.Where(expression).ToList();
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
