using SIMS_Project.Model;
using SIMS_Project.Stores;
using SIMS_Project.Stores.Owner;
using SIMS_Project.ViewModel.Owner;
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
using System.Windows.Shapes;

namespace SIMS_Project.View.Owner
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(UserStore userStore)
        {
            InitializeComponent();
            NavigationStore navigationStore = new NavigationStore();
            ModalNavigationStore modalNavigationStore = new ModalNavigationStore();
            DataContext = new MainViewModel(navigationStore, new NavigationBarViewModel(navigationStore, userStore, modalNavigationStore), userStore, modalNavigationStore);
        }
    }
}
