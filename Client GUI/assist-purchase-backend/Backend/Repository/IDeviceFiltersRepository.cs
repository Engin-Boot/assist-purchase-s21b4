﻿using DataModels;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IDeviceFiltersRepository
    {
        List<DeviceModel> ApplyAllFilters(List<string> filters);
        List<DeviceModel> ApplyAllFiltersNew(DataModels.FilterDataModel filters);
    }
}