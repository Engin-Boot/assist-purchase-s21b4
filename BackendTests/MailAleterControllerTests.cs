using Xunit;

namespace BackendTests
{
    public class MailAleterControllerTests
    {
        readonly DataModels.CustomerModel _customer = new DataModels.CustomerModel()
        {
            CustomerId = "219",
            CustomerName = "random_name",
            CustomerEmailId = "random_email@gmail.com",
            CustomerPhoneNumber = "12345678",
            DeviceId = "VUEX3"
        };
        Backend.Controllers.MailAlertController _controller = new Backend.Controllers.MailAlertController(new Backend.Repository.MailAlerterRepository());
        [Fact]
        public void TestExpectingNewCustomerToBeAddedIfItDoesnotExist()
        {
            Assert.Equal("New Customer", _controller.CustomerAuthentication(_customer));
        }
        [Fact]
        public void TestExpectingExistingCustomerToBeAuthenticatedIfItExists()
        {
            _controller.CustomerAuthentication(_customer);
            Assert.Equal("Existing Customer", _controller.CustomerAuthentication(_customer));
        }
    }
}
