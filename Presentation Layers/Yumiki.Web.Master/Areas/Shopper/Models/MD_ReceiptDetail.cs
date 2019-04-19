﻿using System;
using System.ComponentModel.DataAnnotations;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Web.Shopper.Models
{
    public class MD_ReceiptDetail : MD_Base<TB_ReceiptDetail>
    {
        public MD_ReceiptDetail()
        {
            this._internalItem = new TB_ReceiptDetail();
        }

        public MD_ReceiptDetail(TB_ReceiptDetail receiptDetail)
        {
            if(receiptDetail == null)
            {
                _internalItem = new TB_ReceiptDetail();
                return;
            }

            _internalItem = receiptDetail;
        }

        public string ProductName
        {
            get
            {
                return _internalItem.Product == null ? string.Empty : _internalItem.Product.ProductName;
            }
        }

        public string ProductCode
        {
            get
            {
                return _internalItem.Product == null ? string.Empty : _internalItem.Product.ProductCode;
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

        public override TB_ReceiptDetail ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}