using Backend.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace BackendTests
{
    public class DeviceFiltersRepositoryTests
    {
        readonly DeviceFiltersRepository _deviceFilters = new DeviceFiltersRepository();
        List<DataModels.DeviceModel> _devices = new List<DataModels.DeviceModel>
            {
                new DataModels.DeviceModel
                {
                    Name = "IntelliVue M710",
                    Id = "VUEM710",
                    BatteryCapacity = "7",
                    Measurements = new List<string> { "ECG", "SPO2" },
                    Overview = "Something",
                    Resolution = "1090 x 1020",
                    Weight = 1.2f
                }
            };
        [Fact]
        public void TestExpectingListOfDevicesThatQualifyTheGivenFiltersPassedAsList()
        {
            //filters have to be in order - Weight, Resoulution, Measurements, BatteryCapacity
            List<DataModels.DeviceModel> filtered =_deviceFilters.ApplyAllFilters(new List<string> { "1.8", "1920 x 1080", "SPO2", "7" });
            Assert.Equal("VUEMX750", filtered[0].Id);
            Assert.Equal("VUEMX500", filtered[1].Id);
        }
        [Fact]
        public void TestExpectingEmptyListWhenNoDeviceQualifiesTheFilters()
        {
            List<DataModels.DeviceModel> filtered = _deviceFilters.ApplyAllFilters(new List<string> { "1.8", "1920 x 1080", "SPO2", "9" });
            Assert.False(filtered.Any());
        }
        [Fact]
        public void TestExpectingEmptyListWhenTheFiltersAreNotInCorrectFormat()
        {
            List<DataModels.DeviceModel> filtered = _deviceFilters.ApplyAllFilters(new List<string> { "fan", "1920 x 1080", "SPO2", "9" });
            Assert.False(filtered.Any());
        }
        [Fact]
        public void TestExpectingAllDevicesToBeReturnedWhenInputStringForWeightIsNotInCorrectForm()
        {
            var tempDevices = _deviceFilters.ApplyWeightFilter("",_devices);
            Assert.True(tempDevices.Count == 1);
        }
        [Fact]
        public void TestExpectingAllDevicesToBeReturnedWhenInputStringIsNullOrEmpty()
        {
            var tempDevices = _deviceFilters.ApplyResolutionFilter("", _devices);
            Assert.True(tempDevices.Count == 1);
            tempDevices = _deviceFilters.ApplyMeasurementsFilter("", _devices);
            Assert.True(tempDevices.Count == 1);
            tempDevices = _deviceFilters.ApplyBatteryCapacityFilter("", _devices);
            Assert.True(tempDevices.Count == 1);

        }
        [Fact]
        public void TestExpectingNullWhenInputListIsNull()
        {
            var tempDevices = _deviceFilters.ApplyResolutionFilter("1920 x 1080", null);
            Assert.Null(tempDevices);
            tempDevices = _deviceFilters.ApplyMeasurementsFilter("ECG", null);
            Assert.Null(tempDevices);
            tempDevices = _deviceFilters.ApplyBatteryCapacityFilter("7", null);
            Assert.Null(tempDevices);
        }
    }
}
