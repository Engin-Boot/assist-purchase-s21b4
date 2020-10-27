using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Customer.ServerConnection
{
    public static class GetFiltersOptions
    {

        public static List<string> getMeasurementFeatures()
        {
            List<string> _allMeasurements = new List<string>();
            System.Net.HttpWebRequest _httpReq = System.Net.WebRequest.CreateHttp("http://localhost:5000/api/filter/measurements");
            _httpReq.Method = "GET";
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);

                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(List<string>));
                _allMeasurements = _jsonSerializer.ReadObject(response.GetResponseStream()) as List<string>;
                return _allMeasurements;

            }
            return _allMeasurements;
        }
        
    
    }
}
