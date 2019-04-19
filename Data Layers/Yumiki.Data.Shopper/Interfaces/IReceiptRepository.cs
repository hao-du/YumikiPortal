using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IReceiptRepository
    {
        List<TB_Receipt> GetReceipts(bool showInactive);

        TB_Receipt GetReceipt(Guid receiptID);

        void SaveReceipt(TB_Receipt receipt);
    }
}
