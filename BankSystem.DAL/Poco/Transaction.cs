namespace BankSystem.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction")]
    public partial class Transaction
    {
        public int Id { get; set; }

        [Required]
        [StringLength(18)]
        public string SenderIBANNumber { get; set; }

        [Required]
        [StringLength(18)]
        public string ReceiverIBANNumber { get; set; }

        public int Type { get; set; }

        public double Amount { get; set; }

        public double Fee { get; set; }

        public double OutStandingBalance { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Account Account { get; set; }
    }
}
