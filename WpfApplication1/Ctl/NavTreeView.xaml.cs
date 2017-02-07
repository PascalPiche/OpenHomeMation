using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1.MV;

namespace WpfApplication1.Ctl
{
    /// <summary>
    /// Interaction logic for NavTreeView.xaml
    /// </summary>
    public partial class NavTreeView : UserControl
    {
        public NavTreeView()
        {
            InitializeComponent();
            this.DataContextChanged += NavTreeView_DataContextChanged;
        }

        private void NavTreeView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Set Initial selection
            homeTreeViewItem.IsSelected = true;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MainWindowVM dataContext = this.GetValue(DataContextProperty) as MainWindowVM;
            //TreeViewItem newItem = e.NewValue as TreeViewItem;

            if (dataContext != null)
            {
                dataContext.SelectedNode = e.NewValue;
            }
        }
    }
}
