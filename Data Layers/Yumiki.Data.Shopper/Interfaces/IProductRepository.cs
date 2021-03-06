﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IProductRepository
    {
        List<TB_Product> GetProducts(bool showInactive, string term);

        TB_Product GetProduct(Guid productID);

        void SaveProduct(TB_Product product);
    }
}
