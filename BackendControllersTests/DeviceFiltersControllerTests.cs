using RestSharp;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using RestSharp.Serialization.Json;

namespace BackendControllersTests
{
    public class DeviceFiltersControllerTests
    {
        [Fact]
        public void TestExpectingListOfFilteredDevicesWhenCalledWithListOfFilters()
        {
            RestClient client = new RestClient("http://localhost:5000/api/filters");
            string jsonData = "1.8,1920 x 1080,ECG,7";
            RestRequest request = new RestRequest("/" + jsonData, Method.GET);
            IRestResponse response = client.Execute(request);

            var deserializer = new JsonDeserializer();
            var data = deserializer.Deserialize<List<DataModels.DeviceModel>>(response);
            Assert.Equal("VUEMX750", data[0].Id);
            Assert.Equal("VUEMX500", data[1].Id);
        }
        [Fact]
        public void TestExpectingEmptyListWhenInputStringIsInvalid()
        {
            RestClient client = new RestClient("http://localhost:5000/api/filters");
            string jsonData = "1.8,1920 x 1080,ECG,12";
            RestRequest request = new RestRequest("/" + jsonData, Method.GET);
            IRestResponse response = client.Execute(request);
            var deserializer = new JsonDeserializer();
            var data = deserializer.Deserialize<List<DataModels.DeviceModel>>(response);
            Assert.False(data.Any());
        }
    }
}
