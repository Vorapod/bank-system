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
            throw new NotImplementedException();
        }

        public CustomerModel AddCustomer(CustomerModel customer)
        {
            try
            {
                Customer poco = _mapper.Map<CustomerModel, Customer>(customer);
                _unitOfWork.CustomerRepository.Add(poco);
                _unitOfWork.Commit();
                customer.Id = poco.Id;
                return customer;
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

        public void DeleteAccount(string IBANNumber)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        //public void Deposite(TransferModel transfer)
        //{
        //    // Get account by IBANNumber
        //    var account = new AccountModel();

        //    // Calculate Fee
        //    double fee = (transfer.Amount * 0.10) / 100;
        //    double net = transfer.Amount - fee;

        //    // Update balance
        //    // account.Balance + net



        //    // InsertTransaction
        //    //var transaction 




        //}

        public IEnumerable<AccountModel> GetAccounts(int customerId)
        {
            // Get account from DAL
            return new List<AccountModel>();
        }

        public CustomerModel GetCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerModel> GetCustomers(Dictionary<string, string> filters, Dictionary<string, string> sorting)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionModel> GetTransactions(string IBANNumber)
        {
            throw new NotImplementedException();
        }

        //public void Transfer(TransferModel transfer)
        //{
        //    throw new NotImplementedException();
        //}

        public CustomerModel UpdateCustomer(int customerId, CustomerModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
