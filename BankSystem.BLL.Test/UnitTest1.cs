using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankSystem.DAL;
using AutoMapper;
using BankSystem.BLL.Model;

namespace BankSystem.BLL.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            BankSystemDbContext db = new BankSystemDbContext();
            UnitOfWork uow = new UnitOfWork(db);
            MapperConfiguration mp = new MapperConfiguration(config =>
            {
                config.CreateMap<AccountModel, Account>().ReverseMap();

            });

            var mapper = new Mapper(mp);

            BankSystemBLL bll = new BankSystemBLL(uow, mapper);
            bll.AddAccount(new AccountModel
            {
                Name = "my account"
            });
        }
    }
}
