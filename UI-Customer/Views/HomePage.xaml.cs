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
using UI_Customer.ViewModels;

namespace UI_Customer.Views
{
    /// <summary>
    /// Interaction logic for HomePage1.xaml
    /// </summary>
    public partial class HomePage1 : UserControl
    {
        HomePageViewModel _viewModel = new HomePageViewModel();
        public HomePage1()
        {
            //InitializeComponent();
            this.DataContext = _viewModel;
        }
    }
}
