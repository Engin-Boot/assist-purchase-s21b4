using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit.Abstractions;
namespace BackendTests.BackendApiTests
{
    public class DeviceControllerApiTests
    {
        private readonly ITestOutputHelper _output;
        private readonly MockServer _mockServer;
        private static readonly string _url = "http://localhost:5000/api/devices";

        public DeviceControllerApiTests(ITestOutputHelper output)
        {
            _mockServer = new MockServer();
            this._output = output;
        }
       
        [Fact]
        public async Task TestExpectingAllDevicesToBeReturned()
        {
            var response = await _mockServer.Client.GetAsync(_url);
            var jsonString = await response.Content.ReadAsStringAsync();
            List<DataModels.DeviceModel> devices = JsonConvert.DeserializeObject<List<DataModels.DeviceModel>>(jsonString);
            _output.WriteLine(devices.Count.ToString());
            //Assert.True(devices.Count == 7);
            Assert.Contains("VUEMX500", devices[0].id);

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
            var device = new DataModels.DeviceModel()
            {
                id = "VUEMX300",
                name = "IntelliVue MX300",
                measurements = new List<string> { "ECG" },
                resolution = "190 x 1080",
                overview = "some_random_overview",
                weight = 1.6f,
                batterycapacity = "11"

            };
            //ReSharper disable all
            await _mockServer.Client.PostAsync(_url, new StringContent(JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json"));//ReSharper restore all
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
                name = "IntelliVue MX300",
                id = "VUEMX300",
                batterycapacity = "11",
                measurements = new List<string> { "ECG" },
                overview = "Something",
                resolution = "1090 x 1020",
                weight = 1.2f,
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
                name = "IntelliVue X3",
                id = "VUEX3",
                batterycapacity = "11",
                measurements = new List<string> { "ECG", "SPO2", "Respiration" },
                overview = "The IntelliVue X3 combines powerful monitoring with flexible portability in one compact unit. Supplying comprehensive patient information at a glance it can make a real difference when multiple patients and priorities need attention.",
                resolution = "1024 x 420",
                weight = 1.7f,
            };  //ReSharper disable all
            var response = await _mockServer.Client.PutAsync(_url + "/VUEX3", new StringContent(JsonConvert.SerializeObject(updatedState), Encoding.UTF8, "application/json")); //ReSharper restore all
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestExpectingFalseWhenCalledForUpdattionWithInvalidId()
        {
            var updatedstate = new DataModels.DeviceModel()
            {
                name = "IntelliVue X777",
                id = "VUEX777",
                batterycapacity = "7",
                measurements = new List<string> { "ECG", "SPO2", "Respiration" },
                overview = "The IntelliVue X3 combines powerful monitoring with flexible portability in one compact unit. Supplying comprehensive patient information at a glance it can make a real difference when multiple patients and priorities need attention.",
                resolution = "1024 x 420",
                weight = 1.9f,
            }; 
            //ReSharper disable all
            var response = await _mockServer.Client.PutAsync(_url + "/VUEX777", new StringContent(JsonConvert.SerializeObject(updatedstate), Encoding.UTF8, "application/json"));
            var jsonString = await response.Content.ReadAsStringAsync(); 
            Assert.Equal("false", JsonConvert.DeserializeObject<string>(jsonString));
            //ReSharper restore all
        }

    }
}
