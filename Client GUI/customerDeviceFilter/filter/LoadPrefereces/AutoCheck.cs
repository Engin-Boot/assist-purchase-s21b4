using System.Windows.Controls;
using System.Windows.Automation.Peers;
using UI_Customer.Views;
using System.Windows.Automation.Provider;

namespace UI_Customer.LoadPrefereces
{
    public static class AutoCheck
    {
        
        public static void fun(DataModels.FilterDataModel f)
        {
            //CheckBox cb1 = new CheckBox();
            //cb1.Name = "ECG";
            //cb1.Content = "ECG";
            //MainWindow.AppWindow.filter1Stack.Children.Add(cb1);

            autoCheckMeasurements(f);
            autoCheckWeight(f);
            autoCheckResolution(f);
            autoCheckBatteryCapacity(f);
            

        }
        public static void autoCheckMeasurements(DataModels.FilterDataModel f)
        {
            ShowingDevices _showingDevicesobj = new ShowingDevices();

            foreach (CheckBox el in _showingDevicesobj.filter1Stack.Children)
            {
                foreach (var fil in f.measurements)
                {
                    if (el.Name == fil)
                    {
                        CheckBoxAutomationPeer peer = new CheckBoxAutomationPeer(el);

                        IToggleProvider toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
                        toggleProvider.Toggle();


                    }
                }

            }
        }
        public static void autoCheckWeight(DataModels.FilterDataModel f)
        {
            ShowingDevices _showingDevicesobj = new ShowingDevices();
            foreach (CheckBox el in _showingDevicesobj.filter2Stack.Children)
            {
                string[] limits = el.Content.ToString().Split(' ');
                foreach (var weight in f.weight)
                {
                    if (weight.Equals(el.Content.ToString().Split(' ')[0]))
                    {
                        CheckBoxAutomationPeer peer = new CheckBoxAutomationPeer(el);

                        IToggleProvider toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
                        toggleProvider.Toggle();


                    }
                }

            }
        }
        public static void autoCheckResolution(DataModels.FilterDataModel f)
        {
            ShowingDevices _showingDevicesobj = new ShowingDevices();
            foreach (CheckBox el in _showingDevicesobj.filter3Stack.Children)
            {
                foreach (var fil in f.resolution)
                {
                    if (el.Content.Equals(fil))
                    {
                        CheckBoxAutomationPeer peer = new CheckBoxAutomationPeer(el);

                        IToggleProvider toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
                        toggleProvider.Toggle();


                    }
                }

            }
        }
        public static void autoCheckBatteryCapacity(DataModels.FilterDataModel f)
        {
            ShowingDevices _showingDevicesobj = new ShowingDevices();
            foreach (CheckBox el in _showingDevicesobj.filter4Stack.Children)
            {
                foreach (var fil in f.batterycapacity)
                {
                    if ((int)el.Content==int.Parse(fil))
                    {
                        CheckBoxAutomationPeer peer = new CheckBoxAutomationPeer(el);

                        IToggleProvider toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
                        toggleProvider.Toggle();


                    }
                }

            }
        }


    }
}
