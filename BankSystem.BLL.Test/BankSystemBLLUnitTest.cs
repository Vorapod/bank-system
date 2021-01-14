using System;
using BankSystem.DAL;
using AutoMapper;
using BankSystem.BLL.Model;
using NUnit.Framework;
using BankSystem.DAL.Interface;
using Moq;

namespace BankSystem.BLL.Test
{
    [TestFixture]
    public class BankSystemBLLUnitTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private BankSystemBLL _bll;
        [SetUp]
        public void SetUp()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AccountModel, Account>().ReverseMap();

            });

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(uof => uof.AccountRepository.Add(It.IsAny<Account>()));
            _mockUnitOfWork.Setup(uof => uof.Commit());

            _bll = new BankSystemBLL(_mockUnitOfWork.Object, new Mapper(mapperConfig));
        }

        [Test]
        public void CreateNewAccount_Shoud_Not_Throw_Error()
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
        public void Debit_Should_Be_Work_InCorrect()
        {
            string IBANNumber = "123456";
            //decimal amount = 1000.00;

            //_bll.Debit(IBANNumber, amount);


        }
    }
}
