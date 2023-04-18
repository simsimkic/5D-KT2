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
    /// Interaction logic for AccommodationDetails.xaml
    /// </summary>
    /// 

    public enum TabItem { DETAILS, RESERVATIONS, REVIEWS, RENOVATIONS }

    public partial class AccommodationDetails : UserControl
    {
        public AccommodationDetails()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null) 
            {
                AccommodationDetailsViewModel vm = DataContext as AccommodationDetailsViewModel;
                switch (DetailsTabControl.SelectedIndex)
                {
                    case (int)TabItem.DETAILS:
                        break;
                    case (int)TabItem.RESERVATIONS:
                        vm.NavigateAccommodationReservationsCommand.Execute(null);
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
