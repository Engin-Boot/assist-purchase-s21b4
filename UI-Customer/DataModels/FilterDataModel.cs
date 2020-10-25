using System;
using System.Collections.Generic;
using System.Text;

namespace UI_Customer.DataModels
{
    public class FilterDataModel
    {
        public List<string> measurements { get; set; }
        public List<float> weight { get; set; }
        public List<string> batterycapacity { get; set; }
        public List<string> resolution { get; set; }
        public FilterDataModel()
        {
            measurements = new List<string>();
            weight = new List<float>();
            batterycapacity = new List<string>();
            resolution = new List<string>();

        }
    }
}
