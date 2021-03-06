﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BankSystem.BLL.Model
{
    [DataContract]
    public class AccountModel
    {
        [DataMember]
        public string IBANNumber { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        public int IsActive { get; set; }
        [DataMember]
        public double CurrentBalance { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public virtual ICollection<TransactionModel> Transactions { get; set; }
    }
}
