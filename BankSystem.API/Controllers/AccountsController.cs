using AutoMapper;
using BankSystem.BLL;
using BankSystem.BLL.Interface;
using BankSystem.BLL.Model;
using BankSystem.DAL;
using System.Web.Http;

namespace BankSystem.API.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController
    {
        private readonly BankSystemBLL _bll;
        public AccountsController()
        {
            //TODO: Implement DI later
            BankSystemDbContext db = new BankSystemDbContext();
            UnitOfWork uow = new UnitOfWork(db);
            MapperConfiguration mp = new MapperConfiguration(config =>
            {
                config.CreateMap<AccountModel, Account>().ReverseMap();
                config.CreateMap<TransactionModel, Transaction>().ReverseMap();
            });

            var mapper = new Mapper(mp);

            BankSystemBLL bll = new BankSystemBLL(uow, mapper);
            _bll = bll;
        }
        // POST api/accounts
        [HttpPost]
        [Route("")]
        public AccountModel New([FromBody] AccountModel account)
        {
            var result = _bll.AddAccount(account);
            return result;
        }
        // POST api/accounts{iBANNumber}/deposite
        [Route("{iBANNumber}/Deposit")]
        [HttpPost]
        public AccountModel Deposit(string iBANNumber,[FromBody] DepositModel deposit)
        {
            var result = _bll.Deposit(iBANNumber, deposit);
            return result;
        }

        // POST api/accounts/transfer
        [HttpPost]
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