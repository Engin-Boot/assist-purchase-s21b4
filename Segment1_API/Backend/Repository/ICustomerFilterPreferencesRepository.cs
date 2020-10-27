using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface ICustomerFilterPreferencesRepository
    {
        DataModels.FilterDataModel getCustomerPreferencesByIP(string ip);
        bool saveCustomerPreferencesToFile(string ip, DataModels.FilterDataModel filters);
    }
}
