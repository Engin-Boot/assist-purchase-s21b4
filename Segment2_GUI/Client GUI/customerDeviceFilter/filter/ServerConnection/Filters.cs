using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace filter.ServerConnection
{
    public static class Filters
    {

        public static List<string> getMeasurementsInAllDevices()
        {
            List<string> m = new List<string>();
            System.Net.HttpWebRequest _httpReq =
            System.Net.WebRequest.CreateHttp("http://localhost:5000/api/filter/measurements");
            _httpReq.Method = "GET";
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
               System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(List<string>));
                m =_jsonSerializer.ReadObject(response.GetResponseStream()) as List<string>;
                return m;
            }            
            return m;
        }
        public static DataModels.FilterDataModel getFilterPreferenceByIpFromServer(string ip)
        {
            string url = "http://localhost:5000/api/filterpreferences/";
            url += ip;
            DataModels.FilterDataModel m = new DataModels.FilterDataModel();
            System.Net.HttpWebRequest _httpReq =
            System.Net.WebRequest.CreateHttp(url);
            _httpReq.Method = "GET";
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.FilterDataModel));
                m =_jsonSerializer.ReadObject(response.GetResponseStream()) as DataModels.FilterDataModel;
            }
            return m;
        }
        public static bool AddFilterPreferencesToServer(string ip, DataModels.FilterDataModel fil)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializerForString =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(string));
            string url = "http://localhost:5000/api/filterpreferences/";
            url += ip;
            System.Net.HttpWebRequest _httpPostReq =
              System.Net.WebRequest.CreateHttp(url);
            _httpPostReq.Method = "POST";
            _httpPostReq.ContentType = "application/json";
            System.Runtime.Serialization.Json.DataContractJsonSerializer filterDataJsonSerializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.FilterDataModel));
            filterDataJsonSerializer.WriteObject(_httpPostReq.GetRequestStream(), fil);
            System.Net.HttpWebResponse response = _httpPostReq.GetResponse() as HttpWebResponse;
            bool status = false;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string status_in_string = _jsonSerializerForString.ReadObject(response.GetResponseStream()) as string;
                status = bool.Parse(status_in_string);
            }
            return status;
        }
    }
}
