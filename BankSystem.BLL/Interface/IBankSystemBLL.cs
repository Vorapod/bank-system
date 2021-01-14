﻿using BankSystem.BLL.Model;
using System.Collections.Generic;

namespace BankSystem.BLL.Interface
{
    public interface IBankSystemBLL
    {
        #region [Account]
        AccountModel AddAccount(AccountModel account);
        AccountModel Debit(string IBANNumber, double amount);
        #endregion
    }
}
