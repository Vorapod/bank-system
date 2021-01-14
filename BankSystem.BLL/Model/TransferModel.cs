using System.Runtime.Serialization;

namespace BankSystem.BLL.Model
{
    [DataContract]
    public class TransferModel
    {
        [DataMember]
        public string SenderIBANNumber { get; set; }
        [DataMember]
        public string ReceiverIBANNumber { get; set; }
        [DataMember]
        public double Amount { get; set; }
    }
}
