using DataModels;

using System.Collections.Generic;


namespace Backend.Repository
{
    public class DeviceFiltersRepository : IDeviceFiltersRepository
    {

        private readonly string _csvFilePath;
        readonly List<DeviceModel> _filteredDevices;
        static readonly Utility.CsvInputHandler CsvHandler = new Utility.CsvInputHandler();
        public DeviceFiltersRepository(string filepath)
        {
            this._csvFilePath = filepath;
            _filteredDevices = new DeviceRepository(filepath).GetAllDevices();

        }
        public List<string> GetUniqueMeasurements()
        {
            var deviceDb = CsvHandler.ReadFile(_csvFilePath);
            List<string> measurementNames = new List<string>();
            foreach (var device in deviceDb)
            {
                foreach (var measurement in device.Measurements)
                {
                    if (!measurementNames.Contains(measurement))
                    {
                        measurementNames.Add(measurement);
                    }
                }
            }
            return measurementNames;
        }
        public List<DeviceModel> ApplyAllFilters(FilterDataModel filters)
        {
            var filter1 = ApplyMeasurementsFilter(filters.Measurements, _filteredDevices);
            var filter2 = ApplyWeightFilter(filters.Weight, filter1);
            var filter3 = ApplyResolutionFilter(filters.Resolution, filter2);
            var filter4 = ApplyBatteryCapacityFilter(filters.Batterycapacity, filter3);
            return filter4;
        }
        private List<DeviceModel> ApplyMeasurementsFilter(List<string> expectedMeasurements,List<DeviceModel> devices)
        {
            if(expectedMeasurements.Count>0)
            {
                return devices.FindAll(device => ContainsAllMeasurement(device.Measurements,expectedMeasurements));
            }
            else
            {
                return devices;
            }
        }
        private bool ContainsAllMeasurement(List<string> actualMeasurements, List<string> expectedMeasurements)
        {
            foreach (var w in expectedMeasurements)
            {
                if (!actualMeasurements.Contains(w))
                {
                    return false;
                }
            }
            return true;
        }
        private List<DeviceModel> ApplyWeightFilter(List<string> expectedWeight, List<DeviceModel> devices)
        {
            if (expectedWeight.Count > 0)
            {
                return devices.FindAll(device => weight_in(device.Weight,expectedWeight));                
            }
            else
            {
                return devices;
            }
           
        }
        private bool weight_in(float actualweight,List<string> expectedWeight)
        {
            foreach(var weight in expectedWeight)
            {
                string[] limits = weight.Split('-');
                if(actualweight<=int.Parse(limits[1]) && actualweight>=int.Parse(limits[0]))
                {
                    return true;
                }
            }
            return false;
        }
        private List<DeviceModel> ApplyResolutionFilter(List<string> expectedResolution, List<DeviceModel> devices)
        {
            if(expectedResolution.Count > 0)
            {
                return devices.FindAll(device => resolution_equals(device.Resolution,expectedResolution));
            }
            else
            {               
                return devices;
            }
        }
        private bool resolution_equals(string actualResolution, List<string> expectedResolution)
        {
            foreach (var resolution in expectedResolution)
            {
                if (actualResolution.Equals(resolution))
                {
                    return true;
                }
            }
            return false;
        }

        private List<DeviceModel> ApplyBatteryCapacityFilter(List<string> expectedBattery, List<DeviceModel> devices)
        {
            if (expectedBattery.Count > 0)
            {
                return devices.FindAll(device => battery_equals(device.Batterycapacity, expectedBattery));
            }
            else
            {
                return devices;
            }
        }
        private bool battery_equals(string actualbattery, List<string> expectedBattery)
        {
            foreach (var b in expectedBattery)
            {
                if (actualbattery.Equals(b))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
