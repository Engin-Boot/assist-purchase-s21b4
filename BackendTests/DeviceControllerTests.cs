using System.Collections.Generic;
using Xunit;
namespace BackendTests
{
    public class DeviceControllerTests
    {
        readonly Backend.Controllers.DeviceController _controller = new Backend.Controllers.DeviceController(new Backend.Repository.DeviceRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\BackendTests\TestDevices.csv"));
        readonly DataModels.DeviceModel _device = new DataModels.DeviceModel
        {
            Name = "IntelliVue MX300",
            Id = "VUEMX300",
            BatteryCapacity = "11",
            Measurements = new List<string> { "ECG" },
            Overview = "Something",
            Resolution = "1090 x 1020",
            Weight = 1.2f,
        };
        [Fact]
        public void TestExpectingListOfAllDevicesWhenCalled()
        {
            var devices = (List<DataModels.DeviceModel>)_controller.Get();
            Assert.True(devices[0].Name == "IntelliVue MX750");
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
            var device = _controller.Get("VUEMX750");
            Assert.Equal("IntelliVue MX750", device.Name);
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
                Name = "IntelliVue X3",
                Id = "VUEX3",
                BatteryCapacity = "7",
                Measurements = new List<string> { "ECG", "SPO2", "Respiration" },
                Overview = "The IntelliVue X3 combines powerful monitoring with flexible portability in one compact unit. Supplying comprehensive patient information at a glance it can make a real difference when multiple patients and priorities need attention.",
                Resolution = "1024 x 420",
                Weight = 1.9f,
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
