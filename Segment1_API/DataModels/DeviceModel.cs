using System.Collections.Generic;

namespace DataModels
{
    public class DeviceModel
    {
        public string id { get; set; }
        public string overview { get; set; }
        public string name { get; set; }
        public List<string> measurements { get; set; }
        public float weight { get; set; }
        public string batterycapacity {get; set;}
        public string resolution { get; set; }
        public DeviceModel()
        {
            measurements = new List<string>();
        }
        
    }
}
