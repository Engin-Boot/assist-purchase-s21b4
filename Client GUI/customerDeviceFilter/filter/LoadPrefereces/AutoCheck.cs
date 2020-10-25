using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using System.Windows;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace filter.LoadPrefereces
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
            
            foreach (CheckBox el in MainWindow.AppWindow.filter1Stack.Children)
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
            foreach (CheckBox el in MainWindow.AppWindow.filter2Stack.Children)
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
            foreach (CheckBox el in MainWindow.AppWindow.filter3Stack.Children)
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
            foreach (CheckBox el in MainWindow.AppWindow.filter4Stack.Children)
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
