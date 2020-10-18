using DataModels;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repository
{
    public class MailAlerterRepository : IMailAlerterRepository
    {
        private readonly List<CustomerModel> _customerList;

        public MailAlerterRepository()
        {

            _customerList = new List<CustomerModel>();
        }
        public void AddCustomer(CustomerModel customer)
        {
            _customerList.Add(customer);
        }

        public CustomerModel FindCustomer(string customerId)
        {
            foreach (var customer in _customerList)
            {
                if (customerId == customer.CustomerId)
                    return customer;
            }

            return null;
        }

        public void SendEmail(CustomerModel customer)
        {
            var messageBody = new StringBuilder();
            messageBody.Append("Hello,\n");
            messageBody.Append("The following customer has booked the product.\n");
            messageBody.Append("Please see the details and attend the same.\n");
            messageBody.Append("Customer Name: " + customer.CustomerName + "\n");
            messageBody.Append("Customer Phone Number: " + customer.CustomerPhoneNumber + "\n");
            messageBody.Append("Customer Email Id: " + customer.CustomerEmailId + "\n");
            messageBody.Append("Customer Product Name: " + customer.DeviceId);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(customer.CustomerEmailId));
            email.To.Add(MailboxAddress.Parse("aleenasaleem.260199@gmail.com"));
            email.Subject = "Customer Booking Received";
            email.Body = new TextPart(TextFormat.Plain) { Text = messageBody.ToString() };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("[USERNAME]", "[PASSWORD]");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
