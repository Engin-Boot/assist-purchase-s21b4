using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
namespace BackendTests
{
    public class DeviceControllerTests
    {
        private readonly ITestOutputHelper output;

        readonly Backend.Controllers.DeviceController _controller = new Backend.Controllers.DeviceController(new Backend.Repository.DeviceRepository(@"C:\Users\Ekta\Desktop\assist-purchase-s21b4\Segment1_API\BackendTests\TestDevices.csv"));
        readonly DataModels.DeviceModel _device = new DataModels.DeviceModel
        {
            name = "IntelliVue MX300",
            id = "VUEMX300",
            batterycapacity = "11",
            measurements = new List<string> { "ECG" },
            overview = "Something",
            resolution = "1090 x 1020",
            weight = 1.2f,
        };
        public DeviceControllerTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public void TestExpectingListOfAllDevicesWhenCalled()
        {
            List<DataModels.DeviceModel> viewModel = ((JsonResult)_controller.Get()).Value as List<DataModels.DeviceModel>;
            //output.WriteLine(viewModel.Count.ToString());
            Assert.True(viewModel[0].name.Equals("IntelliVue MX750"));
        }

        [Fact]
        public void TextExpectingDeviceToBeAddedWhenCalledWithDeviceModelObject()
        {
            Assert.True(_controller.Post(_device));
            _controller.Delete("VUEMX300");
        }
        [Fact]
        public void TestExpectingADeviceWhenCalledWithValidStringId()
        {
            DataModels.DeviceModel device = ((JsonResult)_controller.Get("VUEMX750")).Value as DataModels.DeviceModel;
            Assert.Equal("IntelliVue MX750", device.name);
        }
       [Fact]
        public void TestExpectingDeviceToBeRemovedWhenCalledWithStringId()
        {
            _controller.Post(_device);
            Assert.True(_controller.Delete("VUEMX300"));
        }

        [Fact]
        public void TestExpectingDeviceToBeUpdatedWhenCalledWithStirngIdAndUpdatedDeviceState()
        {
            DataModels.DeviceModel device = new DataModels.DeviceModel
            {
                name = "IntelliVue X3",
                id = "VUEX3",
                batterycapacity = "7",
                measurements = new List<string> { "ECG", "SPO2", "Respiration" },
                overview = "The IntelliVue X3 combines powerful monitoring with flexible portability in one compact unit. Supplying comprehensive patient information at a glance it can make a real difference when multiple patients and priorities need attention.",
                resolution = "1024 x 420",
                weight = 1.9f,
            };
            Assert.True(_controller.Update("VUEX3", device));
        }
        [Fact]
        public void TestExpectingFalseWhenIdOfTheObjectIsNullOrEmpty()
        {
            Assert.False(_controller.Update("", _device));
        }
        [Fact]
        public void TestExpectingFalseWhenUpdatedStateOfTheDeviceIsPassedasNull()
        {
            Assert.False(_controller.Update("VUEMX3", null));
        }
    }
}
