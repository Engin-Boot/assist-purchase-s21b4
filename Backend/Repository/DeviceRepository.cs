using System.Collections.Generic;


namespace Backend.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly string _csvFilePath;
        readonly Utility.CsvInputHandler _csvHandler = new Utility.CsvInputHandler();
        public DeviceRepository(string filepath)
        {
            this._csvFilePath = filepath;
        }
        public List<DataModels.DeviceModel> GetAllDevices()
        {
            return  _csvHandler.ReadFile(_csvFilePath);
            
        }
        public DataModels.DeviceModel GetDevice(string id)
        {
            var deviceDb = _csvHandler.ReadFile(_csvFilePath);
            return deviceDb.Find(device => device.Id == id);
        }
        public bool AddNewDevice(DataModels.DeviceModel device)
        {
            return  _csvHandler.WriteToFile(device, _csvFilePath);
        }
        public bool DeleteDevice(string id)
        {
            return _csvHandler.DeleteFromFile(id, _csvFilePath);
        }
        public bool UpdateDevice(string id, DataModels.DeviceModel device)
        {
            bool updated = false;
            var deviceDb = _csvHandler.ReadFile(_csvFilePath);
            foreach (var tempDevice in deviceDb)
            {
                if (tempDevice.Id == id)
                {
                    updated = _csvHandler.DeleteFromFile(tempDevice.Id, _csvFilePath);
                    updated &= _csvHandler.WriteToFile(device, _csvFilePath);
                }
            }
            return updated;
            
        }
    }
}
