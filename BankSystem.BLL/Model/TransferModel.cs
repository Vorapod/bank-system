using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
        public decimal Amount { get; set; }
    }
}
