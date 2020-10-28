using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Xunit;

namespace BackendTests
{
    public class DeviceFiltersControllerTests
    {
        //private readonly ITestOutputHelper output;

        readonly Backend.Controllers.DeviceFiltersController _controller = new Backend.Controllers.DeviceFiltersController(new Backend.Repository.DeviceFiltersRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Segment1_API\BackendTests\TestDevices2.csv"));
        //readonly Backend.Controllers.DeviceFiltersController _controller = new Backend.Controllers.DeviceFiltersController(new Backend.Repository.DeviceFiltersRepository(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy\Segment1_API\BackendTests\TestDevices2.csv"));
        //public DeviceFiltersControllerTests(ITestOutputHelper output)
        //{
        //    this.output = output;
        //}
        [Fact]
        public void WhenGetAllMeasurementsThenGetExpectedMeasurements()
        {
            List<string> measurements= ((JsonResult)_controller.Get()).Value as List<string>;
            //output.WriteLine(measurements.Count.ToString());
            //            ECG SPO2 Respiration NiBP
            Assert.True(measurements.Count == 5);
            Assert.True(measurements[0].Equals("ECG"));
            Assert.True(measurements[1].Equals("SPO2"));
            Assert.True(measurements[2].Equals("Respiration"));
            Assert.True(measurements[3].Equals("NiBP"));
            Assert.True(measurements[4].Equals("Pulse"));


        }
        [Fact]
        public void WhenWantFiteredDevicesThenGetFilteredDevices()
        {
            var filterPreferences = new DataModels.FilterDataModel
            {
                measurements = new List<string>{"ECG", "SPO2", "Respiration", "NiBP"},
                weight = new List<string>(),
                resolution = new List<string>(),
                batterycapacity = new List<string> { "7"}
            };
            List<DataModels.DeviceModel> viewModel = ((JsonResult)_controller.Put(filterPreferences)).Value as List<DataModels.DeviceModel>;
           
            Assert.True(viewModel[0].id.Equals("VUEMX750"));
            Assert.True(viewModel.Count == 2);
        }
        [Fact]
        public void WhenWantFiteredDevicesThenGetFilteredDevices1()
        {
            
            var filterPreferences = new DataModels.FilterDataModel
            {
                measurements = new List<string> { "ECG", "SPO2", "Respiration", "NiBP", "Pulse" },
                weight = new List<string> { "1-10"},
                resolution = new List<string> { "1920x1080"},
                batterycapacity = new List<string> { "7"}
            };
            List<DataModels.DeviceModel> viewModel = ((JsonResult)_controller.Put(filterPreferences)).Value as List<DataModels.DeviceModel>;
            Assert.True(viewModel.Count == 1);
            Assert.True(viewModel[0].id.Equals("VUEMX500"));
        }
        [Fact]
        public void WhenWantFiteredDevicesThenGetFilteredDevices2()
        {

            var filterPreferences = new DataModels.FilterDataModel
            {
                measurements = new List<string> { "ECG"},
                weight = new List<string> { "1-10" },
                resolution = new List<string> { "1920x1080","670x480" },
                batterycapacity = new List<string> { "7" ,"8"}
            };
            List<DataModels.DeviceModel> viewModel = ((JsonResult)_controller.Put(filterPreferences)).Value as List<DataModels.DeviceModel>;
            Assert.True(viewModel.Count == 2);
            
        }
        [Fact]
        public void WhenFilterIsEmptyThenGetAllDevices()
        {
            var filterPreferences = new DataModels.FilterDataModel
            {
                measurements = new List<string>(),
                weight = new List<string>(),
                resolution = new List<string>(),
                batterycapacity = new List<string>()
            };
            List<DataModels.DeviceModel> viewModel = ((JsonResult)_controller.Put(filterPreferences)).Value as List<DataModels.DeviceModel>;
            Assert.True(viewModel.Count == 3);
           
        }

        //[Fact]
        //public void TestExpectingListOfFilteredDevicesWhenCalledWithListOfFilters()
        //{
        //    var filteredDevices = _controller.Get("1.8,1920 x 1080,ECG,7");

        //    Assert.Equal("VUEMX750", filteredDevices[0].id);
        //    Assert.Equal("VUEMX500", filteredDevices[1].id);
        //}
        //[Fact]
        //public void TestExpectingEmptyListWhenNoDeviceQualifiesTheFilters()
        //{
        //    var filteredDevices = _controller.Get("1.6,1920 x 1080,ECG,12");
        //    Assert.False(filteredDevices.Any());
        //}
        //[Fact]
        //public void TestExpectingNullWhenInputStringOfFiltersIsNotInCorrectFormat()
        //{
        //    var filteredDevices = _controller.Get("");
        //    Assert.Null(filteredDevices);
        //}

    }
}
