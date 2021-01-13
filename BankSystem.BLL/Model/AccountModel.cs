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
        public int CustomerId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int IsActive { get; set; }
        [DataMember]
        public decimal Balance { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public CustomerModel Customer { get; set; }
        [DataMember]
        public virtual ICollection<TransactionModel> Transaction { get; set; }
    }
}
