using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IProductQuantityOffsetRepository
    {
        List<TB_ProductQuantityOffset> GetProductQuantityOffsets(bool showInactive, Guid? productID);

        TB_ProductQuantityOffset GetProductQuantityOffset(Guid productQuantityOffsetID);

        void SaveProductQuantityOffset(TB_ProductQuantityOffset productQuantityOffset);
    }
}
