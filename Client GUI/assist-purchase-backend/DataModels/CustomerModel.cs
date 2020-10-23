
namespace DataModels
{
    public class CustomerModel
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerEmailId { get; set; }
        public string DeviceId { get; set; }
    }
}
/*
 * {
        "id": "VUEMX500",
        "overview": "The IntelliVue MX500 combines powerful monitoring with flexible portability in one compact unit. Supplying comprehensive patient information at a glance it can make a real difference when multiple patients and priorities need attention.",
        "name": "IntelliVue MX500",
        "measurements": [
            "ECG",
            "SPO2",
            "Respiration",
            "NiBP",
            "Pulse"
        ],
        "weight": 1.8,
        "batteryCapacity": "7",
        "resolution": "1920 x 1080"
    }

{
        "CustomerId": "c1",
        "CustomerName": "ABC",
        "CustomerContact": "1234",
        "CustomerEmailId": "PQR",
        "DeviceId": "111213",
        
    }
*/