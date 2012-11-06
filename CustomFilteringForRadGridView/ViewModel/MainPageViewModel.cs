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
using HierarchyModel;
using HierarchyModel.Hierarchy;
using HierarchyModel.Model;
using Telerik.Windows.Controls;

namespace CustomFilteringForRadGridView
{
    public class MainPageViewModel : ViewModelBase
    {
        private int _selectedRegionId;
        private List<RegionHierarchy> _nationalHierarchy;
        private List<Store> _stores;

        public int SelectedRegionId
        {
            get { return _selectedRegionId; }
            set
            {
                _selectedRegionId = value;
                OnPropertyChanged("SelectedRegionId");
            }
        }

        public List<Store> Stores
        {
            get { return _stores; }
            set
            {
                _stores = value;
                OnPropertyChanged("Stores");
            }
        }

        public List<RegionHierarchy> NationalHierarchy
        {
            get { return _nationalHierarchy; }
            set
            {
                _nationalHierarchy = value;
                OnPropertyChanged("NationalHierarchy");
            }
        }

        public MainPageViewModel()
        {
            NationalHierarchy = SampleDataGenerator.GenerateDataWithHierarchy();
            Stores = SampleDataGenerator.Stores;
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
