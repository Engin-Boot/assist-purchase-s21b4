using Backend.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackendTests
{
    public class DeviceRepositoryTests
    {
        static readonly string filepath = @"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Segment1_API\BackendTests\TestDevices3.csv";
        //static readonly string filepath = @"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment1_API\BackendTests\TestDevices3.csv";
        readonly DeviceRepository _deviceRepository = new DeviceRepository(filepath);
        readonly DataModels.DeviceModel _device = new DataModels.DeviceModel
        {
            Name = "IntelliVue MX300",
            Id = "VUEMX300",
            Batterycapacity = "7",
            Measurements = new List<string> { "ECG", "SPO2" },
            Overview = "Something",
            Resolution = "1090 x 1020",
            Weight = 1.2f
        };
        [Fact]
        public void TestExpectingListOfAllDevicesWhenCalled()
        {
            List<DataModels.DeviceModel> alldevices = _deviceRepository.GetAllDevices();
            Assert.Equal("IntelliVue MX750", alldevices[0].Name);
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
            Assert.Equal("IntelliVue MX750", tempDevice.Name);
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
            Assert.Equal("IntelliVue MX300", devices[^1].Name);
            _deviceRepository.DeleteDevice(_device.Id);

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
            Assert.True(_deviceRepository.DeleteDevice(_device.Id));
            Assert.Null(_deviceRepository.GetDevice(_device.Id));
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
            DataModels.DeviceModel tempDevice = _deviceRepository.GetDevice("VUEX3");

            tempDevice.Batterycapacity = "10";
            _deviceRepository.UpdateDevice("VUEX3",tempDevice);
            Assert.Equal("10", _deviceRepository.GetDevice("VUEX3").Batterycapacity);
        }
        [Fact]
        public void TestExpectingFalseWhenDeviceToBeUpdatedDoesNotExist()
        {
            Assert.False(_deviceRepository.UpdateDevice("randomId", _device));
        }
    }
}
