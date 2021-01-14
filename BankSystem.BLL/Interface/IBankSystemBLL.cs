using BankSystem.BLL.Model;
using System.Collections.Generic;

namespace BankSystem.BLL.Interface
{
    public interface IBankSystemBLL
    {
        AccountModel AddAccount(AccountModel account);
        AccountModel Debit(DepositModel deposit);
        AccountModel Credit(TransferModel transfer);
    }
}
