using DataModels;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IMailAlerterRepository
    {
        bool AddCustomer(CustomerModel customer);
        bool DeleteCustomerDetails(string id);
        CustomerModel FindCustomer(string customerId);
        bool SendEmail(CustomerModel customer);
        List<CustomerModel> GetAllCustomers();
    }
}