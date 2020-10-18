using Backend.Repository;
using System.Collections.Generic;
using Xunit;

namespace BackendTests
{
    public class DeviceRepositoryTests
    {
        readonly DeviceRepository _deviceRepository = new DeviceRepository();
        readonly DataModels.DeviceModel _device = new DataModels.DeviceModel
        {
            Name = "IntelliVue MX300",
            Id = "VUEMX300",
            BatteryCapacity = "7",
            Measurements = new List<string> { "ECG", "SPO2" },
            Overview = "Something",
            Resolution = "1090 x 1020",
            Weight = 1.2f,
        };
        [Fact]
        public void TestExpectingListOfAllDevicesWhenCalled()
        {
            List<DataModels.DeviceModel> alldevices = _deviceRepository.GetAllDevices();
            Assert.Equal("IntelliVue MX750", alldevices[0].Name);
        }
        [Fact]
        public void TestExpectingADeviceWhenCalledWithStringId()
        {
            DataModels.DeviceModel tempDevice = _deviceRepository.GetDevice("VUEMX750");
            Assert.Equal("IntelliVue MX750", tempDevice.Name);
            
        }
        [Fact]
        public void TestExpectingDeviceToBeAddedIntoListWhenCalledWithNewProduct()
        {
           
            _deviceRepository.AddNewDevice(_device);
            List<DataModels.DeviceModel> devices = _deviceRepository.GetAllDevices();
            Assert.Equal("IntelliVue MX300",devices[^1].Name);
            _deviceRepository.DeleteDevice(_device.Id);

        }
        [Fact]
        public void TestExpectingDeviceToBeRemovedFromTheListWhenCalledWithStringId()
        {
            
            _deviceRepository.AddNewDevice(_device);
            Assert.True(_deviceRepository.DeleteDevice(_device.Id));
            Assert.Null(_deviceRepository.GetDevice(_device.Id));
        }

        [Fact]
        public void TestExpectingDeviceToBeUpdatedFromTheListWhenCalledWithStringId()
        {
            DataModels.DeviceModel tempDevice = _deviceRepository.GetDevice("VUEX3");
            tempDevice.BatteryCapacity = "10";
            _deviceRepository.UpdateDevice("VUEX3",tempDevice);
            Assert.Equal("10", _deviceRepository.GetDevice("VUEX3").BatteryCapacity);
        }
    }
}
