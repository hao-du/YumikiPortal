using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Business.Shopper.Interfaces
{
    public interface IReceiptService
    {
        GetResponse<TB_Receipt> GetReceipts(GetShopperRequest<TB_Receipt> request);

        TB_Receipt GetReceipt(string receiptID);

        List<TB_ReceiptDetail> GetReceiptsByProductID(string productID);

        void SaveReceipt(TB_Receipt receipt);

        List<ExtendEnum> GetStatuses();
    }
}
