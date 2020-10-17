using DataModels;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IDeviceFiltersRepository
    {
        List<DeviceModel> ApplyAllFilters(List<string> filters);
        
    }
}