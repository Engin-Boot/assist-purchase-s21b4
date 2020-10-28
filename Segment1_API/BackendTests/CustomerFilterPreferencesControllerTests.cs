using Microsoft.AspNetCore.Mvc;


using System.Collections.Generic;
using Xunit;


namespace BackendTests
{
    public class CustomerFilterPreferencesControllerTests
    {
        readonly Backend.Controllers.CustomerFilterPreferencesController _controller = new Backend.Controllers.CustomerFilterPreferencesController(new Backend.Repository.CustomerFilterPreferencesRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Segment1_API\BackendTests\TestFilterPreferences1.csv"));
        //readonly Backend.Controllers.CustomerFilterPreferencesController _controller = new Backend.Controllers.CustomerFilterPreferencesController(new Backend.Repository.CustomerFilterPreferencesRepository(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy\Segment1_API\BackendTests\TestFilterPreferences1.csv"));

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
            var filterPreferences = new DataModels.FilterDataModel
            {
                measurements = new List<string> { "ECG", "SPO2", "Respiration", "NiBP", "Pulse" },
                weight = new List<string> { "1-10" },
                resolution = new List<string>(),
                batterycapacity = new List<string>{"7"}
            };
            bool status = false;
            string s = ((JsonResult)_controller.Post("1281", filterPreferences)).Value.ToString();
            if (!string.IsNullOrEmpty(s))
            {
                status = bool.Parse(s);
            }
          
            Assert.True(status);
        }
    }
}
