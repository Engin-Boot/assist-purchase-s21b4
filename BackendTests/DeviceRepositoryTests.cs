using Backend.Repository;
using System.Collections.Generic;
using Xunit;

namespace BackendTests
{
    public class DeviceRepositoryTests
    {
        [Fact]
        public void TestExpectingListOfAllDevicesWhenCalled()
        {
            DeviceRepository deviceRepository = new DeviceRepository();
            List<DataModels.DeviceModel> alldevices = deviceRepository.GetAllDevices();
            Assert.Equal("IntelliVue MX750", alldevices[0].Name);
        }
        [Fact]
        public void TestExpectingADeviceWhenCalledWithStringId()
        {
            DeviceRepository deviceRepository = new DeviceRepository();
            DataModels.DeviceModel device = deviceRepository.GetDevice("VUEMX750");
            Assert.Equal("IntelliVue MX750", device.Name);
            
        }
        [Fact]
        public void TestExpectingDeviceToBeAddedIntoListWhenCalledWithNewProduct()
        {
            DeviceRepository deviceRepository = new DeviceRepository();
            DataModels.DeviceModel device = new DataModels.DeviceModel { 
                Name="IntelliVue MX300",
                Id="VUEMX300",
                BatteryCapacity="7",
                Measurements=new List<string> { "ECG","SPO2"},
                Overview="Something",
                Resolution="1090 x 1020",
                Weight=1.2f,
            }; 
            deviceRepository.AddNewDevice(device);
            List<DataModels.DeviceModel> devices = deviceRepository.GetAllDevices();
            Assert.Equal("IntelliVue MX300",devices[^1].Name);
            deviceRepository.DeleteDevice(device.Id);

        }
        [Fact]
        public void TestExpectingDeviceToBeRemovedFromTheListWhenCalledWithStringId()
        {
            DeviceRepository deviceRepository = new DeviceRepository();
            DataModels.DeviceModel device = new DataModels.DeviceModel
            {
                Name = "IntelliVue MX300",
                Id = "VUEMX300",
                BatteryCapacity = "7",
                Measurements = new List<string> { "ECG", "SPO2" },
                Overview = "Something",
                Resolution = "1090 x 1020",
                Weight = 1.2f,
            };
            deviceRepository.AddNewDevice(device);
            
            Assert.True(deviceRepository.DeleteDevice(device.Id));
            Assert.Null(deviceRepository.GetDevice(device.Id));
        }

        [Fact]
        public void TestExpectingDeviceToBeUpdatedFromTheListWhenCalledWithStringId()
        {
            DeviceRepository deviceRepository = new DeviceRepository();
            DataModels.DeviceModel device = deviceRepository.GetDevice("VUEX3");
            device.BatteryCapacity = "10";
            deviceRepository.UpdateDevice("VUEX3",device);
            Assert.Equal("10", deviceRepository.GetDevice("VUEX3").BatteryCapacity);
        }
    }
}
