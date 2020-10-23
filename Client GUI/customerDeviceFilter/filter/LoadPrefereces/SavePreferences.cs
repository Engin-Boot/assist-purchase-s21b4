using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace filter.LoadPrefereces
{
    public static class SavePreferences
    {
        static string getLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }
        public static void SavePreferencesForIp(DataModels.FilterDataModel fil)
        {
            string ip = getLocalIpAddress();
            AddPreferencesToServer(ip,fil);
        }
        
        static void AddPreferencesToServer(string ip,DataModels.FilterDataModel fil)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializerForString =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(string));
            string url = "http://localhost:5000/api/filterNew/";
            url += ip;
            System.Net.HttpWebRequest _httpPostReq =
              System.Net.WebRequest.CreateHttp(url);
            _httpPostReq.Method = "POST";
            _httpPostReq.ContentType = "application/json";
            System.Runtime.Serialization.Json.DataContractJsonSerializer filterDataJsonSerializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.FilterDataModel));
            filterDataJsonSerializer.WriteObject(_httpPostReq.GetRequestStream(), fil);
            System.Net.HttpWebResponse response = _httpPostReq.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string s = _jsonSerializerForString.ReadObject(response.GetResponseStream()) as string;
                //Console.WriteLine(s);
            }
        }
        public static DataModels.FilterDataModel getFilterPreferenceByIp()
        {
            string ip = getLocalIpAddress();
            return getFilterPreferenceByIpFromServer(ip);
        }
        public static DataModels.FilterDataModel getFilterPreferenceByIpFromServer(string ip)
        {
            string url = "http://localhost:5000/api/filterNew/";
            url += ip;
            DataModels.FilterDataModel m = new DataModels.FilterDataModel();
            System.Net.HttpWebRequest _httpReq =
            System.Net.WebRequest.CreateHttp(url);
            _httpReq.Method = "GET";
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);

                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.FilterDataModel));
                 m=
                       _jsonSerializer.ReadObject(response.GetResponseStream()) as DataModels.FilterDataModel;
                

            }
            return m;
        }

    }
}
