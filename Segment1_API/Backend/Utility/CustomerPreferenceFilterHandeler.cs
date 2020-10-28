using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Backend.Utility
{
    public class CustomerPreferenceFilterHandeler
    {
        
       
        
        public bool SaveToCsv(string ip,DataModels.FilterDataModel fil,string filepath)
        {
            DeleteFromFile(ip,filepath);
            bool isWritten = AddToCsv(ip, fil, filepath);
            return isWritten;
        
        
        }
        private void DeleteFromFile(string ip,string filepath)
        {
            //bool isDeleted = false;

            List<string> preferences = new List<string>();
            try
            {
                /*isDeleted =*/ ReadLinesFromFileAndDeleteIfAvailable(ip, preferences,filepath);
                WriteLinesToFile(preferences, filepath);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //return isDeleted;

        }
        private void WriteLinesToFile(List<string> preferences, string filepath)
        {
            using var writer = new StreamWriter(filepath);
            foreach (var writeline in preferences)
            {
                writer.WriteLine(writeline);

            }
        }
        private void ReadLinesFromFileAndDeleteIfAvailable(string ip, List<string> preferences,string filepath)
        {
            int lineCounter = 0;

            using var reader = new StreamReader(filepath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lineCounter++;
                var values = line.Split(',');
                if (!ip.Equals(values[0]))
                {
                    preferences.Add(line);
                }
            }
            //bool isDeleted = CompareDataListsAfterDelete(lineCounter, preferences);

            //return isDeleted;
        }
        //private bool CompareDataListsAfterDelete(int lineCounter, List<string> devices)
        //{

        //    return lineCounter != devices.Count;
        //}
        private bool AddToCsv(string ip, DataModels.FilterDataModel fil,string filepath)
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
                File.AppendAllText(filepath, csvFormatData);
                isWritten = true;
            }
            return isWritten;
        }

        public DataModels.FilterDataModel GetPreferences(string ip,string filepath)
        {
            DataModels.FilterDataModel preferences = new DataModels.FilterDataModel();
            try
            {
                preferences = WritePreferenceObjectsToList(ip, filepath);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return preferences;

        }
        private DataModels.FilterDataModel WritePreferenceObjectsToList(string ip, string filepath)
        {
            DataModels.FilterDataModel filterPreference = new DataModels.FilterDataModel();
            using var reader = new StreamReader(filepath);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                
                    var values = line.Split(',');
                    if(ip.Equals(values[0]))
                    {
                       filterPreference = FormatStringToDeviceObject(values);
                        return filterPreference;
                    }
                    
                


            }
            return filterPreference;
        }
        
        private DataModels.FilterDataModel FormatStringToDeviceObject(string[] values)
        {
            
           // Console.WriteLine(values[4]);
            DataModels.FilterDataModel filpre = new DataModels.FilterDataModel
            {


                measurements = values[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList(),

                weight = values[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList(),
                resolution = values[3].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList(),
                
                batterycapacity = values[4].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList()

            };
            //Console.WriteLine(filpre.batterycapacity.Count);
            return filpre;
        }

    }
}
