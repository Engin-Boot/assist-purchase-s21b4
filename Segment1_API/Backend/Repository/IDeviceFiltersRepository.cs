using DataModels;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IDeviceFiltersRepository
    {

        List<DeviceModel> ApplyAllFilters(FilterDataModel filters);
        List<string> GetUniqueMeasurements();
    }
}