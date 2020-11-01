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
    public class CustomerDetailsPageUnitTests
    {
        private readonly ITestOutputHelper output;

        public CustomerDetailsPageUnitTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void WhenNoDetailsProvidedByCustomerThenShowMessageBox()
        {
            var application = Application.Launch(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy 2\assist-purchase-s21b4\Segment2_GUI\UI-Customer\bin\Debug\UI-Customer.exe");

            var HomePage = application.GetWindow("HomePage", InitializeOption.NoCache);
            var ContactUs_Button = HomePage.Get<Button>(
                     SearchCriteria.ByText("Interested? Contact Us for IntelliVue MX500"));

            output.WriteLine(ContactUs_Button.Text);
            ContactUs_Button.Click();
            
            var CustomerDetailsPage = application.GetWindow("CustomerDetails", InitializeOption.NoCache);
            var send_button = CustomerDetailsPage.Get<Button>(SearchCriteria.ByText("Send Details"));
            send_button.Click();
            var messageBox = CustomerDetailsPage.MessageBox("Instructions");
            var label = messageBox.Get<Label>(SearchCriteria.Indexed(0));
            output.WriteLine(label.Name);
            Assert.Equal("Please fill all the Details", label.Name);
            
            application.Close();
        }
        
    }
}
