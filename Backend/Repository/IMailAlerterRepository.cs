using DataModels;

namespace Backend.Repository
{
    public interface IMailAlerterRepository
    {
        void AddCustomer(CustomerModel customer);
        bool DeleteCustomerDetails(string id);
        CustomerModel FindCustomer(string customerId);
        bool SendEmail(CustomerModel customer);
    }
}