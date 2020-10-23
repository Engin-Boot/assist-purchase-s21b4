using DataModels;
using System;
using System.Collections.Generic;
using System.IO;


namespace Backend.Utility
{
    public class CustomerDataBaseHandler
    {
        public List<CustomerModel> ReadCustomerDetailsFromFile(string filepath)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            try
            {
                customers = WriteObjectsToList(customers, filepath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return customers;
        }
        private List<CustomerModel> WriteObjectsToList(List<CustomerModel> customers, string filepath)
        {
            using var reader = new StreamReader(filepath);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var values = line.Split(',');
                    CustomerModel customer = FormatStringToCustomerObject(values);
                    customers.Add(customer);
                }


            }
            return customers;
        }
        private CustomerModel FormatStringToCustomerObject(string[] values)
        {
            CustomerModel customer = new CustomerModel()
            {
                CustomerId = values[0],
                CustomerName = values[1],
                CustomerContact = values[2],
                CustomerEmailId = values[3],
                DeviceId = values[4]
            };
            return customer;
        }


        public bool WriteToFile(CustomerModel data, string filepath)
        {
            if (!File.Exists(filepath))
            {
                return false;
            }
            return AppendTextToFile(data, filepath);
        }
        private bool AppendTextToFile(CustomerModel data, string filepath)
        {
            bool isWritten = false;

            string csvData = FormatCustomerObjectDataToString(data);
            csvData += "\n";
            if (!string.IsNullOrEmpty(csvData))
            {
                File.AppendAllText(filepath, csvData);
                isWritten = true;
            }
            return isWritten;
        }
        private string FormatCustomerObjectDataToString(CustomerModel customer)
        {
            string csvFormatData = null;
            try
            {
                if (customer.CustomerId != null)
                {
                    csvFormatData = string.Join(',', new object[]{
                    customer.CustomerId,
                    customer.CustomerName,
                    customer.CustomerContact,
                    customer.CustomerEmailId,
                    customer.DeviceId,
                    });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return csvFormatData;

        }

        public bool DeleteFromFile(string id, string filepath)
        {
            return new CsvInputHandler().DeleteFromFile(id, filepath);
        }
    }
}
