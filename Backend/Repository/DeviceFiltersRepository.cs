using System;
using System.Collections.Generic;


namespace Backend.Repository
{
    public class DeviceFiltersRepository : IDeviceFiltersRepository
    {
        readonly List<DataModels.DeviceModel> _filteredDevices = new DeviceRepository().GetAllDevices();
        public List<DataModels.DeviceModel> ApplyAllFilters(List<string> filters)
        {
            var filter = ApplyWeightFilter(filters[0],_filteredDevices);
            var filter1 = ApplyResolutionFilter(filters[1],filter);
            var filter2 = ApplyMeasurementsFilter(filters[2],filter1);
            var filter3 = ApplyBatteryCapacityFilter(filters[3],filter2);
            return filter3;
           
        }
        private List<DataModels.DeviceModel> ApplyWeightFilter(string filterValue, List<DataModels.DeviceModel> filter1)
        {
            float parsedValue;
            try
            {
                parsedValue = float.Parse(filterValue);
                if(!string.IsNullOrEmpty(filterValue))
                {
                    return filter1.FindAll(device => (device.Weight - parsedValue) < 0.0000001);
                }
                    
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return filter1;
             
            
        }
        private List<DataModels.DeviceModel> ApplyResolutionFilter(string filterValue, List<DataModels.DeviceModel> filter2)
        {
            if (filter2 != null && filterValue!=null)
            {
                return filter2.FindAll(device => device.Resolution == filterValue);
            }
            else
            {
                return filter2;
            }
        }
        private List<DataModels.DeviceModel> ApplyMeasurementsFilter(string filterValue, List<DataModels.DeviceModel> filter3)
        {
            if (filter3 != null && filterValue!=null)
            {
                return filter3.FindAll(device => device.Measurements.Contains(filterValue));
            }
            else
            {
                return filter3;
            }
           

            
        }
        private List<DataModels.DeviceModel> ApplyBatteryCapacityFilter(string filterValue, List<DataModels.DeviceModel> filter4)
        {
            if (filter4 != null && filterValue!=null)
            {
                return filter4.FindAll(device => device.BatteryCapacity == filterValue);
            }
            else
            {
                return filter4;
            }
            
        }
    }
}
