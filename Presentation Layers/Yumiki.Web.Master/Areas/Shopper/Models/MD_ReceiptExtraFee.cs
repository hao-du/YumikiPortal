using System;
using System.ComponentModel.DataAnnotations;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Web.Shopper.Models
{
    public class MD_ReceiptExtraFee : MD_Base<TB_ReceiptExtraFee>
    {
        public MD_ReceiptExtraFee()
        {
            this._internalItem = new TB_ReceiptExtraFee();
        }

        public MD_ReceiptExtraFee(TB_ReceiptExtraFee extraFee)
        {
            if(extraFee == null)
            {
                _internalItem = new TB_ReceiptExtraFee();
                return;
            }

            _internalItem = extraFee;
        }

        public MD_FeeType FeeType
        {
            get
            {
                return new MD_FeeType(_internalItem.FeeType);
            }
        }

        public string FeeTypeName
        {
            get
            {
                return _internalItem.FeeTypeName;
            }
        }

        public Guid FeeTypeID
        {
            get
            {
                return _internalItem.FeeTypeID;
            }
            set
            {
                _internalItem.FeeTypeID = value;
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

        public override TB_ReceiptExtraFee ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            return base.ToObject();
        }
    }
}