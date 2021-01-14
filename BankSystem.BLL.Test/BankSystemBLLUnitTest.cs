using System;
using BankSystem.DAL;
using AutoMapper;
using BankSystem.BLL.Model;
using NUnit.Framework;
using BankSystem.DAL.Interface;
using Moq;
using System.Linq;
using System.Collections.Generic;
using BankSystem.BLL.Enum;

namespace BankSystem.BLL.Test
{
    [TestFixture]
    public class BankSystemBLLUnitTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mapper _mapper;
        private BankSystemBLL _bll;
        [SetUp]
        public void SetUp()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AccountModel, Account>().ReverseMap();
                config.CreateMap<TransactionModel, Transaction>().ReverseMap();
            });

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(uof => uof.AccountRepository.Add(It.IsAny<Account>()));
            _mockUnitOfWork.Setup(uof => uof.Commit());
            _mapper = new Mapper(mapperConfig);

            _bll = new BankSystemBLL(_mockUnitOfWork.Object, _mapper);
        }

        [Test]
        public void CreateNewAccount_Shoud_Not_Throw_Error_When_CreateSuccess()
        {
            var expected = new AccountModel
            {
                IBANNumber = "123456",
                Name = "my account",
            };

            var actual = _bll.AddAccount(expected);

            Assert.AreEqual(expected.IBANNumber, actual.IBANNumber);
            Assert.AreEqual(expected.Name, actual.Name);
        }


        [Test]
        public void Debit_Should_Be_Work_Correctly_When_Deposit_1000()
        {
            double amountExpected = 1000;
            double feeExpected = 1.00;
            double balanceExpected = 999.00;
            string ibanNumberExpected = "mockIBANNumber";

            _mockUnitOfWork.Setup(uof => uof.AccountRepository.GetById(It.IsAny<string>()))
                           .Returns(new Account
                           {
                               IBANNumber = ibanNumberExpected,
                               Balance = 0
                           });

            _bll = new BankSystemBLL(_mockUnitOfWork.Object, _mapper);

            string IBANNumber = "123456";
            double amount = 1000;
            var result = _bll.Debit(IBANNumber, amount);
            List<TransactionModel> tranasctions = result.Transaction.ToList();

            // Assert Account
            Assert.AreEqual(balanceExpected, result.Balance);
            Assert.AreEqual(1, result.Transaction.Count);

            // Assert Transaction
            Assert.AreEqual(amountExpected, tranasctions[0].Amount);
            Assert.AreEqual(feeExpected, tranasctions[0].Fee);
            Assert.AreEqual(balanceExpected, tranasctions[0].OutStandingBalance);
            Assert.AreEqual(ibanNumberExpected, tranasctions[0].SenderIBANNumber);
            Assert.AreEqual(ibanNumberExpected, tranasctions[0].ReceiverIBANNumber);
            Assert.AreEqual((int)TransactionType.Debit, tranasctions[0].Type);
        }

        //[Test]
        //public void Debug()
        //{
        //    BankSystemDbContext db = new BankSystemDbContext();
        //    UnitOfWork uow = new UnitOfWork(db);

        //    BankSystemBLL bll = new BankSystemBLL(uow, _mapper);
        //    var result = bll.Debit("S9SKXSKPKKJPZBXVDU", 1000);
        //}
    }
}
