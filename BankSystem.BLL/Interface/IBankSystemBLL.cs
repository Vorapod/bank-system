using BankSystem.BLL.Model;
using System.Collections.Generic;

namespace BankSystem.BLL.Interface
{
    public interface IBankSystemBLL
    {
        #region [Account]
        AccountModel AddAccount(AccountModel account);
        void Debit(string IBANNumber, decimal amount);
        #endregion
    }
}
