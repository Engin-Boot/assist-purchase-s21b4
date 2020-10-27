using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class FilterDataModel
    {
        public List<string> measurements { get; set; }
        public List<string> weight { get; set; }
        public List<string> batterycapacity { get; set; }
        public List<string> resolution { get; set; }
        public FilterDataModel()
        {
            measurements = new List<string>();
            weight = new List<string>();
            batterycapacity = new List<string>();
            resolution = new List<string>();

        }
    }
}
