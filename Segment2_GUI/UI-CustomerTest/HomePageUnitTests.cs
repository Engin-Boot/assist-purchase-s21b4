using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using Xunit;
using Xunit.Abstractions;
namespace UI_CustomerTest
{
    public class HomePageUnitTests
    {
        private readonly ITestOutputHelper output;

        public HomePageUnitTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        UI_Customer.DataModels.FilterDataModel emptyFilter = new UI_Customer.DataModels.FilterDataModel
        {
            measurements = new List<string>(),
            weight = new List<string>(),
            resolution = new List<string>(),
            batterycapacity = new List<string>()
        };
        //assuming that there is atleast one device containing ECG
        [Fact]
        public void WhenClickOnNeedAsistantshowMeasurementFilters()
        {
            UI_Customer.PreferenceModel.Preferences.SaveFilterPreferencesForIp(emptyFilter);
            var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe");
            
                var HomePage = application.GetWindow("HomePage", InitializeOption.NoCache);

                var assistant_button = HomePage.Get<Button>(
                         SearchCriteria.ByAutomationId("Assistant"));

                output.WriteLine(assistant_button.Text);
                assistant_button.Click();
                var ECG_checkbox = HomePage.Get<CheckBox>(
                         SearchCriteria.ByText("ECG"));
                application.Close();
            
        }
        //Assuming there is one device containing ECG,1-10
        [Fact]
        public void WhenClickOnECGMeasurementThenShowWeightRangeFilters()
        {
            UI_Customer.PreferenceModel.Preferences.SaveFilterPreferencesForIp(emptyFilter);
            var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe");
            
                var HomePage = application.GetWindow("HomePage", InitializeOption.NoCache);

                var assistant_button = HomePage.Get<Button>(
                         SearchCriteria.ByAutomationId("Assistant"));

                output.WriteLine(assistant_button.Text);
                assistant_button.Click();
                var ECG_checkbox = HomePage.Get<CheckBox>(
                         SearchCriteria.ByText("ECG"));
                ECG_checkbox.Click();
                var Weight_Range = HomePage.Get<CheckBox>(SearchCriteria.ByText("1-10 Kg"));
                application.Close();
            
        }
        //Assuming there is one device containing ECG,1-10,1.8,1920x1080
        [Fact]
        public void WhenClickOnWeightRangeThenShowResolutionFilters()
        {
            UI_Customer.PreferenceModel.Preferences.SaveFilterPreferencesForIp(emptyFilter);
            var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe");
            
                var HomePage = application.GetWindow("HomePage", InitializeOption.NoCache);

                var assistant_button = HomePage.Get<Button>(
                         SearchCriteria.ByAutomationId("Assistant"));

                output.WriteLine(assistant_button.Text);
                assistant_button.Click();
                var ECG_checkbox = HomePage.Get<CheckBox>(
                         SearchCriteria.ByText("ECG"));
                ECG_checkbox.Click();
                var Weight_Range = HomePage.Get<CheckBox>(SearchCriteria.ByText("1-10 Kg"));
                Weight_Range.Click();
                var resolution_checkbox = HomePage.Get<CheckBox>(SearchCriteria.ByText("1920x1080"));
                application.Close();
            
        }
        //Assuming there is one device containing ECG,1.8,1920x1080,7
        [Fact]
        public void WhenClickOnResolutionThenShowBatteryCapacityFilters()
        {
            UI_Customer.PreferenceModel.Preferences.SaveFilterPreferencesForIp(emptyFilter);
            var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe");
            
                var HomePage = application.GetWindow("HomePage", InitializeOption.NoCache);

                var assistant_button = HomePage.Get<Button>(
                         SearchCriteria.ByAutomationId("Assistant"));

                output.WriteLine(assistant_button.Text);
                assistant_button.Click();
                var ECG_checkbox = HomePage.Get<CheckBox>(
                         SearchCriteria.ByText("ECG"));
                ECG_checkbox.Click();
                var Weight_Range = HomePage.Get<CheckBox>(SearchCriteria.ByText("1-10 Kg"));
                Weight_Range.Click();
                var resolution_checkbox = HomePage.Get<CheckBox>(SearchCriteria.ByText("1920x1080"));
                resolution_checkbox.Click();
                var battery_checkbox = HomePage.Get<CheckBox>(SearchCriteria.ByText("7"));
                application.Close();
            
        }

        //assuming device with name IntelliVue MX500 is available
        [Fact]
        public void WhenClickOnContactUsThenShowWindowToProvideDeatails()
        {
            var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe");
            
                var HomePage = application.GetWindow("HomePage", InitializeOption.NoCache);
                var ContactUs_Button = HomePage.Get<Button>(
                         SearchCriteria.ByText("Interested? Contact Us for IntelliVue MX500"));

                output.WriteLine(ContactUs_Button.Text);
                ContactUs_Button.Click();
            application.GetWindow("CustomerDetails", InitializeOption.NoCache);
            application.Close();
        }

        
        //Assuming there is one device containing ECG,1.8,1920x1080,7
        [Fact]
        public void WhenCustomerPreferencesAreSavedThenLoadCustomerPreferences()
        {
            UI_Customer.DataModels.FilterDataModel FilterExample = new UI_Customer.DataModels.FilterDataModel
            {
                measurements = new List<string> { "ECG" },
                weight = new List<string> { "1-10" },
                resolution = new List<string> { "1920x1080" },
                batterycapacity = new List<string> { "7" }
            };
            UI_Customer.PreferenceModel.Preferences.SaveFilterPreferencesForIp(FilterExample);
            var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe");
            
                var HomePage = application.GetWindow("HomePage", InitializeOption.NoCache);

                var assistant_button = HomePage.Get<Button>(
                         SearchCriteria.ByAutomationId("Assistant"));

                output.WriteLine(assistant_button.Text);
                assistant_button.Click();
                var ECG_checkbox = HomePage.Get<CheckBox>(
                         SearchCriteria.ByText("ECG"));
                var weightRange_checkbox = HomePage.Get<CheckBox>(
                         SearchCriteria.ByText("1-10 Kg"));
                var resolution_checkbox = HomePage.Get<CheckBox>(
                         SearchCriteria.ByText("1920x1080"));
                var battery_checkbox = HomePage.Get<CheckBox>(
                         SearchCriteria.ByText("7"));
                
                Assert.True(ECG_checkbox.IsSelected);
                Assert.True(weightRange_checkbox.IsSelected);
                Assert.True(resolution_checkbox.IsSelected);
                Assert.True(battery_checkbox.IsSelected);
                application.Close();
            
        }
        
        [Fact]
        public void WhenWindowStartThenCheckDevicesShown()
        {
            UI_Customer.DataModels.DeviceModel[] devices = UI_Customer.ServerConnection.Devices.getAllDevices();
            
            var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe");
            var HomePage = application.GetWindow("HomePage", InitializeOption.NoCache);
            

            foreach (var singleDevice in devices)
            {
                string listOfMeasurements = null;
                foreach (var singleMeasurement in singleDevice.measurements)
                {
                    listOfMeasurements += singleMeasurement + " ";
                }
                string s = singleDevice.name + "\n" + singleDevice.overview + "\n" + listOfMeasurements + "\n";
                HomePage.Get<Label>(SearchCriteria.ByText(s));
                
                
            }
            application.Close();
        }
        
        [Fact]
        public void WhenGetFilteredDevicesFromServerThenShowFilteredDevices()
        {
            UI_Customer.DataModels.FilterDataModel FilterExample = new UI_Customer.DataModels.FilterDataModel
            {
                measurements = new List<string> { "ECG" },
                weight = new List<string> { "1-10" },
                resolution = new List<string> { "1920x1080" },
                batterycapacity = new List<string> { "7" }
            };
            UI_Customer.PreferenceModel.Preferences.SaveFilterPreferencesForIp(FilterExample);
            var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe");

            var HomePage = application.GetWindow("HomePage", InitializeOption.NoCache);

            var assistant_button = HomePage.Get<Button>(
                     SearchCriteria.ByAutomationId("Assistant"));
            assistant_button.Click();
            var devices_got_from_server = UI_Customer.ServerConnection.Devices.getFilterdDevices(FilterExample);
            
            foreach (var singleDevice in devices_got_from_server)
            {
                string listOfMeasurements = null;
                foreach (var singleMeasurement in singleDevice.measurements)
                {
                    listOfMeasurements += singleMeasurement + " ";
                }
                string s = singleDevice.name + "\n" + singleDevice.overview + "\n" + listOfMeasurements + "\n";
                HomePage.Get<Label>(SearchCriteria.ByText(s));
            }
            //Here currently just checking whether filtered devices are present in window or not.
            //it is not checking for other devices than filtered devices are shown or not.(Idealy window must not contain devices other than filtered devices).
            application.Close();
            
        }
        
    }
}
