using System.Collections.Generic;

namespace DataModels
{
    public class DeviceModel
    {
        public string Id { get; set; }
        public string Overview { get; set; }
        public string Name { get; set; }
        public List<string> Measurements { get; set; }
        public float Weight { get; set; }
        public string Batterycapacity {get; set;}
        public string Resolution { get; set; }
        public DeviceModel()
        {
            Measurements = new List<string>();
        }
        
    }
}
