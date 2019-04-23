using System;
using System.ComponentModel.DataAnnotations;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Web.Shopper.Models
{
    public class MD_InvoiceDetail : MD_Base<TB_InvoiceDetail>
    {
        public MD_InvoiceDetail()
        {
            this._internalItem = new TB_InvoiceDetail();
        }

        public MD_InvoiceDetail(TB_InvoiceDetail invoiceDetail)
        {
            if(invoiceDetail == null)
            {
                _internalItem = new TB_InvoiceDetail();
                return;
            }

            _internalItem = invoiceDetail;
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

        public Guid InvoiceID
        {
            get
            {
                return _internalItem.InvoiceID;
            }
            set
            {
                _internalItem.InvoiceID = value;
            }
        }

        public decimal OriginalPrice
        {
            get
            {
                return _internalItem.OriginalPrice;
            }
            set
            {
                _internalItem.OriginalPrice = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return _internalItem.UnitPrice;
            }
            set
            {
                _internalItem.UnitPrice = value;
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

        public decimal Amount
        {
            get
            {
                return _internalItem.Amount;
            }
            set
            {
                _internalItem.Amount = value;
            }
        }

        public override TB_InvoiceDetail ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}