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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_Project.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationReservations.xaml
    /// </summary>
    public partial class AccommodationReservations : UserControl
    {
        public AccommodationReservations()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
            {
                AccommodationReservationsViewModel vm = DataContext as AccommodationReservationsViewModel;
                switch (DetailsTabControl.SelectedIndex)
                {
                    case (int)TabItem.DETAILS:
                        vm.NavigateAccommodationDetailsCommand.Execute(null);
                        break;
                    case (int)TabItem.RESERVATIONS:
                        break;
                    case (int)TabItem.REVIEWS:
                        vm.NavigateAccommodationReviewsCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
