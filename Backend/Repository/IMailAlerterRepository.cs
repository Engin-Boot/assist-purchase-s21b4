using DataModels;

namespace Backend.Repository
{
    public interface IMailAlerterRepository
    {
        void AddCustomer(CustomerModel customer);
        CustomerModel FindCustomer(string customerId);
        void SendEmail(CustomerModel customer);
    }
}