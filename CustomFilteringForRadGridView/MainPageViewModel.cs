using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CustomFilteringForRadGridView
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private List<SampleData> _data;
        public List<SampleData> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }
        

        public MainPageViewModel()
        {
            Data = GenerateData();
        }

        private static List<SampleData> GenerateData()
        {
            var random = new Random();
            var sampleData = new List<SampleData>();

            for (int i = 0; i < 100; i++)
            {
                sampleData.Add(new SampleData
                                   {
                                       StoreName = "Store " + i,
                                       RegionId = (i/10) + 1,
                                       DivisionId = (i/5) + 1,
                                       Value = random.Next(0, 99999999)
                                   });
            }

            return sampleData;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
