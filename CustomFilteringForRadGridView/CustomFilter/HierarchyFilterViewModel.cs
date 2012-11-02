using System;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace CustomFilteringForRadGridView.CustomFilter
{
    public class HierarchyFilterViewModel : INotifyPropertyChanged
    {
        private readonly GridViewBoundColumnBase _fieldDescriptor;
        private readonly string _dataMemberName;
        private readonly FilterDescriptorCollection _targetFilters;

        private FilterDescriptor _regionFilter;
        private FilterDescriptor _divisionFilter;

        /// <summary>
        /// RegionFilter textbox value which is binded on the UI
        /// </summary>
        public object RegionFilterValue
        {
            get { return _regionFilter.Value; }
            set
            {
                // If the value is same as the current value, return
                if (_regionFilter.Value == value) return;

                if(value != null)
                {
                    _regionFilter.Value = value;
                    // Only add the filter if target filter does not already contain the filter
                    if(!_targetFilters.Contains(_regionFilter))
                    {
                        _targetFilters.Add(_regionFilter);
                    }
                }
                else
                {
                    // Unsets the value. This is important, otherwise clear operation maybe inconsistent
                    _regionFilter.Value = OperatorValueFilterDescriptorBase.UnsetValue;
                    // Remove this filter from FilterDescriptorCollection
                    _targetFilters.Remove(_regionFilter);
                }
                OnPropertyChanged("RegionFilterValue");
            }
        }

        public object DivisionFilterValue
        {
            get { return _divisionFilter.Value; }
            set
            {
                // 1. If values are same, return
                if (_divisionFilter.Value == value) return;

                // 2. If values are different, the filter can be set or unset    
                if (value != null)
                {
                    // 2.a. If value is not null, set the value to the filter.Value
                    _divisionFilter.Value = value;

                    if (!_targetFilters.Contains(_divisionFilter))
                    {
                        // 2.a.i. If filterCollection does not already Contains filter, add the filter
                        _targetFilters.Add(_divisionFilter);
                    }
                }
                else
                {
                    // 2.b. If value is null, unset the filter.Value and remove the filter from filterCollection
                    _divisionFilter.Value = OperatorValueFilterDescriptorBase.UnsetValue;
                    _targetFilters.Remove(_divisionFilter);
                }
                OnPropertyChanged("DivisionFilterValue");
            }
        }

        /// <summary>
        /// Returns whether the current filter is active
        /// </summary>
        public bool IsActive
        {
            // The control is active if the FilterDescriptorCollection contains the regionFilter
            get { return _targetFilters.Contains(_regionFilter); }
        }

        /// <summary>
        /// Clears the filter
        /// </summary>
        public void Clear()
        {
            RegionFilterValue = null;
        }

        public HierarchyFilterViewModel(
            GridViewColumn fieldDescriptor,
            FilterDescriptorCollection targetFilters)
        {
            _fieldDescriptor = fieldDescriptor as GridViewBoundColumnBase;
            _targetFilters = targetFilters;
            _dataMemberName = fieldDescriptor.UniqueName;
            CreateFilterDescriptor();
        }

        private void CreateFilterDescriptor()
        {
            _regionFilter = new FilterDescriptor
                                {
                                    Member = "RegionId",
                                    MemberType = typeof(int),
                                    Operator = FilterOperator.IsEqualTo
                                };

            _divisionFilter = new FilterDescriptor
                                  {
                                      Member = "DivisionId",
                                      MemberType = typeof(int),
                                      Operator = FilterOperator.IsEqualTo
                                  };
        }

        public void Prepare()
        {
            // No code for now!
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
