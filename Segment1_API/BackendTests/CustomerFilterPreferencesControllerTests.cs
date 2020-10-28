using Microsoft.AspNetCore.Mvc;


using System.Collections.Generic;
using Xunit;


namespace BackendTests
{
    public class CustomerFilterPreferencesControllerTests
    {
        readonly Backend.Controllers.CustomerFilterPreferencesController _controller = new Backend.Controllers.CustomerFilterPreferencesController(new Backend.Repository.CustomerFilterPreferencesRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Segment1_API\BackendTests\TestFilterPreferences1.csv"));
        
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
                weight = new List<string>(),
                resolution = new List<string>(),
                batterycapacity = new List<string>()
            };
            bool status = bool.Parse(((JsonResult)_controller.Post("1281", filterPreferences)).Value.ToString());
            Assert.True(status);
        }
    }
}
