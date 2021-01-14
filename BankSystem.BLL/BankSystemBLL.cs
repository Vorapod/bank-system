using AutoMapper;
using BankSystem.BLL.Interface;
using BankSystem.BLL.Model;
using BankSystem.DAL;
using BankSystem.DAL.Interface;
using System;
using System.Collections.Generic;

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
                
                //TODO: Get IBANNumber from website with selenium
                //poco.IBANNumber = GetIBANNumber();
                poco.CreatedDate = DateTime.Now;

                _unitOfWork.AccountRepository.Add(poco);
                _unitOfWork.Commit();
                account.IBANNumber = poco.IBANNumber;

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

        private string GetIBANNumber()
        {
            return "Random IBANNumber";
        }
    }
}
