using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UI_Customer.DataModels;

namespace UI_Customer.ViewModels
{
    [DataContract]
    public class TakingCustomerDetailsViewModel : INotifyPropertyChanged
    {
        CustomerModel _customer;
        public TakingCustomerDetailsViewModel()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public TakingCustomerDetailsViewModel(string _deviceid, string _customerid)
        {
            this._customer = new CustomerModel(_deviceid, _customerid);
        }
        [DataMember]
        public string CustomerId
        {
            get
            {
                return this._customer.CustomerId;
            }

            set
            {
                //this.CustomerId = value;
                OnPropertyChanged("CustomerId");
            }
        }
        [DataMember]
        public string CustomerName
        {
            get
            {
                return this._customer.CustomerName;
            }

            set
            {
                this._customer.CustomerName = value;
                OnPropertyChanged("CustomerName");
            }
        }
        [DataMember]
        public string CustomerContact
        {
            get
            {
                return this._customer.CustomerContact;
            }

            set
            {
                this._customer.CustomerContact = value;
                OnPropertyChanged("CustomerContact");
            }
        }
        [DataMember]
        public string CustomerEmailId
        {
            get
            {
                return this._customer.CustomerEmailId;

            }

            set
            {
                this._customer.CustomerEmailId = value;
                OnPropertyChanged("CustomerEmailId");
            }
        }
        [DataMember]
        public string DeviceId
        {
            get
            {
                return this._customer.DeviceId;
            }

            set
            {
                //this._customer.DeviceId = value;
                OnPropertyChanged("DeviceId");
            }
        }


    }
}
