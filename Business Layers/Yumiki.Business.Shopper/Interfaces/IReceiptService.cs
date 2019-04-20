using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Business.Shopper.Interfaces
{
    public interface IReceiptService
    {
        List<TB_Receipt> GetReceipts(bool showInactive);

        TB_Receipt GetReceipt(string receiptID);

        void SaveReceipt(TB_Receipt receipt);

        List<ExtendEnum> GetStatuses();
    }
}
