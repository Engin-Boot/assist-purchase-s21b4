using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace BackendTests.BackendApiTests
{
    public class MailAlerterControllerApiTests
    {
        private readonly MockServer _mockServer;
        private static readonly string _url = "http://localhost:5000/api/alerter";
        readonly DataModels.CustomerModel _customer = new DataModels.CustomerModel()
        {
            CustomerId = "248",
            CustomerName = "Philip Price",
            CustomerEmailId = "philip@gmail.com",
            CustomerContact = "12345678",
            DeviceId = "VUEX3"
        };
        public MailAlerterControllerApiTests()
        {
            _mockServer = new MockServer();
        }

        [Fact]
        public async Task TestExpectingCustomerToBeAddedIfItDoesNotExist()
        { 
            //ReSharper disable all
            var response = await _mockServer.Client.PostAsync(_url+"/authenticate", new StringContent(JsonConvert.SerializeObject(_customer), Encoding.UTF8, "application/json"));
            var jsonString = await response.Content.ReadAsStringAsync();//ReSharper restore all
            Assert.Equal("true", JsonConvert.DeserializeObject<string>(jsonString));
        }
        [Fact]
        public async Task TestExpectingTrueWhenMailHasBeenSentToRespectiveAddress()
        {
            //ReSharper disable all
            var response = await _mockServer.Client.PostAsync(_url + "/alert", new StringContent(JsonConvert.SerializeObject(_customer), Encoding.UTF8, "application/json"));
            var jsonString = await response.Content.ReadAsStringAsync();//ReSharper restore all
            Assert.Equal("true", JsonConvert.DeserializeObject<string>(jsonString));
        }
        [Fact]
        public async Task TestExpectingTrueForCustomerToBeDeletedWhenItExists()
        {
            //ReSharper disable all
            var response = await _mockServer.Client.DeleteAsync(_url + "/"+_customer.CustomerId);
            var jsonString = await response.Content.ReadAsStringAsync();//ReSharper restore all
            Assert.Equal("true", JsonConvert.DeserializeObject<string>(jsonString));
        }
        [Fact]
        public async Task TestExpectingFalseForCustomerToBeDeletedWhenItDoesNotExist()
        {
            //ReSharper disable all
            var response = await _mockServer.Client.DeleteAsync(_url + "/some_random_id");
            var jsonString = await response.Content.ReadAsStringAsync();//ReSharper restore all
            Assert.Equal("false", JsonConvert.DeserializeObject<string>(jsonString));
        }
    }
}
