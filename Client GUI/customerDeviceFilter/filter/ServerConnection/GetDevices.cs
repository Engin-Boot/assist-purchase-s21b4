using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filter.ServerConnection
{
    public static class GetDevices
    {
        public static DataModels.DeviceModel[] getAllDevices()
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

                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.DeviceModel[]));
                DataModels.DeviceModel[] m =
                       _jsonSerializer.ReadObject(response.GetResponseStream()) as DataModels.DeviceModel[];
                return m;

            }
            
            return null;
        }

        public static DataModels.DeviceModel[] getFilterdDevices(DataModels.FilterDataModel f)
        {

            System.Net.HttpWebRequest _httpReq =
                System.Net.WebRequest.CreateHttp("http://localhost:5000/api/filterNew");
            _httpReq.Method = "PUT";
            _httpReq.ContentType = "application/json";
            System.Runtime.Serialization.Json.DataContractJsonSerializer filterDataJsonSerializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.FilterDataModel));
            filterDataJsonSerializer.WriteObject(_httpReq.GetRequestStream(), f);
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);

                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.DeviceModel[]));
                DataModels.DeviceModel[] m =
                       _jsonSerializer.ReadObject(response.GetResponseStream()) as DataModels.DeviceModel[];
                return m;

            }

            return null;
        }
    }
}
