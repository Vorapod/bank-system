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
        public void CreateNewAccount_Shoud_Not_Throw_Error_When_Create_Account_Success()
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
        public void Debit_Should_Not_Throw_Error_When_Deposit_1000_Success()
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

            var result = _bll.Debit(new DepositModel { IBANNumber = ibanNumberExpected, Amount = amountExpected });
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

        [Test]
        public void Debit_Should_Throw_Error_When_Not_Found_The_Account_From_IBANNumber()
        {
            string ibanNumber = "123";

            _mockUnitOfWork.Setup(uof => uof.AccountRepository.GetById(It.IsAny<string>()))
                           .Returns((Account)null);

            _bll = new BankSystemBLL(_mockUnitOfWork.Object, _mapper);

            var ex = Assert.Throws<Exception>(() =>
            {
                _bll.Debit(new DepositModel { IBANNumber = ibanNumber });
            });

            Assert.That(ex.Message, Is.EqualTo($"Account with IBANNumber {ibanNumber} not found."));
        }

        [Test]
        public void Credit_Should_Throw_Error_When_TheMoneyOfSender_Not_Enough()
        {
            double balanceSenderBeforeTransfer = 900;
            double amountExpected = 1000;

            _mockUnitOfWork.Setup(uof => uof.AccountRepository.GetById(It.IsAny<string>()))
                           .Returns(new Account
                           {
                               IBANNumber = "A",
                               Balance = balanceSenderBeforeTransfer
                           });
            _bll = new BankSystemBLL(_mockUnitOfWork.Object, _mapper);

            var ex = Assert.Throws<Exception>(() =>
            {
                _bll.Credit(new TransferModel
                {
                    SenderIBANNumber = "A",
                    ReceiverIBANNumber = "B",
                    Amount = amountExpected
                });
            });

            Assert.That(ex.Message, Is.EqualTo("The money of sender is not enough."));
        }

        [Test]
        public void Credit_Should_Throw_Error_When_NotFound_Sender_Account()
        {
            string senderIBANNumber = "123";
            _mockUnitOfWork.Setup(uof => uof.AccountRepository.GetById(It.IsAny<string>()))
                           .Returns((Account)null);
            _bll = new BankSystemBLL(_mockUnitOfWork.Object, _mapper);

            var ex = Assert.Throws<Exception>(() =>
            {
                _bll.Credit(new TransferModel
                {
                    SenderIBANNumber = senderIBANNumber,
                    ReceiverIBANNumber = "B",
                    Amount = 1000
                });
            });

            Assert.That(ex.Message, Is.EqualTo($"Account with IBANNumber {senderIBANNumber} not found."));
        }

        [Test]
        public void Credit_Should_Throw_Error_When_NotFound_Receiver_Account()
        {
            string receiverIBANNumber = "123";
            _mockUnitOfWork.SetupSequence(uof => uof.AccountRepository.GetById(It.IsAny<string>()))
                           .Returns(new Account
                           {
                               IBANNumber = "A",
                               Balance = 1000
                           })
                           .Returns((Account)null);

            _bll = new BankSystemBLL(_mockUnitOfWork.Object, _mapper);

            var ex = Assert.Throws<Exception>(() =>
            {
                _bll.Credit(new TransferModel
                {
                    SenderIBANNumber = "SenderIBANNumber",
                    ReceiverIBANNumber = receiverIBANNumber,
                    Amount = 1000
                });
            });

            Assert.That(ex.Message, Is.EqualTo($"Account with IBANNumber {receiverIBANNumber} not found."));
        }

        [Test]
        public void Credit_Should_Not_Throw_Error_When_A_Transfer_To_B_With_1000()
        {
            double senderBalanceBeforeTransfer = 1000;
            double recriverBalanceBeforeTransfer = 0;
            string senderIBANNumber = "SenderIBANNumber";
            string receiverIBANNumber = "ReceiverIBANNumber";
            double amountExpected = 1000;

            _mockUnitOfWork.SetupSequence(uof => uof.AccountRepository.GetById(It.IsAny<string>()))
                           .Returns(new Account
                           {
                               IBANNumber = senderIBANNumber,
                               Balance = senderBalanceBeforeTransfer
                           })
                           .Returns(new Account
                           {
                               IBANNumber = receiverIBANNumber,
                               Balance = recriverBalanceBeforeTransfer
                           });

            _bll = new BankSystemBLL(_mockUnitOfWork.Object, _mapper);

            var result = _bll.Credit(new TransferModel
            {
                SenderIBANNumber = senderIBANNumber,
                ReceiverIBANNumber = receiverIBANNumber,
                Amount = amountExpected
            });

        }
        [Test]
        public void Debug()
        {
            BankSystemDbContext db = new BankSystemDbContext();
            UnitOfWork uow = new UnitOfWork(db);

            BankSystemBLL bll = new BankSystemBLL(uow, _mapper);
            //var result = bll.Debit(new DepositModel
            //{
            //    IBANNumber = "ABC",
            //    Amount = 1
            //});
        }
    }
}
