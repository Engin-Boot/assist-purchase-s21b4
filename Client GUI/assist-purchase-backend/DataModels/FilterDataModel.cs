using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class FilterDataModel
    {
        public List<string> measurements { get; set; }
        public List<float> weight { get; set; }
        public List<string> batterycapacity { get; set; }
        public List<string> resolution { get; set; }
    }
}
