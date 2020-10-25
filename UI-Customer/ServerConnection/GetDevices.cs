using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_Customer.DataModels;
using System.Runtime.Serialization.Json;

namespace UI_Customer.ServerConnection
{
    public static class GetDevices
    {
        public static DeviceModel[] GetAllDevices()
        {
            
            System.Net.HttpWebRequest _httpReq =
                System.Net.WebRequest.CreateHttp("http://localhost:5000/api/devices");
            _httpReq.Method = "GET";
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);

                DataContractJsonSerializer _jsonSerializer = new DataContractJsonSerializer(typeof(DeviceModel[]));
                DeviceModel[] _allDevices = _jsonSerializer.ReadObject(response.GetResponseStream()) as DeviceModel[];
                return _allDevices;

            }
            
            return null;
        }

        public static DeviceModel[] getFilterdDevices(FilterDataModel filters)
        {

            System.Net.HttpWebRequest _httpReq =
                System.Net.WebRequest.CreateHttp("http://localhost:5000/api/filterNew");
            _httpReq.Method = "PUT";
            _httpReq.ContentType = "application/json";
            DataContractJsonSerializer filterDataJsonSerializer = new DataContractJsonSerializer(typeof(FilterDataModel));
            filterDataJsonSerializer.WriteObject(_httpReq.GetRequestStream(), filters);
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                //Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);

               DataContractJsonSerializer _jsonSerializer = new DataContractJsonSerializer(typeof(DeviceModel[]));
                DeviceModel[] _allFilteredDevices = _jsonSerializer.ReadObject(response.GetResponseStream()) as DeviceModel[];
                return _allFilteredDevices;

            }

            return null;
        }
    }
}
