using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository
{
    public class DeviceFiltersRepository : IDeviceFiltersRepository
    {

        public readonly string _csvFilePath;// = @"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\assist-purchase-backend\Backend\Devices.csv";
        readonly List<DataModels.DeviceModel> _filteredDevices = new List<DataModels.DeviceModel>();
        readonly static Utility.CsvInputHandler _csvHandler = new Utility.CsvInputHandler();
        public DeviceFiltersRepository(string filepath)
        {
            this._csvFilePath = filepath;
            _filteredDevices = new DeviceRepository(filepath).GetAllDevices();

        }
        public List<string> getUniqueMeasurements()
        {
            var deviceDb = _csvHandler.ReadFile(_csvFilePath);
            List<string> measurementNames = new List<string>();
            foreach (var device in deviceDb)
            {
                foreach (var measurement in device.measurements)
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
            var filter1 = ApplyMeasurementsFilter(filters.measurements, _filteredDevices);
            var filter2 = ApplyWeightFilter(filters.weight, filter1);
            var filter3 = ApplyResolutionFilter(filters.resolution, filter2);
            var filter4 = ApplyBatteryCapacityFilter(filters.batterycapacity, filter3);
            return filter4;
        }
        public List<DataModels.DeviceModel> ApplyMeasurementsFilter(List<string> expectedMeasurements,List<DataModels.DeviceModel> devices)
        {
            if(expectedMeasurements.Count>0)
            {
                return devices.FindAll(device => ContainsAllMeasurement(device.measurements,expectedMeasurements));
            }
            else
            {
                return devices;
            }
        }
        public bool ContainsAllMeasurement(List<string> actualMeasurements, List<string> expectedMeasurements)
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
        public List<DataModels.DeviceModel> ApplyWeightFilter(List<string> expectedWeight, List<DataModels.DeviceModel> devices)
        {
            if (expectedWeight.Count > 0)
            {
                return devices.FindAll(device => weight_in(device.weight,expectedWeight));                
            }
            else
            {
                return devices;
            }
           
        }
        public bool weight_in(float actualweight,List<string> expectedWeight)
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
        public List<DataModels.DeviceModel> ApplyResolutionFilter(List<string> expectedResolution, List<DataModels.DeviceModel> devices)
        {
            if(expectedResolution.Count > 0)
            {
                return devices.FindAll(device => resolution_equals(device.resolution,expectedResolution));
            }
            else
            {               
                return devices;
            }
        }
        public bool resolution_equals(string actualResolution, List<string> expectedResolution)
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

        public List<DataModels.DeviceModel> ApplyBatteryCapacityFilter(List<string> expectedBattery, List<DataModels.DeviceModel> devices)
        {
            if (expectedBattery.Count > 0)
            {
                return devices.FindAll(device => battery_equals(device.batterycapacity, expectedBattery));
            }
            else
            {
                return devices;
            }
        }
        public bool battery_equals(string actualbattery, List<string> expectedBattery)
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

        //public string getFilePath()
        //{
        //    return _csvFilePath;
        //}
        //public List<DataModels.DeviceModel> ApplyAllFilters(List<string> filters)
        //{
        //    var filter = ApplyWeightFilter(filters[0],_filteredDevices);
        //    var filter1 = ApplyResolutionFilter(filters[1],filter);
        //    var filter2 = ApplyMeasurementsFilter(filters[2],filter1);
        //    var filter3 = ApplyBatteryCapacityFilter(filters[3],filter2);
        //    return filter3;

        //}
        //public List<DataModels.DeviceModel> ApplyWeightFilter(string filterValue, List<DataModels.DeviceModel> filter1)
        //{
        //    float parsedValue;
        //    try
        //    {
        //        parsedValue = float.Parse(filterValue);
        //        return filter1.FindAll(device => (device.weight - parsedValue) < 0.0000001);
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    return filter1;


        //}
        //public List<DataModels.DeviceModel> ApplyResolutionFilter(string filterValue, List<DataModels.DeviceModel> filter2)
        //{
        //    if (filter2 != null && !string.IsNullOrEmpty(filterValue))
        //    {
        //        return filter2.FindAll(device => device.resolution == filterValue);
        //    }
        //    else
        //    {
        //        return filter2;
        //    }
        //}
        //public List<DataModels.DeviceModel> ApplyMeasurementsFilter(string filterValue, List<DataModels.DeviceModel> filter3)
        //{
        //    if (filter3 != null && !string.IsNullOrEmpty(filterValue))
        //    {
        //        return filter3.FindAll(device => device.measurements.Contains(filterValue));
        //    }
        //    else
        //    {
        //        return filter3;
        //    }



        //}
        //public List<DataModels.DeviceModel> ApplyBatteryCapacityFilter(string filterValue, List<DataModels.DeviceModel> filter4)
        //{
        //    if (filter4 != null && !string.IsNullOrEmpty(filterValue))
        //    {
        //        return filter4.FindAll(device => device.batterycapacity == filterValue);
        //    }
        //    else
        //    {
        //        return filter4;
        //    }

        //}

    }
}
