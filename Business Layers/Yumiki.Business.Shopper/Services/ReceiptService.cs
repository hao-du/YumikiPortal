using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Business.Shopper.Services
{
    public class ReceiptService : BaseService<IReceiptRepository>, IReceiptService
    {
        public List<TB_Receipt> GetReceipts(bool showInactive)
        {
            return Repository.GetReceipts(showInactive);
        }

        public TB_Receipt GetReceipt(string receiptID)
        {
            if (string.IsNullOrWhiteSpace(receiptID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Receipt ID cannot be empty.", Logger);
            }

            Guid convertedReceiptID = Guid.Empty;
            Guid.TryParse(receiptID, out convertedReceiptID);
            if (convertedReceiptID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Receipt ID must be GUID type.", Logger);
            }

            return Repository.GetReceipt(convertedReceiptID);
        }

        public void SaveReceipt(TB_Receipt receipt)
        {
            if (receipt.TotalAmount <= decimal.Zero)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Total Amount must be greater than 0.", Logger);
            }

            Repository.SaveReceipt(receipt);
        }

        public List<ExtendEnum> GetStatuses()
        {
            return EnumHelper.GetDatasource<EN_ReceiptStatus>();
        }
    }
}
