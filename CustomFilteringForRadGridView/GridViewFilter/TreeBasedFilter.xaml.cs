using System.Windows;
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
            if(DataContext == null)
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

        private void RadTreeView_Checked(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            // This event will be called when a RadTreeViewItem is checked
            // So this should invoke the filtering logic

            var item = e.Source as RadTreeViewItem;
            var vm = this.DataContext as TreeBasedFilterViewModel;

            if (item.Item is RegionHierarchy)
            {
                var region = (item.Item as RegionHierarchy).CurrentRegion;
                

                //vm.Prepare(region);
                vm.AddRegionDistinctValue(region.RegionId);
            }
            else if (item.Item is DivisionHierarchy)
            {
                var division = (item.Item as DivisionHierarchy).CurrentDivision;

                vm.AddDivisionDistinctValue(division.DivisionId);
            }
        }

        private void RadTreeView_Unchecked(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            // To Disable a filter
            var item = e.Source as RadTreeViewItem;
            var vm = this.DataContext as TreeBasedFilterViewModel;

            if (item.Item is RegionHierarchy)
            {
                var region = (item.Item as RegionHierarchy).CurrentRegion;

                //vm.Prepare(null);

                vm.RemoveRegionDistinctValue(region.RegionId);
            }
            else if (item.Item is DivisionHierarchy)
            {
                var division = (item.Item as DivisionHierarchy).CurrentDivision;

                vm.RemoveDivisionDistinctValue(division.DivisionId);
            }
        }
    }
}
