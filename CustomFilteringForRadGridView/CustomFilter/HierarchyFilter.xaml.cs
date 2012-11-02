using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CustomFilteringForRadGridView.CustomFilter;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace CustomFilteringForRadGridView.CustomFilter
{
	public partial class HierarchyFilter : UserControl, IFilteringControl
	{
	    public static readonly DependencyProperty IsActiveProperty =
	        DependencyProperty.Register("IsActive", typeof (bool), typeof (HierarchyFilter), new PropertyMetadata(false));

        #region Custom Control Properties

	    public static readonly DependencyProperty DivisionIdProperty =
	        DependencyProperty.Register("DivisionId", typeof (int), typeof (HierarchyFilter), new PropertyMetadata(default(int)));

	    public int DivisionId
	    {
	        get { return (int) GetValue(DivisionIdProperty); }
	        set { SetValue(DivisionIdProperty, value); }
	    }

	    public static readonly DependencyProperty RegionIdProperty =
	        DependencyProperty.Register("RegionId", typeof (int), typeof (HierarchyFilter), new PropertyMetadata(default(int)));

	    public int RegionId
	    {
	        get { return (int) GetValue(RegionIdProperty); }
	        set
	        {
	            SetValue(RegionIdProperty, value);
                //OnRegionIdChanged(value);
	        }
	    }

        #endregion

        private void OnRegionIdChanged(int value)
        {
            if (!(DataContext is HierarchyFilterViewModel)) return;

            var viewmodel = DataContext as HierarchyFilterViewModel;
            viewmodel.Prepare(value, DivisionId);
        }   

        public HierarchyFilter()
		{
			// Required to initialize variables
			InitializeComponent();
		    DataContext = null;
		}

	    #region Implementation of IFilteringControl

	    public void Prepare(GridViewColumn column)
	    {
	        if(DataContext == null)
	        {
                // Create the view model the very first time prepare is called.
	            var viewmodel = new HierarchyFilterViewModel(column, column.DataControl.FilterDescriptors, DivisionId, RegionId);
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
	            ((HierarchyFilterViewModel)DataContext).Prepare(regionId:RegionId, divisionId:DivisionId);
	        }
	    }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

	    #endregion

        private void OnClear(object sender, RoutedEventArgs e)
        {
            var viewmodel = DataContext as HierarchyFilterViewModel;
            if (viewmodel != null)
            {
                viewmodel.Clear();
            }

        }
	}

    public class RegionIdEventArgs : EventArgs
    {
        public RegionIdEventArgs(int regionId)
        {
            RegionId = regionId;
        }

        public int RegionId { get; set; }
    }
}