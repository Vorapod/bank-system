namespace BankSystem.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BankSystemContext : DbContext
    {
        public BankSystemContext()
            : base("name=BankSystemDbContext")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.IBANNumber)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .Property(e => e.Balance)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Transaction)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.SenderIBANNumber)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Account)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.SenderIBANNumber)
                .IsFixedLength();

            modelBuilder.Entity<Transaction>()
                .Property(e => e.ReceiverIBANNumber)
                .IsFixedLength();

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Fee)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Net)
                .HasPrecision(18, 0);
        }
    }
}
