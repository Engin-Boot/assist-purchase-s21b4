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
    public class UnitTests
    {
        private readonly ITestOutputHelper output;

        public UnitTests(ITestOutputHelper output)
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
            using (var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe"))
            {
                var MainWindow = application.GetWindow("MainWindow", InitializeOption.NoCache);

                var assistant_button = MainWindow.Get<Button>(
                         SearchCriteria.ByAutomationId("Assistant"));

                output.WriteLine(assistant_button.Text);
                assistant_button.Click();
                var ECG_checkbox = MainWindow.Get<CheckBox>(
                         SearchCriteria.ByText("ECG"));
                application.Close();
            }
        }
        //Assuming there is one device containing ECG,1-10
        [Fact]
        public void WhenClickOnECGMeasurementThenShowWeightRangeFilters()
        {
            UI_Customer.PreferenceModel.Preferences.SaveFilterPreferencesForIp(emptyFilter);
            using (var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe"))
            {
                var MainWindow = application.GetWindow("MainWindow", InitializeOption.NoCache);

                var assistant_button = MainWindow.Get<Button>(
                         SearchCriteria.ByAutomationId("Assistant"));

                output.WriteLine(assistant_button.Text);
                assistant_button.Click();
                var ECG_checkbox = MainWindow.Get<CheckBox>(
                         SearchCriteria.ByText("ECG"));
                ECG_checkbox.Click();
                var Weight_Range = MainWindow.Get<CheckBox>(SearchCriteria.ByText("1-10 Kg"));
                application.Close();
            }
        }
        //Assuming there is one device containing ECG,1-10,1.8,1920x1080
        [Fact]
        public void WhenClickOnWeightRangeThenShowResolutionFilters()
        {
            UI_Customer.PreferenceModel.Preferences.SaveFilterPreferencesForIp(emptyFilter);
            using (var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe"))
            {
                var MainWindow = application.GetWindow("MainWindow", InitializeOption.NoCache);

                var assistant_button = MainWindow.Get<Button>(
                         SearchCriteria.ByAutomationId("Assistant"));

                output.WriteLine(assistant_button.Text);
                assistant_button.Click();
                var ECG_checkbox = MainWindow.Get<CheckBox>(
                         SearchCriteria.ByText("ECG"));
                ECG_checkbox.Click();
                var Weight_Range = MainWindow.Get<CheckBox>(SearchCriteria.ByText("1-10 Kg"));
                Weight_Range.Click();
                var resolution_checkbox = MainWindow.Get<CheckBox>(SearchCriteria.ByText("1920x1080"));
                application.Close();
            }
        }
        //Assuming there is one device containing ECG,1-10,1.8,1920x1080,7
        [Fact]
        public void WhenClickOnResolutionThenShowBatteryCapacityFilters()
        {
            UI_Customer.PreferenceModel.Preferences.SaveFilterPreferencesForIp(emptyFilter);
            using (var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe"))
            {
                var MainWindow = application.GetWindow("MainWindow", InitializeOption.NoCache);

                var assistant_button = MainWindow.Get<Button>(
                         SearchCriteria.ByAutomationId("Assistant"));

                output.WriteLine(assistant_button.Text);
                assistant_button.Click();
                var ECG_checkbox = MainWindow.Get<CheckBox>(
                         SearchCriteria.ByText("ECG"));
                ECG_checkbox.Click();
                var Weight_Range = MainWindow.Get<CheckBox>(SearchCriteria.ByText("1-10 Kg"));
                Weight_Range.Click();
                var resolution_checkbox = MainWindow.Get<CheckBox>(SearchCriteria.ByText("1920x1080"));
                resolution_checkbox.Click();
                var battery_checkbox = MainWindow.Get<CheckBox>(SearchCriteria.ByText("7"));
                application.Close();
            }
        }

        //assuming device with name IntelliVue MX500 is available
        [Fact]
        public void WhenClickOnContactUsThenShowFormToProvideDeatails()
        {
            using (var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe"))
            {
                var MainWindow = application.GetWindow("MainWindow", InitializeOption.NoCache);
                var ContactUs_Button = MainWindow.Get<Button>(
                         SearchCriteria.ByText("Interested? Contact Us for IntelliVue MX500"));

                output.WriteLine(ContactUs_Button.Text);
                ContactUs_Button.Click();
            }
        }
    }
}
