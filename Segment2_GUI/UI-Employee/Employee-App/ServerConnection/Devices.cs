using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace Employee_App.ServerConnection
{
    public static class Devices
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
        public static DataModels.DeviceModel getDeviceById(string id)
        {
            string api = "http://localhost:5000/api/devices/" + id;
            System.Net.HttpWebRequest _httpReq =
                System.Net.WebRequest.CreateHttp(api);
            _httpReq.Method = "GET";
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);

                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.DeviceModel));
                DataModels.DeviceModel m =
                       _jsonSerializer.ReadObject(response.GetResponseStream()) as DataModels.DeviceModel;
                return m;

            }

            return null;
        }
        public static void addDevice(DataModels.DeviceModel f)
        {
            Console.WriteLine(f.measurements.GetType());
            System.Net.HttpWebRequest _httpReq =
               System.Net.WebRequest.CreateHttp("http://localhost:5000/api/devices");
            _httpReq.Method = "POST";
            _httpReq.ContentType = "application/json";
            System.Runtime.Serialization.Json.DataContractJsonSerializer filterDataJsonSerializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.DeviceModel));
            filterDataJsonSerializer.WriteObject(_httpReq.GetRequestStream(), f);
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);

                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.DeviceModel[]));
                MessageBox.Show("Addition Successful !");

            }

        }
        public static void deleteDevice(string id)
        {
            string api = "http://localhost:5000/api/devices/" + id;
            System.Net.HttpWebRequest _httpReq = System.Net.WebRequest.CreateHttp(api);
            _httpReq.Method = "DELETE";
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);

                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.DeviceModel));
                MessageBox.Show("Deletion successful !");
            }


        }
        public static void modifyDevice(string id, DataModels.DeviceModel f)
        {
            string api = "http://localhost:5000/api/devices/" + id;
            System.Net.HttpWebRequest _httpReq = System.Net.WebRequest.CreateHttp(api);
            _httpReq.Method = "PUT";
            _httpReq.ContentType = "application/json";
            System.Runtime.Serialization.Json.DataContractJsonSerializer filterDataJsonSerializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.DeviceModel));
            filterDataJsonSerializer.WriteObject(_httpReq.GetRequestStream(), f);
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                Console.WriteLine("Communication Successful");
                Console.WriteLine(response.ContentType);
                Console.WriteLine(response.ContentLength);

                System.Runtime.Serialization.Json.DataContractJsonSerializer _jsonSerializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.DeviceModel[]));
                MessageBox.Show("Modification Successful !");
            }
        }
    }
}
