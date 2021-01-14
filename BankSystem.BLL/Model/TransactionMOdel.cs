using System;
using System.Runtime.Serialization;

namespace BankSystem.BLL.Model
{
    [DataContract]
    public class TransactionModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SenderIBANNumber { get; set; }
        [DataMember]
        public string ReceiverIBANNumber { get; set; }
        [DataMember]
        public int Type { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public double Fee { get; set; }
        [DataMember]
        public double OutStandingBalance { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        
    }
}
