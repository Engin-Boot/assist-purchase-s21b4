using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Employee_App.DataModels
{
    public class DummyDeviceModel : INotifyPropertyChanged
    {
        string name, id, overview, batterycapacity, resolution, measure;
        List<string> measurements;
        float weight;

        public string Measure
        {
            get { return this.measure; }
            set
            {
                if (value != this.measure)
                {
                    this.measure = value;
                    OnPropertyChanged("Measure");
                }
            }
        }
        public string Id
        {
            get { return this.id; }
            set
            {
                if (value != this.id)
                {
                    this.id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Overview
        {
            get { return this.overview; }
            set
            {
                if (value != this.overview)
                {
                    this.overview = value;
                    OnPropertyChanged("Overview");
                }
            }
        }
        public string Name
        {
            get { return this.name; }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public List<string> Measurements
        {
            get { return this.measurements; }
            set
            {
                if (!value.Equals(this.measurements))
                {
                    this.measurements = value.ToList();
                    OnPropertyChanged("Measurements");
                    string mm = "" + this.measurements.Count;
                    MessageBox.Show(this.measurements[0]);
                }
            }
        }
        public float Weight
        {
            get { return this.weight; }
            set
            {
                if (value != this.weight)
                {
                    this.weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }
        public string Batterycapacity
        {
            get { return this.batterycapacity; }
            set
            {
                if (value != this.batterycapacity)
                {
                    this.batterycapacity = value;
                    OnPropertyChanged("Batterycapacity");
                }
            }
        }
        public string Resolution
        {
            get { return this.resolution; }
            set
            {
                if (value != this.resolution)
                {
                    this.resolution = value;
                    OnPropertyChanged("Resolution");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
