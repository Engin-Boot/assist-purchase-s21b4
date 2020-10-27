using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI_Customer.DataModels;

namespace UI_Customer.ServerConnection
{
    public class AddCustomerDetails
    {
        public static void AddCustomer(CustomerModel customer)
        {

            System.Net.HttpWebRequest _httpReq =
                System.Net.WebRequest.CreateHttp("http://localhost:5000/api/alerter/authenticate");
            _httpReq.Method = "POST";
            _httpReq.ContentType = "application/json";
            DataContractJsonSerializer filterDataJsonSerializer = new DataContractJsonSerializer(typeof(CustomerModel));
            filterDataJsonSerializer.WriteObject(_httpReq.GetRequestStream(), customer);
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                Console.WriteLine("Communication Successful");
                //Console.WriteLine(response.ContentType);
               // Console.WriteLine(response.ContentLength);

                //DataContractJsonSerializer _jsonSerializer = new DataContractJsonSerializer(typeof(DeviceModel[]));
                //DeviceModel[] _allFilteredDevices = _jsonSerializer.ReadObject(response.GetResponseStream()) as DeviceModel[];
                MessageBox.Show("Successful");

            }

        }

        public static void SendMail(CustomerModel customer)
        {

            System.Net.HttpWebRequest _httpReq =
                System.Net.WebRequest.CreateHttp("http://localhost:5000/api/alerter/alert");
            _httpReq.Method = "POST";
            _httpReq.ContentType = "application/json";
            DataContractJsonSerializer filterDataJsonSerializer = new DataContractJsonSerializer(typeof(CustomerModel));
            filterDataJsonSerializer.WriteObject(_httpReq.GetRequestStream(), customer);
            System.Net.HttpWebResponse response = _httpReq.GetResponse() as System.Net.HttpWebResponse;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                Console.WriteLine("Communication Successful");
                //Console.WriteLine(response.ContentType);
                //Console.WriteLine(response.ContentLength);

                //DataContractJsonSerializer _jsonSerializer = new DataContractJsonSerializer(typeof(DeviceModel[]));
                //DeviceModel[] _allFilteredDevices = _jsonSerializer.ReadObject(response.GetResponseStream()) as DeviceModel[];
                MessageBox.Show("Thank You!");

            }

            //return false;
        }
    }
}
