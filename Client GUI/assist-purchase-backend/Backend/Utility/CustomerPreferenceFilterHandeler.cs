using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Utility
{
    public static class CustomerPreferenceFilterHandeler
    {
        public static bool saveToCsv(string ip,DataModels.FilterDataModel fil)
        {
            bool isWritten = false;
            
            DeleteFromFile(ip);
            isWritten= AddToCsv(ip, fil);
            return isWritten;
        }
        public static bool DeleteFromFile(string ip)
        {
            bool isDeleted = false;

            List<string> preferences = new List<string>();
            try
            {
                isDeleted = ReadLinesFromFile(ip, preferences);
                WriteLinesToFile(preferences, @"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\assist-purchase-backend\Backend\FilterPreferences.csv");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return isDeleted;

        }
        private static void WriteLinesToFile(List<string> preferences, string filepath)
        {
            using var writer = new StreamWriter(filepath);
            foreach (var writeline in preferences)
            {
                writer.WriteLine(writeline);

            }
        }
        private static bool ReadLinesFromFile(string ip, List<string> preferences)
        {
            int lineCounter = 0;

            using var reader = new StreamReader(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\assist-purchase-backend\Backend\FilterPreferences.csv");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lineCounter++;
                var values = line.Split(',');
                if (ip != values[0])
                {
                    preferences.Add(line);
                }
            }
            bool isDeleted = CompareDataListsAfterDelete(lineCounter, preferences);

            return isDeleted;
        }
        private static bool CompareDataListsAfterDelete(int lineCounter, List<string> devices)
        {

            return lineCounter != devices.Count;
        }
        public static bool AddToCsv(string ip, DataModels.FilterDataModel fil)
        {
            bool isWritten = false;
            string csvFormatData = null;
            if (ip != null)
            {
               // csvFormatData += "\n";
                csvFormatData += string.Join(',', new object[]{
                    ip,
                    string.Join(' ', fil.measurements),
                    string.Join(' ', fil.weight),
                    string.Join(' ', fil.resolution),
                    string.Join(' ', fil.batterycapacity)


                    });
            }

            if (!string.IsNullOrEmpty(csvFormatData))
            {
                File.AppendAllText(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\assist-purchase-backend\Backend\FilterPreferences.csv", csvFormatData);
                isWritten = true;
            }
            return isWritten;
        }

        public static DataModels.FilterDataModel GetPreferences(string ip)
        {
            DataModels.FilterDataModel preferences = new DataModels.FilterDataModel();
            try
            {
                preferences = WriteObjectsToList(ip, @"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\assist-purchase-backend\Backend\FilterPreferences.csv");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return preferences;

        }
        private static DataModels.FilterDataModel WriteObjectsToList(string ip, string filepath)
        {
            DataModels.FilterDataModel filterPreference = new DataModels.FilterDataModel();
            using var reader = new StreamReader(filepath);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var values = line.Split(',');
                    if(ip.Equals(values[0]))
                    {
                       filterPreference = FormatStringToDeviceObject(values);
                        return filterPreference;
                    }
                    
                }


            }
            return filterPreference;
        }

        private static DataModels.FilterDataModel FormatStringToDeviceObject(string[] values)
        {
            string[] w = values[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            float[] weightInFloat = new float[w.Length];
            for (int i = 0; i < w.Length; i++)
            {
                weightInFloat[i] = float.Parse(w[i]);
            }
            Console.WriteLine(values[4]);
            DataModels.FilterDataModel filpre = new DataModels.FilterDataModel
            {


                measurements = values[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList(),

                weight = weightInFloat.ToList(),
                resolution = values[3].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList(),
                
                batterycapacity = values[4].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList()

            };
            Console.WriteLine(filpre.batterycapacity.Count);
            return filpre;
        }

    }
}
