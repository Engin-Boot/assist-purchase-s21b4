
using Xunit;
using DataModels;
using Backend.Repository;

namespace BackendTests
{
    public class MailAleterRepositoryTests
    {
        readonly MailAlerterRepository _mailAlerterRepository = new MailAlerterRepository();
        [Fact]
        public void TestExpectingCustomerToBeAddedIfItDoesnotExist()
        {
            CustomerModel customer = new CustomerModel()
            {
                CustomerId = "218",
                CustomerName = "Tyrell",
                CustomerEmailId = "tyrell@example.com",
                CustomerPhoneNumber = "1234567890",
                DeviceId = "VUEMX750"
            };

            Assert.Null(_mailAlerterRepository.FindCustomer(customer.CustomerId));
            _mailAlerterRepository.AddCustomer(customer);
            Assert.Equal("Tyrell",_mailAlerterRepository.FindCustomer(customer.CustomerId).CustomerName);

        }
        [Fact]
        public void TestExpectingNullWhenCustomerDoesNotExist()
        {
            CustomerModel customer = new CustomerModel()
            {
                CustomerId = "219",
                CustomerName = "Tyrell",
                CustomerEmailId = "tyrell@example.com",
                CustomerPhoneNumber = "1234567890",
                DeviceId = "VUEMX750"
            };

            Assert.Null(_mailAlerterRepository.FindCustomer(customer.CustomerId));
        }
    }
}
