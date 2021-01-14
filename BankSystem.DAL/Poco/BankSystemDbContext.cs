using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BankSystem.DAL
{
    public partial class BankSystemDbContext : DbContext
    {
        public BankSystemDbContext()
            : base("name=BankSystemDbContext")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.IBANNumber)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Transaction)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.SenderIBANNumber)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.SenderIBANNumber)
                .IsFixedLength();

            modelBuilder.Entity<Transaction>()
                .Property(e => e.ReceiverIBANNumber)
                .IsFixedLength();
        }
    }
}
