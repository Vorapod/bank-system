using AutoMapper;
using BankSystem.BLL;
using BankSystem.BLL.Model;
using BankSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BankSystem.API.Controllers
{
    public class AccountsController : ApiController
    {
        private readonly BankSystemBLL _bll;
        public AccountsController()
        {
            //TODO: Move to DI
            BankSystemDbContext db = new BankSystemDbContext();
            UnitOfWork uow = new UnitOfWork(db);
            MapperConfiguration mp = new MapperConfiguration(config =>
            {
                config.CreateMap<AccountModel, Account>().ReverseMap();

            });

            var mapper = new Mapper(mp);

            BankSystemBLL bll = new BankSystemBLL(uow, mapper);
            _bll = bll;
        }

        // POST api/accounts
        public AccountModel Post([FromBody] AccountModel account)
        {
            var result = _bll.AddAccount(account);
            return result;
        }

    }
}