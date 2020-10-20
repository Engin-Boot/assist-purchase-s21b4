using DataModels;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace Backend.Repository
{
    public class MailAlerterRepository : IMailAlerterRepository
    {
        private readonly string _csvFilePath;
        readonly Utility.CustomerDataBaseHandler _csvHandler = new Utility.CustomerDataBaseHandler();

        public MailAlerterRepository(string filepath)
        {
            this._csvFilePath = filepath;
        }
        public bool AddCustomer(CustomerModel customer)
        {
            if(FindCustomer(customer.CustomerId) == null)
            {
                return _csvHandler.WriteToFile(customer, _csvFilePath);
            }
            else
            {
                _csvHandler.DeleteFromFile(customer.CustomerId,_csvFilePath);
                return _csvHandler.WriteToFile(customer, _csvFilePath);
            }
            
        }
        //ReSharper disable all
        public CustomerModel FindCustomer(string customerId)
        {
            var customerList = _csvHandler.ReadCustomerDetailsFromFile(_csvFilePath);
            foreach (var customer in customerList)
            {
                if (customerId == customer.CustomerId)
                    return customer;
            }

            return null;
        }
        //ReSharper restore all
        public bool DeleteCustomerDetails(string id)
        {
            return _csvHandler.DeleteFromFile(id, _csvFilePath);
        }
        public bool SendEmail(CustomerModel customer)
        {
            bool sent;
            var messageBody = new StringBuilder();
            messageBody.Append("The following customer has requested a device.\n");
            messageBody.Append("Find the details below.\n");
            messageBody.Append("Customer Name: " + customer.CustomerName + "\n");
            messageBody.Append("Customer Phone Number: " + customer.CustomerContact + "\n");
            messageBody.Append("Customer Email Id: " + customer.CustomerEmailId + "\n");
            messageBody.Append("Device Id: " + customer.DeviceId);

            string fromaddr = "s21b4.assistpurchase@gmail.com";
            string toaddr = "aleenasaleem.260199@gmail.com";//TO ADDRESS HERE
            string password = "S21B4@casestudy2";

            MailMessage msg = new MailMessage
            {
                Subject = "Customer requested a contact",
                From = new MailAddress(fromaddr),
                Body = messageBody.ToString()
            };
            msg.To.Add(new MailAddress(toaddr));
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromaddr, password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            try
            {
                smtp.Send(msg);
                sent = true;
            }
            catch (Exception e)
            {
                sent = false;
                Console.WriteLine(e.Message);
            }

            smtp.Dispose();
            return sent;
        }
    }
}
