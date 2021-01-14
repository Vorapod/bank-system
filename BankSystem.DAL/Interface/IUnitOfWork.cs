using System;

namespace BankSystem.DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> AccountRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }

        /// <summary>
        /// Commits all changes
        /// </summary>
        void Commit();
        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        void RejectChanges();
        new void Dispose();
    }
}
