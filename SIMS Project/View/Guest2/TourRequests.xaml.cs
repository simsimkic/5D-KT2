using SIMS_Project.Model;
using SIMS_Project.ViewModel.Guest2;
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

namespace SIMS_Project.View.Guest2
{
    /// <summary>
    /// Interaction logic for TourRequests.xaml
    /// </summary>
    public partial class TourRequests : Window
    {
        public TourRequests(User signedInGuest2)
        {
            InitializeComponent();

            DataContext = new TourRequestsViewModel(signedInGuest2);
        }
    }
}
