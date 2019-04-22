using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Web.Shopper.Models
{
    public class MD_Product : MD_Base<TB_Product>
    {
        public MD_Product()
        {
            this._internalItem = new TB_Product();
        }

        public MD_Product(TB_Product product)
        {
            if(product == null)
            {
                _internalItem = new TB_Product();
                return;
            }

            _internalItem = product;
        }

        public string ProductCode
        {
            get
            {
                return _internalItem.ProductCode;
            }
            set
            {
                _internalItem.ProductCode = value;
            }
        }

        public string ProductName
        {
            get
            {
                return _internalItem.ProductName;
            }
            set
            {
                _internalItem.ProductName = value;
            }
        }
        
        public override TB_Product ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}