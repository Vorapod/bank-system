using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BankSystem.BLL.Model
{
    [DataContract]
    public class AccountModel
    {
        [DataMember]
        public string IBANNumber { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int IsActive { get; set; }
        [DataMember]
        public double Balance { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public virtual ICollection<TransactionModel> Transaction { get; set; }
    }
}
