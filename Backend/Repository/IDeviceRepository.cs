using DataModels;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IDeviceRepository
    {
        bool AddNewDevice(DeviceModel device);
        bool DeleteDevice(string id);
        List<DeviceModel> GetAllDevices();
        DeviceModel GetDevice(string id);
        void UpdateDevice(string id, DeviceModel device);
    }
}