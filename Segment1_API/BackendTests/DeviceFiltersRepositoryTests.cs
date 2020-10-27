using Backend.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace BackendTests
{
    public class DeviceFiltersRepositoryTests
    {
        //readonly DeviceFiltersRepository _deviceFilters = new DeviceFiltersRepository();
        //readonly List<DataModels.DeviceModel> _devices = new List<DataModels.DeviceModel>
        //    {
        //        new DataModels.DeviceModel
        //        {
        //            name = "IntelliVue M710",
        //            id = "VUEM710",
        //            batterycapacity = "7",
        //            measurements = new List<string> { "ECG", "SPO2" },
        //            overview = "Something",
        //            resolution = "1090 x 1020",
        //            weight = 1.2f
        //        }
        //    };
        //[Fact]
        //public void TestExpectingListOfDevicesThatQualifyTheGivenFiltersPassedAsList()
        //{
        //    //filters have to be in order - Weight, Resoulution, Measurements, BatteryCapacity
        //    List<DataModels.DeviceModel> filtered =_deviceFilters.ApplyAllFilters(new List<string> { "1.8", "1920 x 1080", "SPO2", "7" });
        //    Assert.Equal("VUEMX750", filtered[0].id);
        //    Assert.Equal("VUEMX500", filtered[1].id);
        //}
        //[Fact]
        //public void TestExpectingEmptyListWhenNoDeviceQualifiesTheFilters()
        //{
        //    List<DataModels.DeviceModel> filtered = _deviceFilters.ApplyAllFilters(new List<string> { "1.8", "1920 x 1080", "SPO2", "9" });
        //    Assert.False(filtered.Any());
        //}
        //[Fact]
        //public void TestExpectingEmptyListWhenTheFiltersAreNotInCorrectFormat()
        //{
        //    List<DataModels.DeviceModel> filtered = _deviceFilters.ApplyAllFilters(new List<string> { "fan", "1920 x 1080", "SPO2", "9" });
        //    Assert.False(filtered.Any());
        //}
        //[Fact]
        //public void TestExpectingAllDevicesToBeReturnedWhenInputStringForWeightIsNotInCorrectForm()
        //{
        //    var tempDevices = _deviceFilters.ApplyWeightFilter("",_devices);
        //    Assert.True(tempDevices.Count == 1);
        //}
        //[Fact]
        //public void TestExpectingAllDevicesToBeReturnedWhenInputStringIsNullOrEmpty()
        //{
        //    var tempDevices = _deviceFilters.ApplyResolutionFilter("", _devices);
        //    Assert.True(tempDevices.Count == 1);
        //    tempDevices = _deviceFilters.ApplyMeasurementsFilter("", _devices);
        //    Assert.True(tempDevices.Count == 1);
        //    tempDevices = _deviceFilters.ApplyBatteryCapacityFilter("", _devices);
        //    Assert.True(tempDevices.Count == 1);

        //}
        //[Fact]
        //public void TestExpectingNullWhenInputListIsNull()
        //{
        //    var tempDevices = _deviceFilters.ApplyResolutionFilter("1920 x 1080", null);
        //    Assert.Null(tempDevices);
        //    tempDevices = _deviceFilters.ApplyMeasurementsFilter("ECG", null);
        //    Assert.Null(tempDevices);
        //    tempDevices = _deviceFilters.ApplyBatteryCapacityFilter("7", null);
        //    Assert.Null(tempDevices);
        //}
    }
}
