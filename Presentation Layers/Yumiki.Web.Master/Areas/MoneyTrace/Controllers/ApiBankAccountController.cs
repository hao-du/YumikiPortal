using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;
using Yumiki.Web.Base;
using Yumiki.Web.MoneyTrace.Constants;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    [RoutePrefix("api/bankaccount")]
    public class ApiBankAccountController : ApiBaseController<IBankAccountService>
    {
        [Route("getall", Name = RouteNames.BankAccountGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive, int currentPage, int itemsPerPage)
        {
            try
            {
                GetBankAccountRequest<TB_BankAccount> bankAccountRequest = new GetBankAccountRequest<TB_BankAccount>
                {
                    UserID = CurrentUser.UserID,
                    ShowInactive = showInactive,
                    CurrentPage = currentPage,
                    ItemsPerPage = itemsPerPage
                };

                GetBankAccountResponse<TB_BankAccount> bankAccounts = BusinessService.GetAllBankAccounts(bankAccountRequest);
                return Ok(bankAccounts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.BankAccountGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string bankAccountId)
        {
            try
            {
                TB_BankAccount bankAccount = BusinessService.GetBankAccount(bankAccountId);
                return Ok(bankAccount);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.BankAccountSave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] TB_BankAccount item)
        {
            try
            {
                item.UserID = CurrentUser.UserID;

                BusinessService.SaveBankAccount(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
