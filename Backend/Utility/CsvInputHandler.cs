using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Backend.Utility
{
    public class CsvInputHandler
    {
        public bool DeleteFromFile(string id, string filepath)
        {
            bool isDeleted = false;
            
            int lineCounter = 0;
            List<string> devices = new List<string>();
            try
            {

                using (var reader = new StreamReader(filepath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineCounter++;
                        var values = line.Split(',');
                        if (string.Compare(id, values[0]) != 0)
                        {
                            devices.Add(line);

                        }
                    }
                    isDeleted = CompareDataListsAfterDelete(lineCounter, devices);

                }
                using var writer = new StreamWriter(filepath);
                foreach (var writeline in devices)
                {
                    writer.WriteLine(writeline);

                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return isDeleted;

        }

        public List<DataModels.DeviceModel> ReadFile(string filepath)
        {
            List<DataModels.DeviceModel> devices = new List<DataModels.DeviceModel>();
            try
            {
                if (File.Exists(filepath))
                {
                    using var reader = new StreamReader(filepath);
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        DataModels.DeviceModel device = FormatStringToObject(values);
                        devices.Add(device);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return devices;
        }

        public bool WriteToFile(DataModels.DeviceModel data, string filepath)
        {
            bool isWritten = false;
            try
            {
                string csvData = FormatObjectDataToString(data);
                if (File.Exists(filepath) && csvData != null)
                {
                    File.AppendAllText(filepath, csvData + '\n');
                    isWritten = true;
                }
               
               
            }
            catch (Exception e)
            {
                isWritten = false;
                Console.WriteLine(e.Message);
            }
            return isWritten;
        }

        private string FormatObjectDataToString(DataModels.DeviceModel device)
        {
            string csvFormatData = null;
            try
            {
                if (device.Id != null)
                {
                    csvFormatData = string.Join(',', new object[]{
                    device.Id,
                    device.Name,
                    device.Overview,
                    string.Join(' ',device.Measurements),
                    device.Weight.ToString(),
                    device.BatteryCapacity,
                    device.Resolution,
                    });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return csvFormatData;

        }

        private DataModels.DeviceModel FormatStringToObject(string[] values)
        {
            DataModels.DeviceModel device = new DataModels.DeviceModel
            {

                Id = values[0],
                Name = values[1],
                Overview = values[2],
                Measurements = values[3].Split(' ').ToList<string>(),
                Weight = float.Parse(values[4]),
                BatteryCapacity = values[5],
                Resolution = values[6],
            };
            return device;
        }

        private bool CompareDataListsAfterDelete(int lineCounter, List<string> devices)
        {

            return lineCounter != devices.Count;
        }
    }
}
