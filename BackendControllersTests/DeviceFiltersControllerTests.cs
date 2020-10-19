using RestSharp;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using RestSharp.Serialization.Json;

namespace BackendControllersTests
{
    public class DeviceFiltersControllerTests
    {
        readonly Backend.Controllers.DeviceFiltersController _controller = new Backend.Controllers.DeviceFiltersController(new Backend.Repository.DeviceFiltersRepository());
        [Fact]
        public void TestExpectingListOfFilteredDevicesWhenCalledWithListOfFilters()
        {
            var filteredDevices = _controller.Get("1.8,1920 x 1080,ECG,7");
            Assert.Equal("VUEMX750", filteredDevices[0].Id);
            Assert.Equal("VUEMX500", filteredDevices[1].Id);
        }
        [Fact]
        public void TestExpectingEmptyListWhenNoDeviceQualifiesTheFilters()
        {
            var filteredDevices = _controller.Get("1.6,1920 x 1080,ECG,12");
            Assert.False(filteredDevices.Any());
        }
        [Fact]
        public void TestExpectingNullWhenInputStringOfFiltersIsNotInCorrectFormat()
        {
            var filteredDevices = _controller.Get("");
            Assert.Null(filteredDevices);
        }

    }
}
