﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI_Customer.DataModels;
using UI_Customer.ViewModels;

namespace UI_Customer.Views
{
    /// <summary>
    /// Interaction logic for TakingCustomerDetails.xaml
    /// </summary>
    public partial class TakingCustomerDetails : UserControl
    {
        TakingCustomerDetailsViewModel currentCustomer;
        public TakingCustomerDetails(string _deviceID)
        {
            InitializeComponent();
            Random ran = new Random();
            int customerIDinInt = ran.Next();
            string _customerID = customerIDinInt.ToString();
            this.currentCustomer = new TakingCustomerDetailsViewModel(_deviceID, _customerID);
            
            this.DataContext = currentCustomer;   
        }

        private void sendingDetails_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show(this.currentCustomer.CustomerId);
            CustomerModel _customerFinal = new CustomerModel(this.currentCustomer.DeviceId, this.currentCustomer.CustomerId);
            _customerFinal.CustomerName = this.currentCustomer.CustomerName;
            _customerFinal.CustomerContact = this.currentCustomer.CustomerContact;
            _customerFinal.CustomerEmailId = this.currentCustomer.CustomerEmailId;

            ServerConnection.AddCustomerDetails.AddCustomer(_customerFinal);
            ServerConnection.AddCustomerDetails.SendMail(_customerFinal);
        }
    }
}
