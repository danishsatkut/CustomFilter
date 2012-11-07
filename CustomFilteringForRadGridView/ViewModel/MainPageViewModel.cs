using System.Collections.Generic;
using HierarchyModel;
using HierarchyModel.Hierarchy;
using HierarchyModel.Model;
using Telerik.Windows.Controls;

namespace CustomFilteringForRadGridView.ViewModel
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
    }
}
