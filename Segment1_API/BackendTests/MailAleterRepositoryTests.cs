
using Xunit;
using DataModels;
using Backend.Repository;

namespace BackendTests
{
    public class MailAleterRepositoryTests
    {
        readonly IMailAlerterRepository _mailAlerterRepository = new MailAlerterRepository(@"C:\Users\Ekta\Desktop\assist-purchase-s21b4\Segment1_API\BackendTests\TestCustomers.csv");
        [Fact]
        public void TestExpectingCustomerToBeAddedIfItDoesnotExist()
        {
            CustomerModel customer = new CustomerModel()
            {
                CustomerId = "212",
                CustomerName = "Tyrell",
                CustomerEmailId = "tyrell@example.com",
                CustomerContact = "1234567890",
                DeviceId = "VUEMX750"
            };

            Assert.Null(_mailAlerterRepository.FindCustomer(customer.CustomerId));
            _mailAlerterRepository.AddCustomer(customer);
            Assert.Equal("Tyrell",_mailAlerterRepository.FindCustomer(customer.CustomerId).CustomerName);
            Assert.Equal("tyrell@example.com", _mailAlerterRepository.FindCustomer(customer.CustomerId).CustomerEmailId);
            Assert.Equal("1234567890", _mailAlerterRepository.FindCustomer(customer.CustomerId).CustomerContact);
            _mailAlerterRepository.DeleteCustomerDetails(customer.CustomerId);

        }
        [Fact]
        public void TestExpectingNullWhenCustomerDoesNotExist()
        {
            CustomerModel customer = new CustomerModel()
            {
                CustomerId = "221",
                CustomerName = "Tyrell",
                CustomerEmailId = "tyrell@example.com",
                CustomerContact = "1234567890",
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
                CustomerContact = "1234567890",
                DeviceId = "VUEMX750"
            };

            Assert.True(_mailAlerterRepository.SendEmail(customer));
        }
    }
}
