using Microsoft.AspNetCore.Mvc;


using System.Collections.Generic;
using Xunit;


namespace BackendTests
{
    public class CustomerFilterPreferencesControllerTests
    {
        readonly Backend.Controllers.CustomerFilterPreferencesController _controller = new Backend.Controllers.CustomerFilterPreferencesController(new Backend.Repository.CustomerFilterPreferencesRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Segment1_API\BackendTests\TestFilterPreferences1.csv"));
        //readonly Backend.Controllers.CustomerFilterPreferencesController _controller = new Backend.Controllers.CustomerFilterPreferencesController(new Backend.Repository.CustomerFilterPreferencesRepository(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment1_API\BackendTests\TestFilterPreferences1.csv"));

        [Fact]
        public void WhenWantPreferencesThenReturnCustomerPreferences()
        {
            if (((JsonResult)_controller.Get("1280")).Value is DataModels.FilterDataModel filters)
            {
                Assert.True(filters.Measurements[0].Equals("ECG"));
                Assert.True(filters.Weight[0].Equals("1-10"));
            }
            else
            {
                Assert.True(false);
            }



        }

        [Fact]
        public void WhenWantToSavePreferencesThenSavePreferences()
        {
            var filterPreferences = new DataModels.FilterDataModel
            {
                Measurements = new List<string> { "ECG", "SPO2", "Respiration", "NiBP", "Pulse" },
                Weight = new List<string> { "1-10" },
                Resolution = new List<string>(),
                Batterycapacity = new List<string>{"7"}
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
