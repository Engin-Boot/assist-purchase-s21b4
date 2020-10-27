﻿using Xunit;

namespace BackendTests
{
    public class MailAleterControllerTests
    {
        readonly DataModels.CustomerModel _customer = new DataModels.CustomerModel()
        {
            CustomerId = "223",
            CustomerName = "Gideon",
            CustomerEmailId = "gideon@gmail.com",
            CustomerContact = "12345678",
            DeviceId = "VUEX3"
        };
        readonly Backend.Controllers.MailAlertController _controller = new Backend.Controllers.MailAlertController(new Backend.Repository.MailAlerterRepository(@"C:\Users\Ekta\Desktop\assist-purchase-s21b4\Segment1_API\BackendTests\TestCustomers.csv"));
        [Fact]
        public void TestExpectingNewCustomerToBeAddedIfItDoesnotExist()
        {
            Assert.True(_controller.CustomerAuthentication(_customer));
            _controller.Delete(_customer.CustomerId);
        }
        [Fact]
        public void TestExpectingExistingCustomerToBeAuthenticatedIfItExists()
        {
            _controller.CustomerAuthentication(_customer);
            Assert.True(_controller.CustomerAuthentication(_customer));
            _controller.Delete(_customer.CustomerId);
        }
        [Fact]
        public void TestExpectingExistingCustomerToBeDeletedFromFileWhenCalledWithValidId()
        {
            _controller.CustomerAuthentication(_customer);
            Assert.True(_controller.Delete(_customer.CustomerId));

        }
    }
}
