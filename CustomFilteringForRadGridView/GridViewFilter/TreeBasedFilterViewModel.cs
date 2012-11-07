using System;
using System.Collections.Generic;
using System.Linq;
using CustomFilteringForRadGridView.ViewModel;
using HierarchyModel;
using HierarchyModel.Hierarchy;
using HierarchyModel.Model;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace CustomFilteringForRadGridView.GridViewFilter
{
    // TODO: Remove the clear filter functionality and implement NationalHierarchy

    public class TreeBasedFilterViewModel : ViewModelBase
    {
        /// <summary>
        /// Actual collection of filter that will contain all the hierarchy filters.
        /// This filter is added to the FilterDescriptorCollection
        /// </summary>
        private CompositeFilterDescriptor _filters;
        private FilterDescriptorCollection _targetFilters;

        private DistinctHierarchyValuesFilterDescriptor _regionHierarchyFilter;
        private DistinctHierarchyValuesFilterDescriptor _divisionHierarchyFilter;

        private List<RegionHierarchy> _sampleData;
        public List<RegionHierarchy> SampleData
        {
            get { return _sampleData; }
            set
            {
                _sampleData = value;
                OnPropertyChanged("SampleData");
            }
        }

        public TreeBasedFilterViewModel(FilterDescriptorCollection targetFilters)
        {
            SampleData = SampleDataGenerator.GenerateDataWithHierarchy();

            _targetFilters = targetFilters;

            CreateFilterDescriptor();

            _filters.FilterDescriptors.CollectionChanged += FilterDescriptors_CollectionChanged;

        }

        void FilterDescriptors_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_filters.FilterDescriptors.Count > 0)
            {
                if (! _targetFilters.Contains(_filters))
                {
                    _targetFilters.Add(_filters);
                }
            }
            else
            {
                _targetFilters.Remove(_filters);
            }

            OnPropertyChanged("IsActive");
            ClearFilterCommand.OnCanExecuteChanged();
        }

        // This method initially creates all the hierarchy filters required
        private void CreateFilterDescriptor()
        {
            _regionHierarchyFilter = new DistinctHierarchyValuesFilterDescriptor("RegionId");
            _divisionHierarchyFilter = new DistinctHierarchyValuesFilterDescriptor("DivisionId");

            _filters = new CompositeFilterDescriptor { LogicalOperator = FilterCompositionLogicalOperator.And };
        }

        public void Prepare(Region region)
        {
            // No code for now.
        }

        /// <summary>
        /// Returns whether the current filter is active
        /// </summary>
        public bool IsActive
        {
            get { return _targetFilters.Contains(_filters); }
        }

        /// <summary>
        /// Clears all the filter
        /// </summary>
        public void Clear()
        {
            _targetFilters.Remove(_filters);
        }

        private ViewModelCommand _clearFilterCommand = null;
        public ViewModelCommand ClearFilterCommand
        {
            get
            {
                return _clearFilterCommand ?? (_clearFilterCommand = new ViewModelCommand(
                                                                         filter => Clear(),
                                                                         filter => IsActive
                                                                         ));
            }
        }

        // This basically maps to the Value properties in the sample code
        public void AddRegionDistinctValue(int regionId)
        {
            _regionHierarchyFilter.AddDistinctValue(regionId);
            if (! _filters.FilterDescriptors.Contains(_regionHierarchyFilter))
            {
                _filters.FilterDescriptors.Add(_regionHierarchyFilter);
            }
        }

        public void RemoveRegionDistinctValue(int regionId)
        {
            _regionHierarchyFilter.RemoveDistinctValue(regionId);
            if(_regionHierarchyFilter.FilterDescriptors.Count() == 0)
            {
                _filters.FilterDescriptors.Remove(_regionHierarchyFilter);
            }
        }

        public void AddDivisionDistinctValue(int divisionId)
        {
            _divisionHierarchyFilter.AddDistinctValue(divisionId);
            if (! _filters.FilterDescriptors.Contains(_divisionHierarchyFilter))
            {
                _filters.FilterDescriptors.Add(_divisionHierarchyFilter);
            }
        }

        public void RemoveDivisionDistinctValue(int divisionId)
        {
            _divisionHierarchyFilter.RemoveDistinctValue(divisionId);
            if (_divisionHierarchyFilter.FilterDescriptors.Count() == 0)
            {
                _filters.FilterDescriptors.Remove(_divisionHierarchyFilter);
            }
        }
    }
}
