using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Data.Shopper.Repositories
{
    public class AdditionalFeeRepository : ShopperRepository, IAdditionalFeeRepository
    {
        public GetResponse<TB_AdditionalFee> GetAdditionalFees(GetShopperRequest<TB_AdditionalFee> request)
        {
            IQueryable<TB_AdditionalFee> query = Context.TB_AdditionalFee
                .Include(TB_AdditionalFee.FieldName.FeeType)
                .Where(c => c.IsActive == !request.ShowInactive)
                .OrderByDescending(c => c.FeeIssueDate);

            int count = 0;
            if (request.EnablePaging)
            {
                count = query.Count();
                query = query.Skip((request.CurrentPage - 1) * request.ItemsPerPage).Take(request.ItemsPerPage);
            }

            return new GetResponse<TB_AdditionalFee>
            {
                Records = query.ToList(),
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage,
                TotalItems = count
            };
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
