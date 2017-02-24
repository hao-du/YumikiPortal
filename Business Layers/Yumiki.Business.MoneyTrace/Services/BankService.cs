using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Business.MoneyTrace.Services
{
    public class BankService : BaseService<IBankRepository>, IBankService
    {
        /// <summary>
        /// Get all active Banks from Database.
        /// </summary>
        /// <param name="request">Request include all filter parameters</param>
        /// <returns>List of all active Banks.</returns>
        public GetBankResponse<TB_Bank> GetAllBanks(GetBankRequest<TB_Bank> request)
        {
            return Repository.GetAllBanks(request);
        }

        /// <summary>
        /// Get a specific bank.
        /// </summary>
        /// <param name="bankID">Specify id for bank need to be retrieved.</param>
        /// <returns>Bank Object</returns>
        public TB_Bank GetBank(string bankID)
        {
            if (string.IsNullOrWhiteSpace(bankID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Bank ID cannot be empty.", Logger);
            }

            Guid convertedBankID = Guid.Empty;
            Guid.TryParse(bankID, out convertedBankID);
            if (convertedBankID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Bank ID must be GUID type.", Logger);
            }

            return Repository.GetBank(convertedBankID);
        }

        /// <summary>
        /// Create/Update a bank
        /// </summary>
        /// <param name="bank">If bank id is empty, then this is new bank. Otherwise, this needs to be updated</param>
        public void SaveBank(TB_Bank bank)
        {
            if (string.IsNullOrWhiteSpace(bank.BankName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Bank Name is required.", Logger);
            }

            Repository.SaveBank(bank);
        }
    }
}
