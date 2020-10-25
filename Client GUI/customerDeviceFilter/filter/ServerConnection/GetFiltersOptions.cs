using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filter.ServerConnection
{
    public static class GetFiltersOptions
    {

        public static List<string> getMeasurementFeatures()
        {
            List<string> m = new List<string>();
            System.Net.HttpWebRequest _httpReq =
            System.Net.WebRequest.CreateHttp("http://localhost:5000/api/filter/measurements");
            _httpReq.Method = "GET";
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);

                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(List<string>));
                m = _jsonSerializer.ReadObject(response.GetResponseStream()) as List<string>;
                return m;

            }
            return m;
        }
        
    
    }
}
