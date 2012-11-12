using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using HierarchyModel.Hierarchy;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace CustomFilteringForRadGridView.GridViewFilter
{
    public partial class TreeBasedFilter : UserControl, IFilteringControl
    {
        public TreeBasedFilter()
        {
            InitializeComponent();
            DataContext = null;
        }

        #region Implementation of IFilteringControl

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(TreeBasedFilter), new PropertyMetadata(false));

        public void Prepare(GridViewColumn column)
        {
            if (DataContext == null)
            {
                // Create the view model the very first time prepare is called.
                var viewmodel = new TreeBasedFilterViewModel(column.DataControl.FilterDescriptors);
                // Let the ViewModel decide about the IsActive property value
                SetBinding(IsActiveProperty, new Binding("IsActive")
                                                 {
                                                     Source = viewmodel,
                                                     Mode = BindingMode.OneWay
                                                 });

                DataContext = viewmodel;
            }
            else
            {
                //((TreeBasedFilterViewModel)DataContext).Prepare(regionId:RegionId, divisionId:DivisionId);
            }
        }

        /// <summary>
        /// IsActive CLR property for the IsActiveProperty
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        #endregion

        // TODO: Delegate the task of calling the method to the ViewModel
        // TODO: eg. AddDistinctValue(item.Item as object)
        // TODO: Asbtract hierarchies using IHierarchyLevel which contains current and collection properties
        private void RadTreeView_Checked(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            // This event will be called when a RadTreeViewItem is checked
            // So this should invoke the filtering logic

            var item = e.Source as RadTreeViewItem;
            var vm = this.DataContext as TreeBasedFilterViewModel;

            if (item.Item is RegionHierarchy)
            {
                var region = (item.Item as RegionHierarchy).CurrentModel;

                // The region is to be removed if its state is indeterminate
                // This is required because filtering between two different 
                // hierarchy levels is done using OR operator.
                // Also using the AND operator has the issue of invalid filtering
                // eg. If Region 1, 2, 3, 4 are checked for filtering, the filtering will
                // happen at region level properly. But when Region 2 is expanded Division 3, 4
                // of Region 2 will be applied as well. So the expression will become:
                // (Region 1 || Region 2|| Region 3 || Region 4) && (Division 3 || Division 4)
                // Thus stores available only in Division 3 and Division 4 will be visible.
                if (item.CheckState == ToggleState.Indeterminate)
                {
                    vm.RemoveRegionDistinctValue(region.Id);
                }
                else
                {
                    // Only add the region filter, if it is fully selected
                    vm.AddRegionDistinctValue(region.Id);
                }
            }
            else if (item.Item is DivisionHierarchy)
            {
                var division = (item.Item as DivisionHierarchy).CurrentModel;

                if (item.CheckState == ToggleState.Indeterminate)
                    vm.RemoveDivisionDistinctValue(division.Id);
                else
                    vm.AddDivisionDistinctValue(division.Id);
            }
        }

        private void RadTreeView_Unchecked(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            // To Disable a filter
            var item = e.Source as RadTreeViewItem;
            var vm = this.DataContext as TreeBasedFilterViewModel;

            if (item.Item is RegionHierarchy)
            {
                var region = (item.Item as RegionHierarchy).CurrentModel;

                //vm.Prepare(null);

                vm.RemoveRegionDistinctValue(region.Id);
            }
            else if (item.Item is DivisionHierarchy)
            {
                var division = (item.Item as DivisionHierarchy).CurrentModel;

                vm.RemoveDivisionDistinctValue(division.Id);
            }
        }

        private void _hierarchyTree_ItemPrepared(object sender, RadTreeViewItemPreparedEventArgs e)
        {
            var item = e.PreparedItem;

            if (item.Item is NationalHierarchy)
            {
                item.CheckState = ToggleState.On;
                item.IsExpanded = true;
            }
        }
    }
}
