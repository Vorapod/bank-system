using AutoMapper;
using BankSystem.BLL;
using BankSystem.BLL.Model;
using BankSystem.DAL;
using System.Web.Http;

namespace BankSystem.API.Controllers
{
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

        // POST api/accounts/new
        [HttpPost]
        public AccountModel New([FromBody] AccountModel account)
        {
            var result = _bll.AddAccount(account);
            return result;
        }

        // POST api/accounts/deposit
        [HttpPost]
        public AccountModel Deposit([FromBody] DepositModel deposit)
        {
            var result = _bll.Debit(deposit.IBANNumber, deposit.Amount);
            return result;
        }

    }
}