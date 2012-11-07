using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace CustomFilteringForRadGridView.GridViewFilter
{
    /// <summary>
    /// This class represents the Filter of Distinct Values for each Hierarchy.
    /// </summary>
    public class DistinctHierarchyValuesFilterDescriptor : DescriptorBase, IDistinctValuesFilterDescriptor
    {
        /// <summary>
        /// Represents the current hierarchy column name on which this filter is to applied.
        /// </summary>
        private readonly string _columnName;

        /// <summary>
        /// Represents the composite filter which will contain the individual descriptor 
        /// for each hierarchy instance.
        /// </summary>
        private readonly CompositeFilterDescriptor _compositeFilter;

        public DistinctHierarchyValuesFilterDescriptor(string columnName)
        {
            _columnName = columnName;
            _compositeFilter = new CompositeFilterDescriptor { LogicalOperator = FilterCompositionLogicalOperator.Or };
            _compositeFilter.PropertyChanged += (sender, args) => OnPropertyChanged(string.Empty);
        }

        public DistinctHierarchyValuesFilterDescriptor(GridViewColumn column)
            : this(column.FilterMemberPath)
        {
        }

        #region Implementation of IDistinctValuesFilterDescriptor

        /// <summary>
        /// Finds the current descriptor in the filterDescriptors.
        /// If the descriptor is found, it is returned else null is returned.
        /// </summary>
        /// <param name="distinctValue">Value of the filter to be searched</param>
        /// <returns>Filter if found, else null</returns>
        public OperatorValueFilterDescriptorBase TryFindDescriptor(object distinctValue)
        {
            // Get the current filterDescriptors
            var filterDescriptors = FilterDescriptors;

            // Checks whether the FilterDescriptor.Value is equal to distinctValue
            Func<OperatorValueFilterDescriptorBase, bool> predicate = fd => object.Equals(fd.Value, distinctValue);

            // Find the current distinctValue in filterDescriptors using predicate
            return filterDescriptors.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Adds the current value in the filter collection
        /// </summary>
        /// <param name="distinctValue">Value to be added</param>
        public void AddDistinctValue(object distinctValue)
        {
            if (TryFindDescriptor(distinctValue) == null)
            {
                // If no value already present in the collection, create a new FilterDescriptor
                var filterDescriptor = CreateSimpleFilterDescriptor(_columnName, distinctValue);
                _compositeFilter.FilterDescriptors.Add(filterDescriptor);
            }
        }

        /// <summary>
        /// Removes the current value from the filter collection
        /// </summary>
        /// <param name="distinctValue">Value to be removed</param>
        /// <returns>Boolean result of the operation</returns>
        public bool RemoveDistinctValue(object distinctValue)
        {
            var filterDescriptor = TryFindDescriptor(distinctValue);
            return ((filterDescriptor != null) && _compositeFilter.FilterDescriptors.Remove(filterDescriptor));
        }

        /// <summary>
        /// Clear the current distinct hierarchy filter
        /// </summary>
        public void Clear()
        {
            _compositeFilter.FilterDescriptors.Clear();
        }

        /// <summary>
        /// Returns all the distinct values from the filter
        /// </summary>
        public IEnumerable<object> DistinctValues
        {
            get
            {
                return (from fd in FilterDescriptors
                        select fd.Value);
            }
        }

        /// <summary>
        /// Returns the filter descriptors from the <see cref="_compositeFilter"/>'s FilterDescriptors collection.
        /// </summary>
        public IEnumerable<OperatorValueFilterDescriptorBase> FilterDescriptors
        {
            get
            {
                return _compositeFilter.FilterDescriptors.Cast<OperatorValueFilterDescriptorBase>();
            }
        }

        /// <summary>
        /// Returns whether the filter is active
        /// </summary>
        public bool IsActive
        {
            get { return FilterDescriptors.Any(fd => fd.IsActive); }
        }

        #endregion

        #region Implementation of IFilterDescriptor

        /// <summary>
        /// Creates filter expression by calling the CreateFilterExpression of the containing composite filter.
        /// </summary>
        /// <param name="instance">The expression for the filter</param>
        /// <returns>Created expression</returns>
        Expression IFilterDescriptor.CreateFilterExpression(Expression instance)
        {
            return _compositeFilter.CreateFilterExpression(instance);
        }

        #endregion

        /// <summary>
        /// Creates a new Filter Descriptor instance
        /// </summary>
        /// <param name="columnName"> </param>
        /// <param name="value">Value for the filter</param>
        /// <returns>Filter Descriptor instance for the specified column</returns>
        private static OperatorValueFilterDescriptorBase CreateSimpleFilterDescriptor(string columnName, object value)
        {
            return new FilterDescriptor(columnName, FilterOperator.IsEqualTo, value);
        }

    }
}
