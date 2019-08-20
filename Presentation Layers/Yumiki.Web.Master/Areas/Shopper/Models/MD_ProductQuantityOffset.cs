using System;
using System.ComponentModel.DataAnnotations;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Web.Shopper.Models
{
    public class MD_ProductQuantityOffset : MD_Base<TB_ProductQuantityOffset>
    {
        public MD_ProductQuantityOffset()
        {
            this._internalItem = new TB_ProductQuantityOffset();
        }

        public MD_ProductQuantityOffset(TB_ProductQuantityOffset productQuantityOffset)
        {
            if(productQuantityOffset == null)
            {
                _internalItem = new TB_ProductQuantityOffset();
                return;
            }

            _internalItem = productQuantityOffset;
        }

        public MD_Product Product
        {
            get
            {
                return new MD_Product(_internalItem.Product);
            }
        }

        public string ProductName
        {
            get
            {
                return _internalItem.ProductName;
            }
        }

        public string ProductCode
        {
            get
            {
                return _internalItem.ProductCode;
            }
        }

        public string OffsetDateUI
        {
            get
            {
                return _internalItem.OffsetDateUI;
            }
        }

        public Guid ProductID
        {
            get
            {
                return _internalItem.ProductID;
            }
            set
            {
                _internalItem.ProductID = value;
            }
        }

        public int Quantity
        {
            get
            {
                return _internalItem.Quantity;
            }
            set
            {
                _internalItem.Quantity = value;
            }
        }

        public override TB_ProductQuantityOffset ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}