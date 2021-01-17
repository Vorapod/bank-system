using BankSystem.BLL.Model;
using System.Collections.Generic;

namespace BankSystem.BLL.Interface
{
    public interface IBankSystemBLL
    {
        AccountModel AddAccount(AccountModel account);
        AccountModel Deposit(string iBANNumber, DepositModel deposit);
        AccountModel Transfer(TransferModel transfer);
        AccountModel GetAccountById(string iBANNumber);
        IEnumerable<AccountModel> GetAccounts();
    }
}
