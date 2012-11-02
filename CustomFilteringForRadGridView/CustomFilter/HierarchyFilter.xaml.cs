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
	            var viewmodel = new HierarchyFilterViewModel(column, column.DataControl.FilterDescriptors);
                // Let the ViewModel decide about the IsActive property value
	            SetBinding(IsActiveProperty, new Binding {Source = viewmodel, Mode = BindingMode.OneWay});

	            DataContext = viewmodel;
	        }
            else
	        {
	            ((HierarchyFilterViewModel)DataContext).Prepare();
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
}