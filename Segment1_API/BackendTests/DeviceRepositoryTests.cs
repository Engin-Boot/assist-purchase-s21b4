using Backend.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTests
{
    public class DeviceRepositoryTests
    {
        static readonly string filepath = @"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\assist-purchase-backend\BackendTests\TestDevices.csv";
        readonly DeviceRepository _deviceRepository = new DeviceRepository(filepath);
        readonly DataModels.DeviceModel _device = new DataModels.DeviceModel
        {
            name = "IntelliVue MX300",
            id = "VUEMX300",
            batterycapacity = "7",
            measurements = new List<string> { "ECG", "SPO2" },
            overview = "Something",
            resolution = "1090 x 1020",
            weight = 1.2f
        };
        [Fact]
        public void TestExpectingListOfAllDevicesWhenCalled()
        {
            List<DataModels.DeviceModel> alldevices = _deviceRepository.GetAllDevices();
            Assert.Equal("IntelliVue MX750", alldevices[0].name);
        }
        [Fact]
        public void TestExpectingEmptyListWhenFileDoesNotExist()
        {
            string tempFilepath = @"E:\BootCamp\NewFolder\assist-purchase-s21b4\Devices.csv";
            var tempDeviceRepository = new DeviceRepository(tempFilepath);
            List<DataModels.DeviceModel> alldevices = tempDeviceRepository.GetAllDevices();
            Assert.False(alldevices.Any());
        }
        [Fact]
        public void TestExpectingADeviceWhenCalledWithStringId()
        {
            DataModels.DeviceModel tempDevice = _deviceRepository.GetDevice("VUEMX750");
            Assert.Equal("IntelliVue MX750", tempDevice.name);
        }
        [Fact]
        public void TestExpectingNullWhenDeviceDoesNotExistInFile()
        {
            DataModels.DeviceModel tempDevice = _deviceRepository.GetDevice("VUEMX1100");
            Assert.Null(tempDevice);
        }

        [Fact]
        public void TestExpectingDeviceToBeAddedIntoListWhenCalledWithNewProduct()
        {
            _deviceRepository.AddNewDevice(_device);
            List<DataModels.DeviceModel> devices = _deviceRepository.GetAllDevices();
            Assert.Equal("IntelliVue MX300", devices[^1].name);
            _deviceRepository.DeleteDevice(_device.id);

        }

        [Fact]
        public void TestExpectingFalseWhenDeviceCouldNotBeAddedToTheFile()
        {
            string tempFilepath = "";
            var tempDeviceRepository = new DeviceRepository(tempFilepath);
            Assert.False(tempDeviceRepository.AddNewDevice(_device));
        }
        [Fact]
        public void TestExpectingFalseWhenDeviceToBeAddedIsNull()
        {
            Assert.False(_deviceRepository.AddNewDevice(null));
        }
        [Fact]
        public void TestExpectingDeviceToBeRemovedFromTheListWhenCalledWithStringId()
        {
            
            _deviceRepository.AddNewDevice(_device);
            Assert.True(_deviceRepository.DeleteDevice(_device.id));
            Assert.Null(_deviceRepository.GetDevice(_device.id));
        }
        [Fact]
        public void TestExpectingFalseWhenDeviceToBeRemovedDoesNotExistInFile()
        {
            Assert.False(_deviceRepository.DeleteDevice("RandomId"));
        }
        [Fact]
        public void TestExpectingFalseWhenFileToBeUsedForDeletingDoesNotExist()
        {
            string tempFilepath = @"NonExistingFilePath";
            var tempDeviceRepository = new DeviceRepository(tempFilepath);
            Assert.False(tempDeviceRepository.DeleteDevice("SomeRandomId"));
        }
        [Fact]
        public void TestExpectingDeviceToBeUpdatedFromTheListWhenCalledWithStringId()
        {
            DataModels.DeviceModel tempDevice = new DataModels.DeviceModel();
             tempDevice= _deviceRepository.GetDevice("VUEX3");
            tempDevice.batterycapacity = "10";
            _deviceRepository.UpdateDevice("VUEX3",tempDevice);
            Assert.Equal("10", _deviceRepository.GetDevice("VUEX3").batterycapacity);
        }
        [Fact]
        public void TestExpectingFalseWhenDeviceToBeUpdatedDoesNotExist()
        {
            Assert.False(_deviceRepository.UpdateDevice("randomId", _device));
        }
    }
}
