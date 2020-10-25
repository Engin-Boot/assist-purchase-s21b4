using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public static class FilterOptions
    {
        readonly static Utility.CsvInputHandler _csvHandler = new Utility.CsvInputHandler();
        public static List<string> getUniqueMeasurements()
        {
            var deviceDb = _csvHandler.ReadFile(@"C:\Users\MEITY\assist-purchase-s21b4\Client GUI\assist-purchase-backend\Backend\Devices.csv");
            List<string> measurementNames = new List<string>();
            foreach(var f in deviceDb)
            {
                foreach(var m in f.measurements)
                {
                    if(!measurementNames.Contains(m))
                    {
                        measurementNames.Add(m);
                    }
                }
            }
            return measurementNames;
        }
    }
}
