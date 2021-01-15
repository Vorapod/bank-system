using BankSystem.BLL.Model;
using System.Collections.Generic;

namespace BankSystem.BLL.Interface
{
    public interface IBankSystemBLL
    {
        AccountModel AddAccount(AccountModel account);
        AccountModel Deposit(DepositModel deposit);
        AccountModel Transfer(TransferModel transfer);

        List<AccountModel> GetAccounts();
    }
}
