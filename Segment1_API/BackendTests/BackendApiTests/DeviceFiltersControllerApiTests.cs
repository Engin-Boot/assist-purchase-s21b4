
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace BackendTests.BackendApiTests
{
    public class DeviceFiltersControllerApiTests
    {
        private readonly MockServer _mockServer;
        private static readonly string _url = "http://localhost:5000/api/filters";

        public DeviceFiltersControllerApiTests()
        {
            _mockServer = new MockServer();
        }
        //[Fact]
        //public async Task TestExpectingFilteredListOfDevicesWhenCalledWithValidFilters()
        //{
        //    var response = await _mockServer.Client.GetAsync(_url+ "/1.8,1920 x 1080,ECG,7");
        //    var jsonString = await response.Content.ReadAsStringAsync();
        //    List<DataModels.DeviceModel> devices = JsonConvert.DeserializeObject<List<DataModels.DeviceModel>>(jsonString);
        //    Assert.Contains("VUEMX750", devices[0].id);
        //    Assert.Contains("VUEMX500", devices[1].id);
        //}
        //[Fact]
        //public async Task TestExpectingNoDevicesWhenCalledWithInvalidFilters()
        //{
        //    var response = await _mockServer.Client.GetAsync(_url + "/1.8,1920x1080,ECG,13");
        //    var jsonString = await response.Content.ReadAsStringAsync();
        //    List<DataModels.DeviceModel> devices = JsonConvert.DeserializeObject<List<DataModels.DeviceModel>>(jsonString);
        //    Assert.False(devices.Any());
        //}
        [Fact]
        public async Task TestExpectingNullWhenCalledWithInvalidInputStringOfFilters()
        {
            var response = await _mockServer.Client.GetAsync(_url + "/");
            var jsonString = await response.Content.ReadAsStringAsync();
            var devices = JsonConvert.DeserializeObject<List<DataModels.DeviceModel>>(jsonString);
            Assert.Null(devices);

        }
    }
}
