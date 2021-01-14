using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.BLL.Model
{
    [DataContract]
    public class DepositModel
    {
        [DataMember]
        public string IBANNumber { get; set; }
        [DataMember]
        public double Amount { get; set; }
    }
}
