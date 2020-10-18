using Backend.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace BackendTests
{
    public class DeviceFiltersRepositoryTests
    {
        [Fact]
        public void TestExpectingListOfDevicesThatQualifyTheGivenFiltersPassedAsList()
        {
            //filters have to be in order - Weight, Resoulution, Measurements, BatteryCapacity
            DeviceFiltersRepository deviceFilters = new DeviceFiltersRepository();
            List<DataModels.DeviceModel> filtered =deviceFilters.ApplyAllFilters(new List<string> { "1.8", "1920 x 1080", "SPO2", "7" });
            Assert.Equal("VUEMX750", filtered[0].Id);
            Assert.Equal("VUEMX500", filtered[1].Id);
        }
        [Fact]
        public void TestExpectingEmptyListWhenNoDeviceQualifiesTheFilters()
        {
            DeviceFiltersRepository deviceFilters = new DeviceFiltersRepository();
            List<DataModels.DeviceModel> filtered = deviceFilters.ApplyAllFilters(new List<string> { "1.8", "1920 x 1080", "SPO2", "9" });
            Assert.False(filtered.Any());
        }
        [Fact]
        public void TestExpectingEmptyListWhenTheFiltersAreNotInCorrectFormat()
        {
            DeviceFiltersRepository deviceFilters = new DeviceFiltersRepository();
            List<DataModels.DeviceModel> filtered = deviceFilters.ApplyAllFilters(new List<string> { "fan", "1920 x 1080", "SPO2", "9" });
            Assert.False(filtered.Any());
        }
    }
}
