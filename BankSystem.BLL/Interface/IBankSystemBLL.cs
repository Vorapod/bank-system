using BankSystem.BLL.Model;
using System.Collections.Generic;

namespace BankSystem.BLL.Interface
{
    public interface IBankSystemBLL
    {
        #region [Customer]

        IEnumerable<CustomerModel> GetCustomers(Dictionary<string, string> filters, Dictionary<string, string> sorting);
        CustomerModel GetCustomer(int customerId);
        CustomerModel AddCustomer(CustomerModel customer);
        CustomerModel UpdateCustomer(int customerId, CustomerModel customer);
        void DeleteCustomer(int customerId);
        #endregion
    }
}
