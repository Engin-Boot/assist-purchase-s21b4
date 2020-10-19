using DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Backend.Utility
{
    public class CsvInputHandler
    {
        public bool DeleteFromFile(string id, string filepath)
        {
            bool isDeleted = false;
            
            List<string> devices = new List<string>();
            try
            {
                isDeleted = ReadLinesFromFile(id,devices, filepath);
                WriteLinesToFile(devices,filepath);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return isDeleted;

        }
        private bool ReadLinesFromFile(string id,List<string> devices, string filepath)
        {
            int lineCounter = 0;
           
            using var reader = new StreamReader(filepath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lineCounter++;
                var values = line.Split(',');
                if (string.Compare(id, values[0], true, CultureInfo.CurrentCulture) != 0)
                {
                    devices.Add(line);

                }
            }
            bool isDeleted = CompareDataListsAfterDelete(lineCounter, devices);

            return isDeleted;
        }
       
        private void WriteLinesToFile(List<string> devices,string filepath)
        {
            using var writer = new StreamWriter(filepath);
            foreach (var writeline in devices)
            {
                writer.WriteLine(writeline);

            }
        }

        public List<DeviceModel> ReadFile(string filepath)
        {
            List<DeviceModel> devices = new List<DeviceModel>();
            try
            {
                    devices = WriteObjectsToList(devices, filepath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return devices;
        }
        private List<DeviceModel> WriteObjectsToList(List<DeviceModel> devices,string filepath)
        {
            using var reader = new StreamReader(filepath);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var values = line.Split(',');
                    DeviceModel device = FormatStringToObject(values);
                    devices.Add(device);
                }
                

            }
            return devices;
        }

        public bool WriteToFile(DeviceModel data, string filepath)
        {
            if (!File.Exists(filepath))
            {
                return false;
            }
            return AppendTextToFile(data,filepath);
        }

        private bool AppendTextToFile(DeviceModel data,string filepath)
        {
            bool isWritten = false;
            
            string csvData = FormatObjectDataToString(data);
            if (!string.IsNullOrEmpty(csvData))
            {
                try
                {
                    File.AppendAllText(filepath, csvData + '\n');
                    isWritten = true;
                }
                catch (Exception e)
                {
                    isWritten = false;
                    Console.WriteLine(e.Message);
                }
            }
            
            
            return isWritten;
        }
        private string FormatObjectDataToString(DeviceModel device)
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
                    device.Weight.ToString(CultureInfo.CurrentCulture),
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

        private DeviceModel FormatStringToObject(string[] values)
        {
            DeviceModel device = new DeviceModel
            {

                Id = values[0],
                Name = values[1],
                Overview = values[2],
                Measurements = values[3].Split(' ').ToList(),
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
