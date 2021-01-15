using System.Runtime.Serialization;

namespace BankSystem.BLL.Model
{
    [DataContract]
    public class DepositModel
    {
        [DataMember]
        public double Amount { get; set; }
    }
}
