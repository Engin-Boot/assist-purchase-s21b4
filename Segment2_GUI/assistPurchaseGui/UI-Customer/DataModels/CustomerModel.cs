
using System.Runtime.Serialization;

namespace UI_Customer.DataModels
{
    [DataContract]
    public class CustomerModel
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string CustomerContact { get; set; }
        [DataMember]
        public string CustomerEmailId { get; set; }
        [DataMember]
        public string DeviceId { get; set; }

        public CustomerModel(string _deviceid, string _customerId)
        {
            this.DeviceId = _deviceid;
            this.CustomerId = _customerId;
        }


    }
}
