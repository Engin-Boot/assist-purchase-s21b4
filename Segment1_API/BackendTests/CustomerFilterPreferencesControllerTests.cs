using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BackendTests
{
    public class CustomerFilterPreferencesControllerTests
    {
        readonly Backend.Controllers.CustomerFilterPreferencesController _controller = new Backend.Controllers.CustomerFilterPreferencesController(new Backend.Repository.CustomerFilterPreferencesRepository(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\assist-purchase-backend\BackendTests\TestFilterPreferences.csv"));
        
        [Fact]
        public void WhenWantPreferencesThenReturnCustomerPreferences()
        {
           DataModels.FilterDataModel filters = ((JsonResult)_controller.Get("1280")).Value as DataModels.FilterDataModel;
            
            Assert.True(filters.measurements[0].Equals("ECG"));
            Assert.True(filters.weight[0].Equals("1-10"));
        }

        [Fact]
        public void WhenWantToSavePreferencesThenSavePreferences()
        {
            var _filterPreferences = new DataModels.FilterDataModel
            {
                measurements = new List<string> { "ECG", "SPO2", "Respiration", "NiBP", "Pulse" },
                weight = new List<string>(),
                resolution = new List<string>(),
                batterycapacity = new List<string>()
            };
            bool status = (bool)((JsonResult)_controller.Post("1281", _filterPreferences)).Value;
            Assert.True(status);
        }
    }
}
