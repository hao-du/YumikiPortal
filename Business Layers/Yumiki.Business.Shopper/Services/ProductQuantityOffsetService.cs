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
    public class ProductQuantityOffsetService : BaseService<IProductQuantityOffsetRepository>, IProductQuantityOffsetService
    {
        public List<TB_ProductQuantityOffset> GetProductQuantityOffsets(bool showInactive, string productID = null)
        {
            Guid? productIDParam = null;

            if (!string.IsNullOrWhiteSpace(productID))
            {
                Guid convertedProductID = Guid.Empty;
                if (!Guid.TryParse(productID, out convertedProductID))
                {
                    throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Product ID must be GUID type.", Logger);
                }

                productIDParam = convertedProductID;
            }

            return Repository.GetProductQuantityOffsets(showInactive, productIDParam);
        }

        public TB_ProductQuantityOffset GetProductQuantityOffset(string productQuantityOffsetID)
        {
            if (string.IsNullOrWhiteSpace(productQuantityOffsetID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Offset ID cannot be empty.", Logger);
            }

            Guid convertedProductQuantityOffsetID = Guid.Empty;
            Guid.TryParse(productQuantityOffsetID, out convertedProductQuantityOffsetID);
            if (convertedProductQuantityOffsetID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "ProductQuantityOffset ID must be GUID type.", Logger);
            }

            return Repository.GetProductQuantityOffset(convertedProductQuantityOffsetID);
        }

        public void SaveProductQuantityOffset(TB_ProductQuantityOffset productQuantityOffset)
        {
            if (productQuantityOffset.Quantity == 0)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Quantity cannot be 0.", Logger);
            }

            if (string.IsNullOrWhiteSpace(productQuantityOffset.Descriptions))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Descriptions is required for adding offset reason.", Logger);
            }

            Repository.SaveProductQuantityOffset(productQuantityOffset);
        }
    }
}
