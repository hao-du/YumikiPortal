using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Repositories
{
    public class ReceiptRepository : ShopperRepository, IReceiptRepository
    {
        public List<TB_Receipt> GetReceipts(bool showInactive)
        {
            return Context.TB_Receipt.Where(c => c.IsActive == !showInactive).ToList();
        }

        public TB_Receipt GetReceipt(Guid receiptID)
        {
            return Context.TB_Receipt.SingleOrDefault(c => c.ID == receiptID);
        }

        public void SaveReceipt(TB_Receipt receipt)
        {
            if (receipt.ID == Guid.Empty)
            {
                Context.TB_Receipt.Add(receipt);
            }
            else
            {
                TB_Receipt dbReceipt = Context.TB_Receipt.Single(c => c.ID == receipt.ID);
                dbReceipt.ExternalReceiptID = receipt.ExternalReceiptID;
                dbReceipt.ExternalReceiptUrl = receipt.ExternalReceiptUrl;
                dbReceipt.TotalAmount = receipt.TotalAmount;
                dbReceipt.ReceiptDate = receipt.ReceiptDate;
                dbReceipt.Status = receipt.Status;

                dbReceipt.Descriptions = receipt.Descriptions;
                dbReceipt.IsActive = receipt.IsActive;
            }

            Save();
        }
    }
}
