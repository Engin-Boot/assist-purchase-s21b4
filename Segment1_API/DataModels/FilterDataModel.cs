
using System.Collections.Generic;


namespace DataModels
{
    public class FilterDataModel
    {
        public List<string> Measurements { get; set; }
        public List<string> Weight { get; set; }
        public List<string> Batterycapacity { get; set; }
        public List<string> Resolution { get; set; }
        public FilterDataModel()
        {
            Measurements = new List<string>();
            Weight = new List<string>();
            Batterycapacity = new List<string>();
            Resolution = new List<string>();
        }
    }
}
