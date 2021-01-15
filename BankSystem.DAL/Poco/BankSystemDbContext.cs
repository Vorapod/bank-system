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

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.IBANNumber)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.IBANNumber)
                .IsFixedLength();

            modelBuilder.Entity<Transaction>()
                .Property(e => e.PartnerIBANNuberRef)
                .IsFixedLength();
        }
    }
}
