using DataModels;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IDeviceFiltersRepository
    {

        List<DeviceModel> ApplyAllFilters(DataModels.FilterDataModel filters);
        List<string> GetUniqueMeasurements();
    }
}