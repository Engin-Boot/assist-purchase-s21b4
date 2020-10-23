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
using DataModels;


namespace filter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class GFG : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (x == 0 || y == 0)
                {
                    return 0;
                }

                // CompareTo() method 
                return x.CompareTo(y);

            }
        }
        public MainWindow()
        {
            List<DataModels.weightRanges> weightRanges1 = new List<DataModels.weightRanges>();
            for (int i = 1; i <= 91; i += 10) 
            {
                DataModels.weightRanges t = new DataModels.weightRanges();
                t.min = i;
                t.max = i + 9;
                t.content = i.ToString() + "-" + (i + 9).ToString() + " Kg";
                weightRanges1.Add(t);
            }
            




            DataModels.FilterDataModel filters = new DataModels.FilterDataModel();

            InitializeComponent();
            List<string> m = new List<string>();

            m = ServerConnection.GetFiltersOptions.getMeasurementFeatures();
            DataModels.DeviceModel[] UpdatedListOfDevices = ServerConnection.GetDevices.getAllDevices();
            DataModels.DeviceModel[] PassedToStack2 = UpdatedListOfDevices;
            DataModels.DeviceModel[] PassedToStack3 = UpdatedListOfDevices;
            DataModels.DeviceModel[] PassedToStack4 = UpdatedListOfDevices;



            addMeasuremntstoFilterStack();



            

            //DataModels.DeviceModel[] passedToStack2;
            AddDevicesToDeviceStackPanel(UpdatedListOfDevices);
            

            void addMeasuremntstoFilterStack()
            {
                
                StackPanel innerStack;
                innerStack = new StackPanel
                {
                    Orientation = Orientation.Vertical
                };
            

                foreach (var c in m)
                {
                    CheckBox cb = new CheckBox();
                    cb.Name = c;
                    cb.Content = c;

                    cb.Click += new RoutedEventHandler(filter_CheckBox_Clicked);
           
                    innerStack.Children.Add(cb);
                }
                filter1Stack.Children.Add(innerStack);
                
            }
            void filter_CheckBox_Clicked(object sender, EventArgs e)
            {
                CheckBox chk = (sender as CheckBox);
                if ((bool)chk.IsChecked)
                {
                    filters.measurements.Add(chk.Content.ToString());
                }
                else
                {
                    filters.measurements.Remove(chk.Content.ToString());
                }
                filters.weight.Clear();
                filters.resolution.Clear();
                filters.batterycapacity.Clear();
                updateDeviceStack();
                filter2Stack.Children.Clear();
                filter3Stack.Children.Clear();
                filter4Stack.Children.Clear();
                if (filters.measurements.Count>0)
                {
                    AddFilterStack2();
                    
                }
                
            }
            void AddFilterStack2()
            {
                //if(UpdatedListOfDevices.Length>0)
                //{
                //    //StackPanel innerStack = new StackPanel
                //    //{ Orientation = Orientation.Vertical };

                //    Grid DynamicGrid = new Grid();

                //    DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
                //    DynamicGrid.VerticalAlignment = VerticalAlignment.Top;
                //    DynamicGrid.ShowGridLines = true;
                //    DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);

                //    ColumnDefinition gridCol1 = new ColumnDefinition();

                //    ColumnDefinition gridCol2 = new ColumnDefinition();


                //    DynamicGrid.ColumnDefinitions.Add(gridCol1);

                //    DynamicGrid.ColumnDefinitions.Add(gridCol2);

                //    RowDefinition gridRow1 = new RowDefinition();



                //    RowDefinition gridRow2 = new RowDefinition();

                //    DynamicGrid.RowDefinitions.Add(gridRow1);

                //    DynamicGrid.RowDefinitions.Add(gridRow2);

                //    TextBox tminLabel = new TextBox{Text = "Min:",};    Grid.SetRow(tminLabel, 0);Grid.SetColumn(tminLabel, 0);
                //    TextBox tmaxLabel = new TextBox { Text = "Max:", }; Grid.SetRow(tminLabel, 1); Grid.SetColumn(tminLabel, 0);

                //    TextBox tmin = new TextBox{Text = "0"};Grid.SetRow(tmin, 0);Grid.SetColumn(tmin, 1);tmin.TextChanged += new TextChangedEventHandler(min_Weight_changed);
                //    TextBox tmax = new TextBox{Text = "1000"}; Grid.SetRow(tmax, 1);Grid.SetColumn(tmax, 1);tmax.TextChanged += new TextChangedEventHandler(max_Weight_changed);
                //    minWeight = 0;
                //    maxWeight = 1000;
                //    DynamicGrid.Children.Add(tminLabel);
                //    DynamicGrid.Children.Add(tmaxLabel);
                //    DynamicGrid.Children.Add(tmin);
                //    DynamicGrid.Children.Add(tmax);

                //    TextBlock filter2Label = new TextBlock { Text="Select Weight"};
                //    filter2Stack.Children.Add(filter2Label);
                //    filter2Stack.Children.Add(DynamicGrid);
                //}

                //if (UpdatedListOfDevices.Length > 0)
                //{
                //    StackPanel innerStack;
                //    innerStack = new StackPanel
                //    {
                //        Orientation = Orientation.Vertical
                //    };

                //    //Build the item list
                //    List<float> items = new List<float>();
                //    foreach (var d in UpdatedListOfDevices)
                //    {
                //        if(!items.Contains(d.weight))
                //        items.Add(d.weight);

                //    }
                //    ComboBox resCB = new ComboBox();
                //    //Populate the ComboBox from the item list
                //    resCB.ItemsSource = items;
                //    innerStack.Children.Add(resCB);
                //    resCB.SelectionChanged += new SelectionChangedEventHandler(wight_Selected);
                //    filter2Stack.Children.Add(innerStack);
                //}
                if (UpdatedListOfDevices.Length > 0)
                {
                    PassedToStack2 = UpdatedListOfDevices;
                    List<int> selected = new List<int>();
                    StackPanel innerStack;
                    innerStack = new StackPanel
                    {
                        Orientation = Orientation.Vertical
                    };

                    //Build the item list
                    
                    foreach(var d in UpdatedListOfDevices)
                    {
                        int index = (int)Math.Floor(d.weight / 10);
                        if(d.weight%10.0==0 && d.weight>0)
                        {
                            index -= 1;
                        }
                        if(!selected.Contains(index))
                        {
                            selected.Add(index);
                        }
                    }
                    GFG gg = new GFG();
                    selected.Sort(gg);
                    TextBlock t = new TextBlock { Text = "Select Weight range" };
                    innerStack.Children.Add(t);
                    foreach (var c in selected)
                    {
                        CheckBox cb = new CheckBox();
                        
                        cb.Content = weightRanges1[c].content;

                        cb.Click += new RoutedEventHandler(weight_filter_CheckBox_Clicked);

                        innerStack.Children.Add(cb);
                    }
                    filter2Stack.Children.Add(innerStack);
                }

                
            }
            void weight_filter_CheckBox_Clicked(object sender, EventArgs e)
            {
                CheckBox chk = (sender as CheckBox);
                
                if ((bool)chk.IsChecked)
                {
                    string a = chk.Content.ToString();
                    string[] a1=a.Split(' ')[0].Split('-');
                    addWeightFilter(int.Parse(a1[0]), int.Parse(a1[1]),PassedToStack2);
                }
                else
                {
                    string a = chk.Content.ToString();
                    string[] a1 = a.Split(' ')[0].Split('-');
                    removeWeightFilter(int.Parse(a1[0]), int.Parse(a1[1]));
                }
                
                filters.resolution.Clear();
                filters.batterycapacity.Clear();
                updateDeviceStack();
                
                filter3Stack.Children.Clear();
                filter4Stack.Children.Clear();
                if (filters.weight.Count > 0)
                {
                    AddFilterStack3();

                }
                
            }
            void addWeightFilter(int min,int max,DataModels.DeviceModel[] passed)
            {
                foreach(var d in passed)
                {
                    if(d.weight<=max && d.weight>=min)
                    {
                        if(!filters.weight.Contains(d.weight))
                        {
                            filters.weight.Add(d.weight);
                        }
                    }
                }
            }
            void removeWeightFilter(int min, int max)
            {
                
                foreach (var w in filters.weight.ToList())
                {
                    if (w<=max && w>=min)
                    {
                        filters.weight.Remove(w);
                    }
                }
            }
            //void wight_Selected(object sender, SelectionChangedEventArgs e)
            //{
            //    ComboBox chk = (sender as ComboBox);
            //    filters.weight.Clear();
            //    filters.resolution.Clear();
            //    filters.batterycapacity.Clear();
            //    filters.weight.Add(float.Parse(chk.SelectedItem.ToString()));


            //    updateDeviceStack();
            //    filter3Stack.Children.Clear();
            //    filter4Stack.Children.Clear();
            //    if (filters.measurements.Count > 0)
            //    {
            //        AddFilterStack3();

            //    }
            //}
            //void min_Weight_changed(object sender, TextChangedEventArgs e)
            //{

            //    TextBox chk = (sender as TextBox);
            //    if(chk.Text.Length>0)
            //    {
            //        minWeight = int.Parse(chk.Text);
            //        Weight_changed();
            //    }

            //}
            //void max_Weight_changed(object sender, TextChangedEventArgs e)
            //{
            //    TextBox chk = (sender as TextBox);
            //    if (chk.Text.Length > 0)
            //    {
            //        maxWeight = int.Parse(chk.Text);
            //        Weight_changed();
            //    }
            //}

            //void Weight_changed()
            //{
            //    List<float> availableDevicesWeight = new List<float>();
            //    foreach(var u in UpdatedListOfDevices)
            //    {
            //        if(!availableDevicesWeight.Contains(u.weight))
            //        {
            //            availableDevicesWeight.Add(u.weight);
            //        }
            //    }
            //    List<float> filteredWeights = new List<float>();
            //    foreach(var v in availableDevicesWeight)
            //    {
            //        if(v>=minWeight && v<=maxWeight)
            //        {
            //            filteredWeights.Add(v);
            //        }
            //    }

            //    filters.weight = filteredWeights;
            //    updateDeviceStack();
            //    filter3Stack.Children.Clear();
            //    filter4Stack.Children.Clear();
            //}
            void AddFilterStack3()
            {
                //if(UpdatedListOfDevices.Length>0)
                //{
                //    StackPanel innerStack;
                //    innerStack = new StackPanel
                //    {
                //        Orientation = Orientation.Vertical
                //    };

                //    //Build the item list
                //    List<string> items = new List<string>();
                //    foreach (var d in UpdatedListOfDevices)
                //    {
                //        if (!items.Contains(d.resolution))
                //            items.Add(d.resolution);

                //    }
                //    ComboBox resCB = new ComboBox();
                //    //Populate the ComboBox from the item list
                //    resCB.ItemsSource = items;
                //    innerStack.Children.Add(resCB);
                //    resCB.SelectionChanged += new SelectionChangedEventHandler(resolution_selected);
                //    filter3Stack.Children.Add(innerStack);
                //}
                if (UpdatedListOfDevices.Length > 0)
                {
                    PassedToStack3 = UpdatedListOfDevices;
                    
                    StackPanel innerStack;
                    innerStack = new StackPanel
                    {
                        Orientation = Orientation.Vertical
                    };


                    
                    TextBlock t = new TextBlock { Text = "Select Resolution" };
                    innerStack.Children.Add(t);

                    //Build the item list
                    List<string> items = new List<string>();
                    foreach (var d in UpdatedListOfDevices)
                    {
                        if (!items.Contains(d.resolution))
                            items.Add(d.resolution);

                    }
                    
                    foreach (var c in items)
                    {
                        CheckBox cb = new CheckBox();

                        cb.Content = c;

                        cb.Click += new RoutedEventHandler(resolution_filter_CheckBox_Clicked);

                        innerStack.Children.Add(cb);
                    }
                    filter3Stack.Children.Add(innerStack);
                }
            }
            void resolution_filter_CheckBox_Clicked(object sender, EventArgs e)
            {
                CheckBox chk = (sender as CheckBox);
                if ((bool)chk.IsChecked)
                {
                    filters.resolution.Add(chk.Content.ToString());
                }
                else
                {
                    filters.resolution.Remove(chk.Content.ToString());
                }
                
                filters.batterycapacity.Clear();
                updateDeviceStack();
                filter4Stack.Children.Clear();
                if (filters.resolution.Count > 0)
                {
                    AddFilterStack4();

                }

            }
            //void resolution_selected(object sender, SelectionChangedEventArgs e)
            //{
            //    ComboBox chk = (sender as ComboBox);
            //    filters.resolution.Clear();
            //    filters.batterycapacity.Clear();
            //    filters.resolution.Add(chk.SelectedItem.ToString());


            //    updateDeviceStack();

            //    filter4Stack.Children.Clear();
            //    if (filters.measurements.Count > 0)
            //    {
            //        AddFilterStack4();

            //    }
            //}
            void AddFilterStack4()
            {
                //if (UpdatedListOfDevices.Length > 0)
                //{
                //    StackPanel innerStack;
                //    innerStack = new StackPanel
                //    {
                //        Orientation = Orientation.Vertical
                //    };

                //    //Build the item list
                //    List<string> items = new List<string>();
                //    foreach (var d in UpdatedListOfDevices)
                //    {
                //        if (!items.Contains(d.batterycapacity))
                //            items.Add(d.batterycapacity);

                //    }
                //    ComboBox resCB = new ComboBox();
                //    //Populate the ComboBox from the item list
                //    resCB.ItemsSource = items;
                //    innerStack.Children.Add(resCB);
                //    resCB.SelectionChanged += new SelectionChangedEventHandler(battery_selected);
                //    filter4Stack.Children.Add(innerStack);
                //}
                if (UpdatedListOfDevices.Length > 0)
                {
                    PassedToStack4 = UpdatedListOfDevices;
                    
                    StackPanel innerStack;
                    innerStack = new StackPanel
                    {
                        Orientation = Orientation.Vertical
                    };



                    TextBlock t = new TextBlock { Text = "Select Battery Capacity" };
                    innerStack.Children.Add(t);

                    //Build the item list
                    List<int> items = new List<int>();
                    foreach (var d in UpdatedListOfDevices)
                    {
                        if (!items.Contains(int.Parse(d.batterycapacity)))
                            items.Add(int.Parse(d.batterycapacity));

                    }
                    GFG gg = new GFG();
                    items.Sort(gg);
                    foreach (var c in items)
                    {
                        CheckBox cb = new CheckBox();

                        cb.Content = c;

                        cb.Click += new RoutedEventHandler(battery_filter_CheckBox_Clicked);

                        innerStack.Children.Add(cb);
                    }
                    filter4Stack.Children.Add(innerStack);
                }
            }
            void battery_filter_CheckBox_Clicked(object sender, EventArgs e)
            {
                CheckBox chk = (sender as CheckBox);
                if ((bool)chk.IsChecked)
                {
                    filters.batterycapacity.Add(chk.Content.ToString());
                }
                else
                {
                    filters.batterycapacity.Remove(chk.Content.ToString());
                }

                updateDeviceStack();
                
                

            }
            //void battery_selected(object sender, SelectionChangedEventArgs e)
            //{
            //    ComboBox chk = (sender as ComboBox);
            //    filters.batterycapacity.Clear();
            //    filters.batterycapacity.Add(chk.SelectedItem.ToString());


            //    updateDeviceStack();

            //}
            void updateDeviceStack()
            {
                DevicesStack.Children.Clear();
                UpdatedListOfDevices= ServerConnection.GetDevices.getFilterdDevices(filters);
                AddDevicesToDeviceStackPanel(UpdatedListOfDevices);
            }
            void AddDevicesToDeviceStackPanel(DataModels.DeviceModel[] d1)
            {

                
                StackPanel innerStack = new StackPanel
                {
                    Orientation = Orientation.Vertical

                };
                Border myBorder1 = new Border();
                myBorder1.BorderBrush = Brushes.Black;
                myBorder1.BorderThickness = new Thickness(1);
                //innerStack.Children.Add(myBorder1);
                innerStack.Margin = new System.Windows.Thickness(4, 4, 4, 4);
                foreach (var c in d1)
                {
                    TextBlock cb = new TextBlock();
                    string s = c.name + "\n" + c.overview+"\n"+c.measurements.ToString();
                    cb.Text = s;
                    Border b = new Border();
                    b.BorderBrush = new SolidColorBrush(Colors.Blue);
                    b.BorderThickness = new Thickness(4);
                    b.Child = cb;
                    innerStack.Children.Add(b);
                }
                DevicesStack.Children.Add(innerStack);

            }
        }

       

        

        

       
    }
}
