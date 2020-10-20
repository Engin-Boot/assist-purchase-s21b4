using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BackendTests.BackendApiTests
{
    public class DeviceControllerApiTests
    {
        private readonly MockServer _mockServer;
        private static readonly string _url = "http://localhost:5000/api/devices";

        public DeviceControllerApiTests()
        {
            _mockServer = new MockServer();
        }

        [Fact]
        public async Task TestExpectingAllDevicesToBeReturned()
        {
            var response = await _mockServer.Client.GetAsync(_url);
            var jsonString = await response.Content.ReadAsStringAsync();
            List<DataModels.DeviceModel> devices = JsonConvert.DeserializeObject<List<DataModels.DeviceModel>>(jsonString);
            Assert.Contains("VUEMX750", devices[0].Id);
            Assert.Contains("VUEMX500", devices[1].Id);
        }

        [Fact]
        public async Task TestExpectingADeviceToBeReturnedWhenCalledWithValidId()
        {
            var response = await _mockServer.Client.GetAsync(_url + "/VUEX3");
            var returnString = await response.Content.ReadAsStringAsync();
            Assert.Contains("IntelliVue X3", returnString);
        }

        [Fact]
        public async Task TestExpectingNullWhenCalledWithInvalidIdForGetMethod()
        {
            var response = await _mockServer.Client.GetAsync(_url + "/VUEX8");
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Null(JsonConvert.DeserializeObject<string>(jsonString));
        }

        [Fact]
        public async Task TestExpectingTrueWhenCalledWithValidIdToDeleteMethod()
        {
            var response = await _mockServer.Client.DeleteAsync(_url + "/VUEMX300");
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Equal("true",JsonConvert.DeserializeObject<string>(jsonString));
        }


        [Fact]
        public async Task TestExpectingFalseWhenCalledWithInvalidIdToDeleteMethod()
        {
            var response = await _mockServer.Client.DeleteAsync(_url + "/VUE777");
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Equal("false", JsonConvert.DeserializeObject<string>(jsonString));
        }

        [Fact]
        public async Task TestExpectingTrueWhenNewDeviceIsAddedSuccessfully()
        {
            var newDevice = new DataModels.DeviceModel()
            {
                Name = "IntelliVue MX300",
                Id = "VUEMX300",
                BatteryCapacity = "11",
                Measurements = new List<string> { "ECG" },
                Overview = "Something",
                Resolution = "1090 x 1020",
                Weight = 1.2f,
            };
            //ReSharper disable all
            var response = await _mockServer.Client.PostAsync(_url , new StringContent(JsonConvert.SerializeObject(newDevice), Encoding.UTF8, "application/json")); //ReSharper restore all
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Equal("true", JsonConvert.DeserializeObject<string>(jsonString));
        }

        [Fact]
        public async Task TestExpectingTrueWhenDeviceHasBeenUpdatedWhenCalledWithValidId()
        {
            var updatedState = new DataModels.DeviceModel()
            {
                Name = "IntelliVue X3",
                Id = "VUEX3",
                BatteryCapacity = "7",
                Measurements = new List<string> { "ECG", "SPO2", "Respiration" },
                Overview = "The IntelliVue X3 combines powerful monitoring with flexible portability in one compact unit. Supplying comprehensive patient information at a glance it can make a real difference when multiple patients and priorities need attention.",
                Resolution = "1024 x 420",
                Weight = 1.9f,
            };  //ReSharper disable all
            var response = await _mockServer.Client.PutAsync(_url + "/VUEX3", new StringContent(JsonConvert.SerializeObject(updatedState), Encoding.UTF8, "application/json")); //ReSharper restore all
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestExpectingFalseWhenCalledForUpdattionWithInvalidId()
        {
            var updatedstate = new DataModels.DeviceModel()
            {
                Name = "IntelliVue X777",
                Id = "VUEX777",
                BatteryCapacity = "7",
                Measurements = new List<string> { "ECG", "SPO2", "Respiration" },
                Overview = "The IntelliVue X3 combines powerful monitoring with flexible portability in one compact unit. Supplying comprehensive patient information at a glance it can make a real difference when multiple patients and priorities need attention.",
                Resolution = "1024 x 420",
                Weight = 1.9f,
            }; 
            var response = await _mockServer.Client.PutAsync(_url + "/VUEX777", new StringContent(JsonConvert.SerializeObject(updatedstate), Encoding.UTF8, "application/json"));
            var jsonString = await response.Content.ReadAsStringAsync();
            Assert.Equal("false", JsonConvert.DeserializeObject<string>(jsonString));
        }

    }
}
