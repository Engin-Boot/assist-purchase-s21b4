using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository
{
    public class DeviceFiltersRepository : IDeviceFiltersRepository
    {
        static readonly string filepath = @"C:\Users\MEITY\assist-purchase-s21b4\Client GUI\assist-purchase-backend\Backend\Devices.csv";
        readonly List<DataModels.DeviceModel> _filteredDevices = new DeviceRepository(filepath).GetAllDevices();
        public List<DataModels.DeviceModel> ApplyAllFilters(List<string> filters)
        {
            var filter = ApplyWeightFilter(filters[0],_filteredDevices);
            var filter1 = ApplyResolutionFilter(filters[1],filter);
            var filter2 = ApplyMeasurementsFilter(filters[2],filter1);
            var filter3 = ApplyBatteryCapacityFilter(filters[3],filter2);
            return filter3;
           
        }
        public List<DataModels.DeviceModel> ApplyWeightFilter(string filterValue, List<DataModels.DeviceModel> filter1)
        {
            float parsedValue;
            try
            {
                parsedValue = float.Parse(filterValue);
                return filter1.FindAll(device => (device.weight - parsedValue) < 0.0000001);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return filter1;
             
            
        }
        public List<DataModels.DeviceModel> ApplyResolutionFilter(string filterValue, List<DataModels.DeviceModel> filter2)
        {
            if (filter2 != null && !string.IsNullOrEmpty(filterValue))
            {
                return filter2.FindAll(device => device.resolution == filterValue);
            }
            else
            {
                return filter2;
            }
        }
        public List<DataModels.DeviceModel> ApplyMeasurementsFilter(string filterValue, List<DataModels.DeviceModel> filter3)
        {
            if (filter3 != null && !string.IsNullOrEmpty(filterValue))
            {
                return filter3.FindAll(device => device.measurements.Contains(filterValue));
            }
            else
            {
                return filter3;
            }
           

            
        }
        public List<DataModels.DeviceModel> ApplyBatteryCapacityFilter(string filterValue, List<DataModels.DeviceModel> filter4)
        {
            if (filter4 != null && !string.IsNullOrEmpty(filterValue))
            {
                return filter4.FindAll(device => device.batterycapacity == filterValue);
            }
            else
            {
                return filter4;
            }
            
        }

        public List<DeviceModel> ApplyAllFiltersNew(FilterDataModel filters)
        {
            //Console.WriteLine(_filteredDevices[0].BatteryCapacity);
            var filter = ApplyMeasurementsFilterNew(filters.measurements, _filteredDevices);

            var filter1 = ApplyWeightFilterNew(filters.weight, filter);
            var filter2 = ApplyResolutionFilterNew(filters.resolution, filter1);
            var filter3 = ApplyBatteryCapacityFilterNew(filters.batterycapacity, filter2);
            return filter3;
        }

        public List<DataModels.DeviceModel> ApplyMeasurementsFilterNew(List<string> m,List<DataModels.DeviceModel> d)
        {
            if(m.Count>0)
            {
                return d.FindAll(device => ContainsAllMeasurement(device.measurements,m));
            }
            else
            {
                return d;
            }
        }
        public bool ContainsAllMeasurement(List<string> m, List<string> expectedm)
        {
            foreach (var w in expectedm)
            {
                if (!m.Contains(w))
                {
                    return false;
                }
            }
            return true;
        }
        public List<DataModels.DeviceModel> ApplyWeightFilterNew(List<float> w, List<DataModels.DeviceModel> d)
        {
            if (w.Count > 0)
            {
                //Console.WriteLine(d.Count);
                return d.FindAll(device => weight_equals(device.weight,w));
                
            }
            else
            {
                return d;
            }
           
        }
        public bool weight_equals(float actualweight,List<float> expectedWeight)
        {
            foreach(var w in expectedWeight)
            {
                //Console.WriteLine(w);
                if((actualweight - w) < 0.0000001 && (actualweight - w) > -0.0000001)
                {
                    //Console.WriteLine("in loop");
                    //Console.WriteLine(actualweight);
                    return true;
                }
            }
            return false;
        }

        public List<DataModels.DeviceModel> ApplyResolutionFilterNew(List<string> r, List<DataModels.DeviceModel> d)
        {
            if(r.Count > 0)
            {
                return d.FindAll(device => resolution_equals(device.resolution,r));
            }
            else
            {
               
                return d;
            }
        }
        public bool resolution_equals(string actualResolution, List<string> expectedResolution)
        {
            foreach (var r in expectedResolution)
            {
                if (actualResolution==r)
                {
                    return true;
                }
            }
            return false;
        }

        public List<DataModels.DeviceModel> ApplyBatteryCapacityFilterNew(List<string> filterValue, List<DataModels.DeviceModel> d)
        {
            if (filterValue.Count > 0)
            {
                return d.FindAll(device => battery_equals(device.batterycapacity, filterValue));
            }
            else
            {
                //Console.WriteLine(d[0].BatteryCapacity);
                return d;
            }

        }
        public bool battery_equals(string actualbattery, List<string> expectedBattery)
        {
            foreach (var b in expectedBattery)
            {
                if (actualbattery == b)
                {
                    //Console.WriteLine(actualbattery);
                    return true;
                }
            }
            return false;
        }
    }
}
