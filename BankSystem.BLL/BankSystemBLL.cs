using AutoMapper;
using BankSystem.BLL.Enum;
using BankSystem.BLL.Interface;
using BankSystem.BLL.Model;
using BankSystem.DAL;
using BankSystem.DAL.Interface;
using System;
using System.Linq;

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
                
                //TODO: Get IBANNumber from website by selenium
                if(String.IsNullOrEmpty(account.IBANNumber))
                poco.IBANNumber = GetIBANNumber();

                //TODO: Find the way to set defalut value
                poco.CreatedDate = DateTime.Now;
                poco.IsActive = true;

                _unitOfWork.AccountRepository.Add(poco);
                _unitOfWork.Commit();
                account.IBANNumber = poco.IBANNumber;
                account.CreatedDate = poco.CreatedDate;

                return account;
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

        public AccountModel Debit(string IBANNumber, double amount)
        {
            try
            {
                Account account = _unitOfWork.AccountRepository.GetById(IBANNumber);

                if (account == null)
                    throw new Exception("Not found an account");

                // TODO: Move hard code to config
                double fee = (amount * 0.1) / 100;
                account.Balance += amount - fee;

                account.Transaction.Add(new Transaction
                {
                    SenderIBANNumber = IBANNumber,
                    ReceiverIBANNumber = IBANNumber,
                    Type = (int)TransactionType.Debit,
                    Amount = amount,
                    Fee = fee,
                    OutStandingBalance = account.Balance,
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


        #region Private method
        private string GetIBANNumber()
        {
            return RandomString(18);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

      
        #endregion

    }
}
