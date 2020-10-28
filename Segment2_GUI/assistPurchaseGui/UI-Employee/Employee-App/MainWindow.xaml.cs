using System;
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

using System.Net;
using System.Windows.Automation.Peers;
using Employee_App.DataModels;
using System.Windows.Media.TextFormatting;

namespace Employee_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = DummyDevice;
        }

        private void Button_Click_GetAllDevices(object sender, RoutedEventArgs e)
        {
            CustomersStack.Children.Clear();
            if (this.DeviceOperationStack.Visibility == Visibility.Visible)
            {
                DeviceOperationStack.Visibility = Visibility.Collapsed;
            }
            DataModels.DeviceModel[] d1 = ServerConnection.Devices.getAllDevices();
            StackPanel innerStack = new StackPanel
            {
                Orientation = Orientation.Vertical

            };

            foreach (var c in d1)
            {
                TextBlock cb = new TextBlock();
                cb.Margin = new Thickness(10);
                string s = "ID :    " + c.id + "\n" + "NAME :    " + c.name + "\n" + "OVERVIEW :    " + c.overview + "\n" +
                    "MEASUREMENTS :    " + string.Join(",", c.measurements) + "\n" + "WEIGHT :    " + c.weight + "\n" + "BATTERY :    "
                    + c.batterycapacity + "\n" + "RESOLUTION :    " + c.resolution;
                cb.Text = s;
                Border b = new Border();
                b.BorderBrush = new SolidColorBrush(Colors.Black);
                b.BorderThickness = new Thickness(4);
                b.Margin = new Thickness(10);
                b.Background = new SolidColorBrush(Colors.LightBlue);
                b.Child = cb;

                innerStack.Children.Add(b);

            }
            DevicesStack.Children.Clear();
            DevicesStack.Children.Add(innerStack);

        }
        DataModels.DummyDeviceModel DummyDevice = new DataModels.DummyDeviceModel();
        private void Button_Click_search(object sender, RoutedEventArgs e)
        {
            if (this.DummyDevice.Id == null)
            {
                MessageBox.Show("Please enter some id . This field can't be blank . ");
                return;
            }

            DataModels.DeviceModel d3 = ServerConnection.Devices.getDeviceById(this.DummyDevice.Id);
            if (d3 == null)
            {
                MessageBox.Show(" No such device Present .PLease enter a valid device id. ");
                return;
            }
            string textboxmeasure = "";
            foreach (string ss in d3.measurements)
            {
                textboxmeasure = textboxmeasure + ss + ",";
            }
            textboxmeasure = textboxmeasure.Substring(0, textboxmeasure.Length - 1);
            this.DummyDevice.Id = d3.id;
            this.DummyDevice.Name = d3.name;
            this.DummyDevice.Overview = d3.overview;
            this.DummyDevice.Resolution = d3.resolution;
            this.DummyDevice.Weight = d3.weight;
            this.DummyDevice.Batterycapacity = d3.batterycapacity;
            this.DummyDevice.Measure = textboxmeasure;
            MessageBox.Show(" Search successful ! ");
        }

        private void Button_Click_home(object sender, RoutedEventArgs e)
        {
            CustomersStack.Children.Clear();
            DevicesStack.Children.Clear();
            if (this.DeviceOperationStack.Visibility == Visibility.Collapsed)
            {
                DeviceOperationStack.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_add(object sender, RoutedEventArgs e)
        {
            string idDel = this.DummyDevice.Id;
            DataModels.DeviceModel d3 = ServerConnection.Devices.getDeviceById(this.DummyDevice.Id);
            if (d3 != null)
            {
                MessageBox.Show(" A device with this id is already present . PLease use a different id . ");
                return;
            }
            DeviceModel device = new DeviceModel();
            device.id = this.DummyDevice.Id;
            device.name = this.DummyDevice.Name;
            device.overview = this.DummyDevice.Overview;
            device.resolution = this.DummyDevice.Resolution;
            device.batterycapacity = this.DummyDevice.Batterycapacity;
            device.weight = this.DummyDevice.Weight;
            List<string> actualmeasure = this.DummyDevice.Measure.Split(',').ToList();
            device.measurements = new List<string>(actualmeasure);

            ServerConnection.Devices.addDevice(device);
        }
        private void Button_Click_modify(object sender, RoutedEventArgs e)
        {
            string idDel = this.DummyDevice.Id;
            DataModels.DeviceModel d3 = ServerConnection.Devices.getDeviceById(this.DummyDevice.Id);
            if (d3 == null)
            {
                MessageBox.Show(" No such device Present .PLease enter a valid device id. ");
                return;
            }
            DeviceModel device = new DeviceModel();
            device.id = this.DummyDevice.Id;
            device.name = this.DummyDevice.Name;
            device.overview = this.DummyDevice.Overview;
            device.resolution = this.DummyDevice.Resolution;
            device.batterycapacity = this.DummyDevice.Batterycapacity;
            device.weight = this.DummyDevice.Weight;
            List<string> actualmeasure2 = this.DummyDevice.Measure.Split(',').ToList();
            device.measurements = new List<string>(actualmeasure2);
            ServerConnection.Devices.modifyDevice(idDel, device);
        }

        private void Button_Click_del(object sender, RoutedEventArgs e)
        {

            string idDel = this.DummyDevice.Id;
            DataModels.DeviceModel d3 = ServerConnection.Devices.getDeviceById(this.DummyDevice.Id);
            if (d3 == null)
            {
                MessageBox.Show(" No such device Present .PLease enter a valid device id. ");
                return;
            }
            ServerConnection.Devices.deleteDevice(idDel);
        }

        private void Button_Click_GetAllCustomers(object sender, RoutedEventArgs e)
        {
            DevicesStack.Children.Clear();

            if (this.DeviceOperationStack.Visibility == Visibility.Visible)
            {
                DeviceOperationStack.Visibility = Visibility.Collapsed;
            }
            DataModels.CustomerModel[] d1 = ServerConnection.Customers.getAllCustomers();
            if (d1 == null)
            {
                MessageBox.Show("No Customers At the Moment");
            }
            StackPanel innerStack = new StackPanel
            {
                Orientation = Orientation.Vertical

            };

            foreach (var c in d1)
            {
                TextBlock cb = new TextBlock();
                cb.Margin = new Thickness(10);
                string s = "ID :    " + c.customerId + "\n" + "NAME :    " + c.customerName + "\n" + "CONTACT :    " + c.customerContact +
                    "\n" + "EMAIL ID :    " + c.customerEmailId + "\n" + "DEVICE ID  :    " + c.deviceId;
                cb.Text = s;
                Border b = new Border();
                b.BorderBrush = new SolidColorBrush(Colors.Black);
                b.BorderThickness = new Thickness(4);
                b.Margin = new Thickness(10);
                b.Background = new SolidColorBrush(Colors.LightBlue);
                b.Child = cb;

                innerStack.Children.Add(b);

            }
            CustomersStack.Children.Clear();
            CustomersStack.Children.Add(innerStack);
        }

        private void Button_Click_clear(object sender, RoutedEventArgs e)
        {
            this.DummyDevice.Id = "";
            this.DummyDevice.Name = "";
            this.DummyDevice.Overview = "";
            this.DummyDevice.Weight = 0;
            this.DummyDevice.Batterycapacity = "";
            this.DummyDevice.Measure = "";
            this.DummyDevice.Resolution = "";
        }
    }
}
