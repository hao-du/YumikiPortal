using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Data.Base;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IReceiptRepository
    {
        GetResponse<TB_Receipt> GetReceipts(GetShopperRequest<TB_Receipt> request);

        TB_Receipt GetReceipt(Guid receiptID);

        List<TB_ReceiptDetail> GetReceiptsByProductID(Guid productID);

        void SaveReceipt(TB_Receipt receipt);
    }
}
