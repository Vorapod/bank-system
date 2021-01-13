using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BankSystem.BLL.Model
{
    [DataContract]
    public class CustomerModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public DateTime? CreateDate { get; set; }
        [DataMember]
        public IEnumerable<AccountModel> Account { get; set; }
    }
}
