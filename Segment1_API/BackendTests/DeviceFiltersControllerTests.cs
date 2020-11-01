using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Xunit;

namespace BackendTests
{
    public class DeviceFiltersControllerTests
    {
        //private readonly ITestOutputHelper output;

        readonly Backend.Controllers.DeviceFiltersController _controller = new Backend.Controllers.DeviceFiltersController(new Backend.Repository.DeviceFiltersRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Segment1_API\BackendTests\TestDevices2.csv"));
        //readonly Backend.Controllers.DeviceFiltersController _controller = new Backend.Controllers.DeviceFiltersController(new Backend.Repository.DeviceFiltersRepository(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment1_API\BackendTests\TestDevices2.csv"));
        //public DeviceFiltersControllerTests(ITestOutputHelper output)
        //{
        //    this.output = output;
        //}
        [Fact]
        public void WhenGetAllMeasurementsThenGetExpectedMeasurements()
        {
            //output.WriteLine(measurements.Count.ToString());
            //            ECG SPO2 Respiration NiBP
            if (((JsonResult)_controller.Get()).Value is List<string> measurements)
            {
                Assert.True(measurements.Count == 5);
                Assert.True(measurements[0].Equals("ECG"));
                Assert.True(measurements[1].Equals("SPO2"));
                Assert.True(measurements[2].Equals("Respiration"));
                Assert.True(measurements[3].Equals("NiBP"));
                Assert.True(measurements[4].Equals("Pulse"));
            }
            else
            {
                Assert.True(false);
            }


        }
        [Fact]
        public void WhenWantFiteredDevicesThenGetFilteredDevices()
        {
            var filterPreferences = new DataModels.FilterDataModel
            {
                Measurements = new List<string>{"ECG", "SPO2", "Respiration", "NiBP"},
                Weight = new List<string>(),
                Resolution = new List<string>(),
                Batterycapacity = new List<string> { "7"}
            };
            if (((JsonResult)_controller.Put(filterPreferences)).Value is List<DataModels.DeviceModel> viewModel)
            {
                Assert.True(viewModel[0].Id.Equals("VUEMX750"));
                Assert.True(viewModel.Count == 2);
            }
            else
            {
                Assert.True(false);
            }

        }
        [Fact]
        public void WhenWantFiteredDevicesThenGetFilteredDevices1()
        {
            var filterPreferences = new DataModels.FilterDataModel
            {
                Measurements = new List<string> { "ECG", "SPO2", "Respiration", "NiBP", "Pulse" },
                Weight = new List<string> { "1-10"},
                Resolution = new List<string> { "1920x1080"},
                Batterycapacity = new List<string> { "7"}
            };
            if (((JsonResult)_controller.Put(filterPreferences)).Value is List<DataModels.DeviceModel> viewModel)
            {
                Assert.True(viewModel.Count == 1);
                Assert.True(viewModel[0].Id.Equals("VUEMX500"));
            }
            else
            {
                Assert.True(false);
            }

        }
        [Fact]
        public void WhenWantFiteredDevicesThenGetFilteredDevices2()
        {
            var filterPreferences = new DataModels.FilterDataModel
            {
                Measurements = new List<string> { "ECG"},
                Weight = new List<string> { "1-10" },
                Resolution = new List<string> { "1920x1080","670x480" },
                Batterycapacity = new List<string> { "7" ,"8"}
            };
            if (((JsonResult)_controller.Put(filterPreferences)).Value is List<DataModels.DeviceModel> viewModel)
            {
                Assert.True(viewModel.Count == 2);
            }
            else
            {
                Assert.True(false);
            }

        }
        [Fact]
        public void WhenFilterIsEmptyThenGetAllDevices()
        {
            var filterPreferences = new DataModels.FilterDataModel
            {
                Measurements = new List<string>(),
                Weight = new List<string>(),
                Resolution = new List<string>(),
                Batterycapacity = new List<string>()
            };
            if (((JsonResult)_controller.Put(filterPreferences)).Value is List<DataModels.DeviceModel> viewModel)

                Assert.True(viewModel.Count == 3);
            else
                Assert.True(false);

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
