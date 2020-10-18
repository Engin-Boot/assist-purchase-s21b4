using RestSharp;
using RestSharp.Serialization;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace BackendControllersTests
{
    public class DeviceControllerTests
    {

        [Fact]
        public void TestExpectingListOfAllDevicesWhenCalled()
        {
            RestClient client = new RestClient("http://localhost:5000/api/devices");
            RestRequest request = new RestRequest("", Method.GET);
            IRestResponse response = client.Execute(request);
            var deserializer = new JsonDeserializer();
            var data = deserializer.Deserialize<List<DataModels.DeviceModel>>(response);
            Assert.True(response.StatusCode == (HttpStatusCode.OK));
            Assert.True(response.ContentType.Equals(ContentType.Json + "; charset=utf-8"));
            Assert.True(data[0].Name == "IntelliVue MX750");

        }
        [Fact]
        public void TextExpectingDeviceToBeAddedWhenCalledWithDeviceModelObject()
        {
            DataModels.DeviceModel device = new DataModels.DeviceModel
            {
                Name = "IntelliVue MX300",
                Id = "VUEMX300",
                BatteryCapacity = "7",
                Measurements = new List<string> { "ECG", "SPO2" },
                Overview = "Something",
                Resolution = "1090 x 1020",
                Weight = 1.2f,
            };

            HttpWebRequest httpPostReq = WebRequest.CreateHttp("http://localhost:5000/api/devices");
            httpPostReq.Method = "POST";
            httpPostReq.ContentType = "application/json";
            System.Runtime.Serialization.Json.DataContractJsonSerializer deviceDataJsonSerializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.DeviceModel));
            deviceDataJsonSerializer.WriteObject(httpPostReq.GetRequestStream(), device);
            HttpWebResponse response = httpPostReq.GetResponse() as HttpWebResponse;
            if (response != null)
            {
                Assert.True(response.StatusCode == HttpStatusCode.OK);
            }

            httpPostReq.Abort();
            response.Dispose();

            var id = device.Id;
            RestClient client = new RestClient("http://localhost:5000/api/devices");
            RestRequest request = new RestRequest("/" + id, Method.GET);
            client.Execute(request);
        }
        [Fact]
        public void TestExpectingADeviceWhenCalledWithValidStringId()
        {
            var id = "VUEMX750";
            RestClient client = new RestClient("http://localhost:5000/api/devices");
            RestRequest request = new RestRequest("/" + id, Method.GET);
            IRestResponse response = client.Execute(request);
            var deserializer = new JsonDeserializer();
            var data = deserializer.Deserialize<DataModels.DeviceModel>(response);
            Assert.True(response.StatusCode == (HttpStatusCode.OK));
            Assert.True(data.Name == "IntelliVue MX750");
        }
        [Fact]
        public void TestExpectingDeviceToBeRemovedWhenCalledWithStringId()
        {

            HttpWebRequest httpDeleteReq = WebRequest.CreateHttp("http://localhost:5000/api/devices/VUEMX300");
            httpDeleteReq.Method = "DELETE";
            httpDeleteReq.ContentType = "application/json";
            HttpWebResponse response = httpDeleteReq.GetResponse() as HttpWebResponse;
            if(response!=null)
                 Assert.True(response.StatusCode == HttpStatusCode.OK);
            response.Dispose();
            httpDeleteReq.Abort();
        }

        [Fact]
        public void TestExpectingDeviceToBeUpdatedWhenCalledWithStirngIdAndUpdatedDeviceState()
        {

            DataModels.DeviceModel device = new DataModels.DeviceModel
            {
                Name = "IntelliVue X3",
                Id = "VUEX3",
                BatteryCapacity = "7",
                Measurements = new List<string> { "ECG", "SPO2", "Respiration" },
                Overview = "The IntelliVue X3 combines powerful monitoring with flexible portability in one compact unit. Supplying comprehensive patient information at a glance it can make a real difference when multiple patients and priorities need attention.",
                Resolution = "1024 x 420",
                Weight = 1.9f,
            };

            //testing the update API 
            HttpWebRequest httpPutReq = WebRequest.CreateHttp("http://localhost:5000/api/devices/VUEX3");
            httpPutReq.Method = "PUT";
            httpPutReq.ContentType = "application/json";
            System.Runtime.Serialization.Json.DataContractJsonSerializer deviceDataJsonSerializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(DataModels.DeviceModel));
            deviceDataJsonSerializer.WriteObject(httpPutReq.GetRequestStream(), device);
            HttpWebResponse response = httpPutReq.GetResponse() as HttpWebResponse;
            if(response!=null)
                 Assert.True(response.StatusCode == HttpStatusCode.OK);
            httpPutReq.Abort();
        }

    }
}
