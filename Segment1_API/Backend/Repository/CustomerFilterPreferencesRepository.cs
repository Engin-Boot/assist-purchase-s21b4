





namespace Backend.Repository
{
    public class CustomerFilterPreferencesRepository : ICustomerFilterPreferencesRepository
    {
        private readonly string _csvFilePath;

        static readonly  Utility.CustomerPreferenceFilterHandeler CsvHandler = new Utility.CustomerPreferenceFilterHandeler();
        public CustomerFilterPreferencesRepository(string filepath)
        {
            this._csvFilePath = filepath;
            
        }

        public DataModels.FilterDataModel GetCustomerPreferencesByIp(string ip)
        {
            return CsvHandler.GetPreferences(ip, _csvFilePath);
        }

        public string SaveCustomerPreferencesToFile(string ip, DataModels.FilterDataModel filters)
        {
            return CsvHandler.SaveToCsv(ip,filters,_csvFilePath).ToString();
        }
    }
}
