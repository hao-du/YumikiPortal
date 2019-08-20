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

namespace Yumiki.Business.Shopper.Interfaces
{
    public interface IProductQuantityOffsetService
    {
        List<TB_ProductQuantityOffset> GetProductQuantityOffsets(bool showInactive, string productID = null);

        TB_ProductQuantityOffset GetProductQuantityOffset(string productQuantityOffsetID);

        void SaveProductQuantityOffset(TB_ProductQuantityOffset productQuantityOffset);
    }
}
