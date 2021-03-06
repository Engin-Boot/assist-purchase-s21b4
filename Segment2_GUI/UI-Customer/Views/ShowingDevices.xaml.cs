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
using UI_Customer.ViewModels;
using System.Net;
using UI_Customer.DataModels;
using System.ComponentModel;
using UI_Customer.PreferenceModel;

namespace UI_Customer.Views
{
    public partial class ShowingDevices : UserControl
    {
        List<DataModels.weightRanges> weightRangesToShow = getWeightRangesToShow();

        DataModels.FilterDataModel filters = new DataModels.FilterDataModel();
        List<string> initialMeasurementsTOShow = new List<string>();
        DataModels.DeviceModel[] UpdatedListOfDevices = ServerConnection.Devices.getAllDevices();
        DataModels.DeviceModel[] PassedToStack2_weightsStack = ServerConnection.Devices.getAllDevices();
        DataModels.DeviceModel[] PassedToStack3_resolutionStack = ServerConnection.Devices.getAllDevices();
        DataModels.DeviceModel[] PassedToStack4_batteryCapacityStack = ServerConnection.Devices.getAllDevices();
        public static ShowingDevices AppWindow;
        public ShowingDevices()
        {
            InitializeComponent();
            AppWindow = this;
            DataModels.DeviceModel[] ListOfDevices = ServerConnection.Devices.getAllDevices();
            if (ListOfDevices.Length == 0)
            {
                MessageBox.Show("Devices not found!");
                Window win = new Window();
                win.Close();
                
            }
            else
            {
                AddDevicesToDeviceStackPanel(ListOfDevices);
            }

        }

        public void AddDevicesToDeviceStackPanel(DataModels.DeviceModel[] listOfDevices)
        {
            StackPanel innerStack = new StackPanel
            {
                Orientation = Orientation.Vertical

            };
            Border myBorder1 = new Border();
            myBorder1.BorderBrush = Brushes.Black;
            myBorder1.BorderThickness = new Thickness(1);

            innerStack.Margin = new System.Windows.Thickness(4, 4, 4, 4);
            foreach (var singleDevice in listOfDevices)
            {
                TextBlock cb = new TextBlock();
                Button _button = new Button();
                string listOfMeasurements = null;
                foreach (var singleMeasurement in singleDevice.measurements)
                {
                    listOfMeasurements += singleMeasurement + " ";
                }
                string s = singleDevice.name + "\n" + singleDevice.overview + "\n" + listOfMeasurements + "\n";
                cb.Text = s;
                _button.Content = "Interested? Contact Us for " + singleDevice.name;
                _button.HorizontalAlignment = HorizontalAlignment.Right;
                _button.Name = singleDevice.id;
                _button.Click += (object sender, RoutedEventArgs e) => { OnInterestedButtonClick(sender, e, singleDevice.id); };
                Border b = new Border();
                b.BorderBrush = new SolidColorBrush(Colors.Blue);
                b.BorderThickness = new Thickness(4);
                b.Child = cb;
                innerStack.Children.Add(b);
                innerStack.Children.Add(_button);
            }
            DevicesStack.Children.Add(innerStack);

        }

        public void OnInterestedButtonClick(object sender, RoutedEventArgs e, string id)
        {
            TakingCustomerDetails _addingDetails = new TakingCustomerDetails(id);
            Window win = new Window();
            win.Title = "CustomerDetails";
            win.Content = _addingDetails;
            win.Show();
        }

        public void Assistant_Click(object sender1, EventArgs e1)
        {
            filter1StackLabel.Children.Clear();
            filter1Stack.Children.Clear();
            filter2StackLabel.Children.Clear();
            filter2Stack.Children.Clear();
            filter3StackLabel.Children.Clear();
            filter3Stack.Children.Clear();
            filter4StackLabel.Children.Clear();
            filter4Stack.Children.Clear();
            DevicesStack.Children.Clear();
            DataModels.DeviceModel[] ListOfDevices = ServerConnection.Devices.getAllDevices();
            if (ListOfDevices.Length == 0)
            {
                MessageBox.Show("Devices not found!");
                Window win = new Window();
                win.Close();

            }
            AddDevicesToDeviceStackPanel(ListOfDevices);
            initialMeasurementsTOShow = ServerConnection.Filters.getMeasurementsInAllDevices();
            addInitialMeasuremntstoMeasurementStack();
            AddDevicesToDeviceStackPanel(UpdatedListOfDevices);
            void addInitialMeasuremntstoMeasurementStack()
            {
                if (initialMeasurementsTOShow.Count > 0)
                {
                    TextBlock t = new TextBlock { Text = "Select Measurement " };
                    filter1StackLabel.Children.Add(t);

                    foreach (var measure in initialMeasurementsTOShow)
                    {
                        CheckBox cb_temp = new CheckBox();
                        cb_temp.Content = measure;
                        cb_temp.Checked += new RoutedEventHandler(measurement_filter_CheckBox_Clicked);
                        cb_temp.Unchecked += new RoutedEventHandler(measurement_filter_CheckBox_Clicked);
                        filter1Stack.Children.Add(cb_temp);
                    }
                }
            }

            var _filterPreferences = Preferences.getFilterPreferencesByIp();
            AutoCheck.autocheckFilterPreferences(_filterPreferences);
        }
        void measurement_filter_CheckBox_Clicked(object sender, EventArgs e)
        {
            CheckBox measurement_checkbox = (sender as CheckBox);

            if ((bool)measurement_checkbox.IsChecked)
            {
                filters.measurements.Add(measurement_checkbox.Content.ToString());
            }
            else
            {
                filters.measurements.Remove(measurement_checkbox.Content.ToString());
            }
            filters.weight.Clear();
            filters.resolution.Clear();
            filters.batterycapacity.Clear();
            Preferences.SaveFilterPreferencesForIp(filters);
            updateDeviceStack();
            filter2StackLabel.Children.Clear();
            filter2Stack.Children.Clear();
            filter3StackLabel.Children.Clear();
            filter3Stack.Children.Clear();
            filter4StackLabel.Children.Clear();
            filter4Stack.Children.Clear();
            if (filters.measurements.Count > 0)
            {
                AddFilterStack2_weightStack();

            }

        }
        void AddFilterStack2_weightStack()
        {

            if (UpdatedListOfDevices.Length > 0)
            {
                PassedToStack2_weightsStack = UpdatedListOfDevices;
                List<int> selectedRanges = new List<int>();

                selectedRanges = GetRangesToBeSelected(UpdatedListOfDevices);
                Comparor comparorObj = new Comparor();
                selectedRanges.Sort(comparorObj);
                TextBlock FilterLabel = new TextBlock { Text = "  Select Weight range   " };
                filter2StackLabel.Children.Add(FilterLabel);
                foreach (var range in selectedRanges)
                {
                    CheckBox cb_temp = new CheckBox();
                    cb_temp.Content = weightRangesToShow[range].content;
                    cb_temp.Checked += new RoutedEventHandler(weight_filter_CheckBox_Clicked);
                    cb_temp.Unchecked += new RoutedEventHandler(weight_filter_CheckBox_Clicked);
                    filter2Stack.Children.Add(cb_temp);
                }
            }
        }
        List<int> GetRangesToBeSelected(DataModels.DeviceModel[] devices)
        {
            List<int> selectedRanges = new List<int>();
            foreach (var device in UpdatedListOfDevices)
            {
                int indexOfRangeToSelect = getIndexOfRangeByWeightValue(device.weight);
                if (!selectedRanges.Contains(indexOfRangeToSelect))
                {
                    selectedRanges.Add(indexOfRangeToSelect);
                }
            }
            return selectedRanges;
        }
        int getIndexOfRangeByWeightValue(float weight)
        {
            int indexOfRangeToSelect = (int)Math.Floor(weight / 10);
            if (weight % 10.0 == 0 && weight > 0)
            {
                indexOfRangeToSelect -= 1;
            }
            return indexOfRangeToSelect;
        }
        void weight_filter_CheckBox_Clicked(object sender, EventArgs e)
        {
            CheckBox weight_checkbox = (sender as CheckBox);
            if ((bool)weight_checkbox.IsChecked)
            {
                string checkbox_content = weight_checkbox.Content.ToString();
                string[] checkbox_content_split = checkbox_content.Split(' ');
                filters.weight.Add(checkbox_content_split[0]);
            }
            else
            {
                string checkbox_content = weight_checkbox.Content.ToString();
                string[] checkbox_content_split = checkbox_content.Split(' ');
                filters.weight.Remove(checkbox_content_split[0]);
            }
            filters.resolution.Clear();
            filters.batterycapacity.Clear();
            Preferences.SaveFilterPreferencesForIp(filters);
            updateDeviceStack();
            filter3StackLabel.Children.Clear();
            filter3Stack.Children.Clear();
            filter4StackLabel.Children.Clear();
            filter4Stack.Children.Clear();
            if (filters.weight.Count > 0)
            {
                AddFilterStack3_resolutionStack();

            }

        }
        void AddFilterStack3_resolutionStack()
        {
            if (UpdatedListOfDevices.Length > 0)
            {
                PassedToStack3_resolutionStack = UpdatedListOfDevices;
                TextBlock FilterLabel = new TextBlock { Text = "  Select Resolution   " };
                filter3StackLabel.Children.Add(FilterLabel);

                //Build the item list
                List<string> uniqueResolutions = new List<string>();
                foreach (var device in UpdatedListOfDevices)
                {
                    if (!uniqueResolutions.Contains(device.resolution))
                        uniqueResolutions.Add(device.resolution);

                }

                foreach (var resolution in uniqueResolutions)
                {
                    CheckBox cb_temp = new CheckBox();
                    cb_temp.Content = resolution;
                    cb_temp.Checked += new RoutedEventHandler(resolution_filter_CheckBox_Clicked);
                    cb_temp.Unchecked += new RoutedEventHandler(resolution_filter_CheckBox_Clicked);
                    filter3Stack.Children.Add(cb_temp);
                }
            }
        }
        void resolution_filter_CheckBox_Clicked(object sender, EventArgs e)
        {
            CheckBox resolution_checkbox = (sender as CheckBox);
            if ((bool)resolution_checkbox.IsChecked)
            {
                filters.resolution.Add(resolution_checkbox.Content.ToString());
            }
            else
            {
                filters.resolution.Remove(resolution_checkbox.Content.ToString());
            }
            filters.batterycapacity.Clear();
            Preferences.SaveFilterPreferencesForIp(filters);
            updateDeviceStack();
            filter4StackLabel.Children.Clear();
            filter4Stack.Children.Clear();
            if (filters.resolution.Count > 0)
            {
                AddFilterStack4_batteryCapcityStack();
            }
        }
        void AddFilterStack4_batteryCapcityStack()
        {
            if (UpdatedListOfDevices.Length > 0)
            {
                PassedToStack4_batteryCapacityStack = UpdatedListOfDevices;
                TextBlock FilterLabel = new TextBlock { Text = "  Select Battery Capacity  " };
                filter4StackLabel.Children.Add(FilterLabel);

                //Build the item list
                List<int> uniqueBatteryCapacities = new List<int>();
                foreach (var device in UpdatedListOfDevices)
                {
                    if (!uniqueBatteryCapacities.Contains(int.Parse(device.batterycapacity)))
                        uniqueBatteryCapacities.Add(int.Parse(device.batterycapacity));

                }
                Comparor comparorObj = new Comparor();
                uniqueBatteryCapacities.Sort(comparorObj);
                foreach (var batteryCapacity in uniqueBatteryCapacities)
                {
                    CheckBox cb = new CheckBox();
                    cb.Content = batteryCapacity;
                    cb.Checked += new RoutedEventHandler(battery_filter_CheckBox_Clicked);
                    cb.Unchecked += new RoutedEventHandler(battery_filter_CheckBox_Clicked);
                    filter4Stack.Children.Add(cb);
                }
            }
        }
        void battery_filter_CheckBox_Clicked(object sender, EventArgs e)
        {
            CheckBox batteryCapacity = (sender as CheckBox);
            if ((bool)batteryCapacity.IsChecked)
            {
                filters.batterycapacity.Add(batteryCapacity.Content.ToString());
            }
            else
            {
                filters.batterycapacity.Remove(batteryCapacity.Content.ToString());
            }
            Preferences.SaveFilterPreferencesForIp(filters);
            updateDeviceStack();
        }

        void updateDeviceStack()
        {
            DevicesStack.Children.Clear();
            UpdatedListOfDevices = ServerConnection.Devices.getFilterdDevices(filters);
            AddDevicesToDeviceStackPanel(UpdatedListOfDevices);
        }
        public static List<DataModels.weightRanges> getWeightRangesToShow()
        {
            List<DataModels.weightRanges> weightRangesToShow = new List<DataModels.weightRanges>();
            for (int i = 1; i <= 91; i += 10)
            {
                DataModels.weightRanges temp = new DataModels.weightRanges();
                temp.min = i;
                temp.max = i + 9;
                temp.content = i.ToString() + "-" + (i + 9).ToString() + " Kg";
                weightRangesToShow.Add(temp);
            }
            return weightRangesToShow;
        }


    }

    class Comparor : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x == 0 || y == 0)
            {
                return 0;
            }
            return x.CompareTo(y);
        }
    }

}
