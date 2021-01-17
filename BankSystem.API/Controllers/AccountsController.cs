using AutoMapper;
using BankSystem.API.Filters;
using BankSystem.BLL;
using BankSystem.BLL.Interface;
using BankSystem.BLL.Model;
using BankSystem.DAL;
using BankSystem.DAL.Interface;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankSystem.API.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController
    {
        private readonly IBankSystemBLL _bll;
        public AccountsController(IBankSystemBLL bll)
        {
            _bll = bll;
        }

        [HttpPost]
        [Route("")]
        [CustomExceptionFilter]
        public IHttpActionResult New([FromBody] AccountModel account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _bll.AddAccount(account);
            return Created($"http://localhost:44327/api/account/{result.IBANNumber}", result);
        }
        [Route("{iBANNumber}/Deposit")]
        [HttpPost]
        [CustomExceptionFilter]
        public IHttpActionResult Deposit(string iBANNumber,[FromBody] DepositModel deposit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _bll.Deposit(iBANNumber, deposit);
            return Ok(result);
        }

        [HttpPost]
        [Route("transfer")]
        [CustomExceptionFilter]
        public IHttpActionResult Transfer([FromBody] TransferModel transfer)
        {
            var result = _bll.Transfer(transfer);
            return Ok(result);
        }

        [HttpGet]
        [Route("{iBANNumber}")]
        [CustomExceptionFilter]
        public IHttpActionResult GetAccount(string iBANNumber)
        {
            var result = _bll.GetAccountById(iBANNumber);
            return Ok(result);
        }
    }
}