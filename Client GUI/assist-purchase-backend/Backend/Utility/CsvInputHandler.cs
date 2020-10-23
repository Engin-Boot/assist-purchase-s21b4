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
                if (id != values[0])
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
                    DeviceModel device = FormatStringToDeviceObject(values);
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
            
            string csvData = FormatDeviceObjectDataToString(data);
            if (!string.IsNullOrEmpty(csvData))
            {
                File.AppendAllText(filepath, csvData + '\n');
                isWritten = true;
            }
            
            
            return isWritten;
        }
        private string FormatDeviceObjectDataToString(DeviceModel device)
        {
            string csvFormatData = null;
            try
            {
                if (device.id != null)
                {
                    csvFormatData = string.Join(',', new object[]{
                    device.id,
                    device.name,
                    device.overview,
                    string.Join(' ',device.measurements),
                    device.weight.ToString(CultureInfo.CurrentCulture),
                    device.batterycapacity,
                    device.resolution,
                    });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return csvFormatData;

        }

        private DeviceModel FormatStringToDeviceObject(string[] values)
        {
            DeviceModel device = new DeviceModel
            {

                id = values[0],
                name = values[1],
                overview = values[2],
                measurements = values[3].Split(' ').ToList(),
                weight = float.Parse(values[4]),
                batterycapacity = values[5],
                resolution = values[6],
            };
            return device;
        }

        private bool CompareDataListsAfterDelete(int lineCounter, List<string> devices)
        {

            return lineCounter != devices.Count;
        }


        
    }
}
