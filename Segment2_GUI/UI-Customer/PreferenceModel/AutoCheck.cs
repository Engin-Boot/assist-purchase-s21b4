﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using UI_Customer.Views;

namespace UI_Customer.PreferenceModel
{
    public static class AutoCheck
    {
        public static void autocheckFilterPreferences(DataModels.FilterDataModel filters)
        {
            autoCheckMeasurements(filters);
            autoCheckWeight(filters);
            autoCheckResolution(filters);
            autoCheckBatteryCapacity(filters);
        }
        public static void autoCheckMeasurements(DataModels.FilterDataModel filters)
        {
            //ShowingDevices _devicesobj = new ShowingDevices();

            foreach (CheckBox cb in ShowingDevices.AppWindow.filter1Stack.Children)
            {
                foreach (var measurement in filters.measurements)
                {
                    if (cb.Content.Equals(measurement))
                    {
                        CheckBoxAutomationPeer peer = new CheckBoxAutomationPeer(cb);
                        IToggleProvider toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
                        toggleProvider.Toggle();
                    }
                }
            }
        }
        public static void autoCheckWeight(DataModels.FilterDataModel filters)
        {
            //ShowingDevices _devicesobj = new ShowingDevices();
            foreach (CheckBox cb in ShowingDevices.AppWindow.filter2Stack.Children)
            {
                foreach (var weight in filters.weight)
                {
                    if (weight.Equals(cb.Content.ToString().Split(' ')[0]))
                    {
                        CheckBoxAutomationPeer peer = new CheckBoxAutomationPeer(cb);
                        IToggleProvider toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
                        toggleProvider.Toggle();
                    }
                }
            }
        }
        public static void autoCheckResolution(DataModels.FilterDataModel filters)
        {
            //ShowingDevices _devicesobj = new ShowingDevices();
            foreach (CheckBox cb in ShowingDevices.AppWindow.filter3Stack.Children)
            {
                foreach (var resolution in filters.resolution)
                {
                    if (cb.Content.Equals(resolution))
                    {
                        CheckBoxAutomationPeer peer = new CheckBoxAutomationPeer(cb);
                        IToggleProvider toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
                        toggleProvider.Toggle();
                    }
                }
            }
        }
        public static void autoCheckBatteryCapacity(DataModels.FilterDataModel filters)
        {
            //ShowingDevices _devicesobj = new ShowingDevices();
            foreach (CheckBox cb in ShowingDevices.AppWindow.filter4Stack.Children)
            {
                foreach (var fil in filters.batterycapacity)
                {
                    if ((int)cb.Content == int.Parse(fil))
                    {
                        CheckBoxAutomationPeer peer = new CheckBoxAutomationPeer(cb);
                        IToggleProvider toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
                        toggleProvider.Toggle();
                    }
                }

            }
        }

    }
}
