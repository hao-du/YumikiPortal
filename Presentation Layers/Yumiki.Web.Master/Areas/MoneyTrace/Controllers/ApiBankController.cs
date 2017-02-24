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
    [RoutePrefix("api/bank")]
    public class ApiBankController : ApiBaseController<IBankService>
    {
        [Route("getall", Name = RouteNames.BankGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive, int currentPage, int itemsPerPage)
        {
            try
            {
                GetBankRequest<TB_Bank> bankRequest = new GetBankRequest<TB_Bank>
                {
                    UserID = CurrentUser.UserID,
                    ShowInactive = showInactive,
                    GetShareable = false,
                    CurrentPage = currentPage,
                    ItemsPerPage = itemsPerPage
                };

                GetBankResponse<TB_Bank> banks = BusinessService.GetAllBanks(bankRequest);
                return Ok(banks);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("getwithshareableitems", Name = RouteNames.BankGetAllWithShareableItems)]
        [HttpGet()]
        public IHttpActionResult GetWithShareableItems(bool showInactive)
        {
            try
            {
                GetBankRequest<TB_Bank> bankRequest = new GetBankRequest<TB_Bank>
                {
                    UserID = CurrentUser.UserID,
                    ShowInactive = showInactive,
                    GetShareable = true,
                };

                IEnumerable<TB_Bank> banks = BusinessService.GetAllBanks(bankRequest).Records;
                return Ok(banks);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.BankGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string bankID)
        {
            try
            {
                TB_Bank bank = BusinessService.GetBank(bankID);
                return Ok(bank);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.BankSave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] TB_Bank item)
        {
            try
            {
                item.UserID = CurrentUser.UserID;

                BusinessService.SaveBank(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
