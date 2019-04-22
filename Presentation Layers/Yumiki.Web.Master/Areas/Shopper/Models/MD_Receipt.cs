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
    public class MD_Receipt : MD_Base<TB_Receipt>
    {
        public MD_Receipt()
        {
            this._internalItem = new TB_Receipt();
        }

        public MD_Receipt(TB_Receipt receipt)
        {
            if(receipt == null)
            {
                _internalItem = new TB_Receipt();
                return;
            }

            _internalItem = receipt;
        }

        public string ExternalReceiptID
        {
            get
            {
                return _internalItem.ExternalReceiptID;
            }
            set
            {
                _internalItem.ExternalReceiptID = value;
            }
        }

        public string ExternalReceiptUrl
        {
            get
            {
                return _internalItem.ExternalReceiptUrl;
            }
            set
            {
                _internalItem.ExternalReceiptUrl = value;
            }
        }

        public string Status
        {
            get
            {
                return ((int)_internalItem.Status).ToString();
            }
            set
            {
                _internalItem.Status = (EN_ReceiptStatus)int.Parse(value);
            }
        }

        public string StatusUI
        {
            get
            {
                return EnumHelper.GetDescription((EN_ReceiptStatus)_internalItem.Status);
            }
        }

        public decimal TotalAmount
        {
            get
            {
                return _internalItem.TotalAmount;
            }
            set
            {
                _internalItem.TotalAmount = value;
            }
        }

        public string ReceiptDate
        {
            get
            {
                return _internalItem.ReceiptDate.ToString(Formats.DateTime.LongDate);
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _internalItem.ReceiptDate = DateTime.Parse(value);
                }
            }
        }

        private List<MD_ReceiptDetail> _receiptDetails = null;
        public List<MD_ReceiptDetail> ReceiptDetails
        {
            get
            {
                if (_receiptDetails == null)
                {
                    _receiptDetails =_internalItem.ReceiptDetails.Select(c => new MD_ReceiptDetail(c)).ToList();
                }

                if (_receiptDetails == null)
                {
                    _receiptDetails = new List<MD_ReceiptDetail>();
                }

                return _receiptDetails;
            }
            set
            {
                _internalItem.ReceiptDetails.Clear();

                value.ForEach(c => _internalItem.ReceiptDetails.Add(c.ToObject()));
            }
        }

        private List<MD_ReceiptExtraFee> _receiptExtraFees = null;
        public List<MD_ReceiptExtraFee> ReceiptExtraFees
        {
            get
            {
                if (_receiptExtraFees == null)
                {
                    _receiptExtraFees = _internalItem.ReceiptExtraFees.Select(c => new MD_ReceiptExtraFee(c)).ToList();
                }

                if (_receiptExtraFees == null)
                {
                    _receiptExtraFees = new List<MD_ReceiptExtraFee>();
                }

                return _receiptExtraFees;
            }
            set
            {
                _internalItem.ReceiptExtraFees.Clear();

                value.ForEach(c => _internalItem.ReceiptExtraFees.Add(c.ToObject()));
            }
        }

        public override TB_Receipt ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            List<TB_ReceiptDetail> details = new List<TB_ReceiptDetail>();
            details.AddRange(ReceiptDetails.Select(c => c.ToObject()));
            _internalItem.ReceiptDetails = details;

            List< TB_ReceiptExtraFee> extraFees = new List<TB_ReceiptExtraFee>();
            extraFees.AddRange(ReceiptExtraFees.Select(c => c.ToObject()));
            _internalItem.ReceiptExtraFees = extraFees;

            return base.ToObject();
        }
    }
}