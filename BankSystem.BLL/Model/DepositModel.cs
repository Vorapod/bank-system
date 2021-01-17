using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BankSystem.BLL.Model
{
    [DataContract]
    public class DepositModel
    {
        [DataMember]
        [Required]
        public double Amount { get; set; }
    }
}
