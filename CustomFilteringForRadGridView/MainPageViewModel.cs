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
        private List<StoreData> _storeData;
        private List<DivisionData> _divisionData;
        private List<RegionData> _regionData;
        private int _selectedRegionId;

        public int SelectedRegionId
        {
            get { return _selectedRegionId; }
            set
            {
                _selectedRegionId = value;
                OnPropertyChanged("SelectedRegionId");
            }
        }

        public List<StoreData> StoreData
        {
            get { return _storeData; }
            set
            {
                _storeData = value;
                OnPropertyChanged("Data");
            }
        }

        public List<DivisionData> DivisionData
        {
            get { return _divisionData; }
            set
            {
                _divisionData = value;
                OnPropertyChanged("DivisionData");
            }
        }

        public List<RegionData> RegionData
        {
            get { return _regionData; }
            set
            {
                _regionData = value;
                OnPropertyChanged("RegionData");
            }
        }

        public MainPageViewModel()
        {
            GenerateData();
        }

        private void GenerateData()
        {
            var random = new Random();
            var storeData = new List<StoreData>();
            var regionData = new List<RegionData>();
            var divisionData = new List<DivisionData>();

            for (int i = 0; i < 100; i++)
            {
                storeData.Add(new StoreData
                                   {
                                       StoreName = "Store " + i,
                                       RegionId = (i/10) + 1,
                                       DivisionId = (i/5) + 1,
                                       Value = random.Next(0, 99999999)
                                   });

                if(i % 5 == 0)
                {
                    divisionData.Add(new DivisionData
                                         {
                                             DivisionName = "Division " + (i + 1),
                                             DivisionId = i + 1,
                                             RegionId = (i / 10) + 1
                                         });
                }

                if (i % 10 == 0)
                {
                    regionData.Add(new RegionData
                                       {
                                           RegionName = "Region " + (i + 1),
                                           RegionId = i + 1
                                       });
                }
            }

            StoreData = storeData;
            DivisionData = divisionData;
            RegionData = regionData;
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
