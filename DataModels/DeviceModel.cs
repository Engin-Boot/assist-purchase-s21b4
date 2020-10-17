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
        public string BatteryCapacity {get; set;}
        public string Resolution { get; set; }
    }
}
