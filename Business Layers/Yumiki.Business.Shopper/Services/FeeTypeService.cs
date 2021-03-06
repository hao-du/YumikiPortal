﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Business.Shopper.Services
{
    public class FeeTypeService : BaseService<IFeeTypeRepository>, IFeeTypeService
    {
        public List<TB_FeeType> GetFeeTypes(bool showInactive, bool forReceipt, bool forInvoice, bool forAdditionFee)
        {
            return Repository.GetFeeTypes(showInactive, null, forReceipt, forInvoice, forAdditionFee);
        }
        public List<TB_FeeType> GetFeeTypes(string term, bool forReceipt, bool forInvoice, bool forAdditionFee)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Search Term cannot be empty.", Logger);
            }

            return Repository.GetFeeTypes(false, term, forReceipt, forInvoice, forAdditionFee);
        }

        public TB_FeeType GetFeeType(string feeTypeID)
        {
            if (string.IsNullOrWhiteSpace(feeTypeID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Fee Type ID cannot be empty.", Logger);
            }

            Guid convertedFeeTypeID = Guid.Empty;
            Guid.TryParse(feeTypeID, out convertedFeeTypeID);
            if (convertedFeeTypeID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Fee Type ID must be GUID type.", Logger);
            }

            return Repository.GetFeeType(convertedFeeTypeID);
        }

        public void SaveFeeType(TB_FeeType feeType)
        {
            if (string.IsNullOrWhiteSpace(feeType.FeeTypeName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Fee Type Name is required.", Logger);
            }

            if (!feeType.ShowInAdditionalFee && !feeType.ShowInInvoice && !feeType.ShowInReceipt)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Fee Type must be applied to at least one free.", Logger);
            }

            Repository.SaveFeeType(feeType);
        }
    }
}
