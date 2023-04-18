using SIMS_Project.Controller;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIMS_Project.View
{
    /// <summary>
    /// Interaction logic for AccommodationOwnerRate.xaml
    /// </summary>
    public partial class AccommodationOwnerRateWindow : Window
    {
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public AccommodationReservationController _controller { get; set; }
        public User SignedInUser { get; set; }
        public AccommodationOwnerRateWindow(User signedInUser)
        {
            InitializeComponent();
            this.DataContext = this;
            _controller = AccommodationReservationController.GetInstance();
            SignedInUser = signedInUser;                                                                                            
            Reservations = new ObservableCollection<AccommodationReservation>(_controller.GetRateableReservationsForUser(signedInUser.Id));
        }

        private void BtnRateForm_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedReservation!=null)
            {
                AccommodationOwnerRateForm accommodationOwnerRateForm = new AccommodationOwnerRateForm(SelectedReservation, SignedInUser);
                accommodationOwnerRateForm.ShowDialog();
            }

            else
            {
                MessageBox.Show("None of the reservations were selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
    }
}
