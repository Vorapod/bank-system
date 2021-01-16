using AutoMapper;
using BankSystem.BLL;
using BankSystem.BLL.Interface;
using BankSystem.BLL.Model;
using BankSystem.DAL;
using BankSystem.DAL.Interface;
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
        public AccountModel New([FromBody] AccountModel account)
        {
            var result = _bll.AddAccount(account);
            return result;
        }
        [Route("{iBANNumber}/Deposit")]
        [HttpPost]
        public AccountModel Deposit(string iBANNumber,[FromBody] DepositModel deposit)
        {
            var result = _bll.Deposit(iBANNumber, deposit);
            return result;
        }

        [HttpPost]
        [Route("transfer")]
        public AccountModel Transfer([FromBody] TransferModel transfer)
        {
            var result = _bll.Transfer(transfer);
            return result;
        }

        [HttpGet]
        [Route("{iBANNumber}")]
        public AccountModel GetAccount(string iBANNumber)
        {
            var result = _bll.GetAccountById(iBANNumber);
            return result;
        }
    }
}