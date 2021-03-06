﻿using AutoMapper;
using BankSystem.BLL.Enum;
using BankSystem.BLL.Interface;
using BankSystem.BLL.Model;
using BankSystem.DAL;
using BankSystem.DAL.Interface;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Data;

namespace BankSystem.BLL
{
    public class BankSystemBLL : IBankSystemBLL
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BankSystemBLL(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public AccountModel AddAccount(AccountModel account)
        {
            try
            {
                Account poco = _mapper.Map<AccountModel, Account>(account);

                poco.IBANNumber = GetIBANNumber();

                //TODO: Find the way to set defalut value
                poco.CreatedDate = DateTime.Now;
                poco.IsActive = true;

                _unitOfWork.AccountRepository.Add(poco);
                _unitOfWork.Commit();

                return _mapper.Map<Account, AccountModel>(poco); ;
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                throw ex;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public AccountModel Deposit(string iBANNumber, DepositModel deposit)
        {
            try
            {
                Account account = GetAccount(iBANNumber);

                // TODO: Move hard code to config
                double fee = (deposit.Amount * 0.1) / 100;
                account.CurrentBalance += deposit.Amount - fee;

                account.Transactions.Add(new Transaction
                {
                    Type = (int)TransactionType.Deposit,
                    StatementType = (int)TransactionType.Deposit,
                    Amount = deposit.Amount,
                    Fee = fee,
                    OutStandingBalance = account.CurrentBalance,
                    CreatedDate = DateTime.Now
                });

                _unitOfWork.Commit();

                return _mapper.Map<Account, AccountModel>(account);
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                throw ex;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public AccountModel Transfer(TransferModel transfer)
        {
            try
            {
                // Get sender's account
                Account sender = GetAccount(transfer.SenderIBANNumber);
                if (sender.CurrentBalance < transfer.Amount)
                    throw new Exception("The money of sender is not enough.");
                // Get receiver's account
                Account receiver = GetAccount(transfer.ReceiverIBANNumber);

                // Sender Transaction
                sender.CurrentBalance -= transfer.Amount;
                sender.Transactions.Add(new Transaction
                {
                    IBANNumber = transfer.SenderIBANNumber,
                    Type = (int)TransactionType.Transfer,
                    StatementType = (int)Enum.StatementType.Credit,
                    Amount = transfer.Amount * -1,
                    Fee = 0,
                    OutStandingBalance = sender.CurrentBalance,
                    PartnerIBANNuberRef = transfer.ReceiverIBANNumber,
                    CreatedDate = DateTime.Now
                });

                // Receiver Transaction
                receiver.CurrentBalance += transfer.Amount;
                receiver.Transactions.Add(new Transaction
                {
                    IBANNumber = transfer.ReceiverIBANNumber,
                    Type = (int)TransactionType.Transfer,
                    StatementType = (int)Enum.StatementType.Debit,
                    Amount = transfer.Amount,
                    Fee = 0,
                    OutStandingBalance = receiver.CurrentBalance,
                    PartnerIBANNuberRef = transfer.SenderIBANNumber,
                    CreatedDate = DateTime.Now
                });

                _unitOfWork.Commit();

                return _mapper.Map<Account, AccountModel>(sender);
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                throw ex;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public AccountModel GetAccountById(string iBANNumber)
        {
            try
            {
                if (String.IsNullOrEmpty(iBANNumber))
                    throw new ArgumentNullException(iBANNumber);
                return _mapper.Map<Account, AccountModel>(GetAccount(iBANNumber));
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                throw ex;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
        public IEnumerable<AccountModel> GetAccounts()
        {
            try
            {
                return _mapper.Map<IEnumerable<Account>, IEnumerable<AccountModel>>(_unitOfWork.AccountRepository.GetAll());
            }
            catch (Exception ex)
            {
                _unitOfWork.RejectChanges();
                throw ex;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
        #region Private method
        private string GetIBANNumber()
        {
            string iBANNumber = string.Empty;
            using (var driver = new ChromeDriver())
            {
                // Go to the home page
                driver.Navigate().GoToUrl("http://randomiban.com/?country=Netherlands");
                iBANNumber = driver.FindElementById("demo").Text;
            }
            return iBANNumber;
        }

       

        private Account GetAccount(string iBANNumber)
        {
            Account account = _unitOfWork.AccountRepository.GetById(iBANNumber);

            if (account == null)
                throw new ObjectNotFoundException($"Account with IBANNumber {iBANNumber} not found.");

            return account;
        }

      
        #endregion

    }
}
