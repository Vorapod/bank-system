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
        public decimal Amount { get; set; }
        [DataMember]
        public decimal Fee { get; set; }
        [DataMember]
        public decimal OutStandingBalance { get; set; }
        [DataMember]
        public decimal CreatedDate { get; set; }
        [DataMember]
        public AccountModel Account { get; set; }
    }
}
