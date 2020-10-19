using DataModels;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
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
            messageBody.Append("Device Id: " + customer.DeviceId);

            string fromaddr = "s21b4.assistpurchase@gmail.com";
            string toaddr = "aleenasaleem.260199@gmail.com";//TO ADDRESS HERE
            string password = "S21B4@casestudy2";

            MailMessage msg = new MailMessage();
            msg.Subject = "Username &password";
            msg.From = new MailAddress(fromaddr);
            msg.Body = messageBody.ToString();
            msg.To.Add(new MailAddress(toaddr));
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                EnableSsl = true
            };
            NetworkCredential nc = new NetworkCredential(fromaddr, password);
            smtp.Credentials = nc;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(msg);

        }
    }
}
