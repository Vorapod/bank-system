using System.Data.Entity;

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
                .Property(e => e.Balance)
                .HasPrecision(18, 0);

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

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Fee)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.OutStandingBalance)
                .HasPrecision(18, 0);
        }
    }
}
