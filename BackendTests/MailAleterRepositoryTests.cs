
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
            Assert.Equal("tyrell@example.com", _mailAlerterRepository.FindCustomer(customer.CustomerId).CustomerEmailId);
            Assert.Equal("1234567890", _mailAlerterRepository.FindCustomer(customer.CustomerId).CustomerPhoneNumber);

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
        [Fact]
        public void TestExpectingMailSentWhenCalledWithCustomerBody()
        {
            CustomerModel customer = new CustomerModel()
            {
                CustomerId = "307",
                CustomerName = "ElliotAlderson",
                CustomerEmailId = "Elliot@example.com",
                CustomerPhoneNumber = "1234567890",
                DeviceId = "VUEMX750"
            };

            _mailAlerterRepository.SendEmail(customer);
        }
    }
}
