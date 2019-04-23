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
    public class MD_Invoice : MD_Base<TB_Invoice>
    {
        public MD_Invoice()
        {
            this._internalItem = new TB_Invoice();
        }

        public MD_Invoice(TB_Invoice invoice)
        {
            if(invoice == null)
            {
                _internalItem = new TB_Invoice();
                return;
            }

            _internalItem = invoice;
        }

        public string InvoiceNumber
        {
            get
            {
                return _internalItem.InvoiceNumber;
            }
            set
            {
                _internalItem.InvoiceNumber = value;
            }
        }

        public string CustomerName
        {
            get
            {
                return _internalItem.CustomerName;
            }
            set
            {
                _internalItem.CustomerName = value;
            }
        }

        public string CustomerAddress
        {
            get
            {
                return _internalItem.CustomerAddress;
            }
            set
            {
                _internalItem.CustomerAddress = value;
            }
        }

        public string CustomerPhone
        {
            get
            {
                return _internalItem.CustomerPhone;
            }
            set
            {
                _internalItem.CustomerPhone = value;
            }
        }

        public string CustomerEmail
        {
            get
            {
                return _internalItem.CustomerEmail;
            }
            set
            {
                _internalItem.CustomerEmail = value;
            }
        }

        public string CustomerNote
        {
            get
            {
                return _internalItem.CustomerNote;
            }
            set
            {
                _internalItem.CustomerNote = value;
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
                _internalItem.Status = (EN_InvoiceStatus)int.Parse(value);
            }
        }

        public string StatusUI
        {
            get
            {
                return EnumHelper.GetDescription((EN_InvoiceStatus)_internalItem.Status);
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

        public string InvoiceDate
        {
            get
            {
                return _internalItem.InvoiceDate.ToString(Formats.DateTime.LongDate);
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _internalItem.InvoiceDate = DateTime.Parse(value);
                }
            }
        }

        private List<MD_InvoiceDetail> _invoiceDetails = null;
        public List<MD_InvoiceDetail> InvoiceDetails
        {
            get
            {
                if (_invoiceDetails == null)
                {
                    _invoiceDetails =_internalItem.InvoiceDetails.Select(c => new MD_InvoiceDetail(c)).ToList();
                }

                if (_invoiceDetails == null)
                {
                    _invoiceDetails = new List<MD_InvoiceDetail>();
                }

                return _invoiceDetails;
            }
            set
            {
                _internalItem.InvoiceDetails.Clear();

                value.ForEach(c => _internalItem.InvoiceDetails.Add(c.ToObject()));
            }
        }

        private List<MD_InvoiceExtraFee> _invoiceExtraFees = null;
        public List<MD_InvoiceExtraFee> InvoiceExtraFees
        {
            get
            {
                if (_invoiceExtraFees == null)
                {
                    _invoiceExtraFees = _internalItem.InvoiceExtraFees.Select(c => new MD_InvoiceExtraFee(c)).ToList();
                }

                if (_invoiceExtraFees == null)
                {
                    _invoiceExtraFees = new List<MD_InvoiceExtraFee>();
                }

                return _invoiceExtraFees;
            }
            set
            {
                _internalItem.InvoiceExtraFees.Clear();

                value.ForEach(c => _internalItem.InvoiceExtraFees.Add(c.ToObject()));
            }
        }

        public override TB_Invoice ToObject()
        {
            _internalItem.UserID = CurrentUser.UserID;

            List<TB_InvoiceDetail> details = new List<TB_InvoiceDetail>();
            details.AddRange(InvoiceDetails.Select(c => c.ToObject()));
            _internalItem.InvoiceDetails = details;

            List< TB_InvoiceExtraFee> extraFees = new List<TB_InvoiceExtraFee>();
            extraFees.AddRange(InvoiceExtraFees.Select(c => c.ToObject()));
            _internalItem.InvoiceExtraFees = extraFees;

            return base.ToObject();
        }
    }
}