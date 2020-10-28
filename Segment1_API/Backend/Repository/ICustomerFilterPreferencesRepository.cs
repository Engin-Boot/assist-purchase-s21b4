




namespace Backend.Repository
{
    public interface ICustomerFilterPreferencesRepository
    {
        DataModels.FilterDataModel GetCustomerPreferencesByIp(string ip);
        string SaveCustomerPreferencesToFile(string ip, DataModels.FilterDataModel filters);
    }
}
