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
    /// Interaction logic for MoveAndCancelReservation.xaml
    /// </summary>
    public partial class AccommodationReservationMoveAndCancel : Window
    {
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public AccommodationReservationController ReservationController { get; set; }

        public ObservableCollection<ReservationMoveRequest> Requests { get; set; }  
        public ReservationMoveRequest SelectedRequest { get; set; }
        public ReservationMoveRequestController RequestController { get; set; }
        public AccommodationReservationMoveAndCancel(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            ReservationController = AccommodationReservationController.GetInstance();
            RequestController = ReservationMoveRequestController.GetInstance();
            Reservations = new ObservableCollection<AccommodationReservation>(ReservationController.GetUpcomingReservations(user.Id));
            Requests = new ObservableCollection<ReservationMoveRequest>(RequestController.GetAllMoveRequestsForUser(user.Id));
        }

        private void BtnMove_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedReservation != null) 
            {
                AccommodationMoveReservationForm accommodationMoveReservationForm = new AccommodationMoveReservationForm(SelectedReservation);
                accommodationMoveReservationForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("None of the reservations were selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                string messageRes = "Are you sure you want to cancel this reservation?";
                string messageCaptionRes = "Cancelling reservation";
                MessageBoxResult result = MessageBox.Show(messageRes, messageCaptionRes, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                   if(ReservationController.CanReservationBeCancelled(SelectedReservation))
                   {
                        MessageBox.Show("Reservation successfullu cancelled!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ReservationController.CancelReservation(SelectedReservation);
                        this.Close();
                   }
                   else
                   {
                        MessageBox.Show("It is too late to cancel!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                   }
                    
                }    
            }
            else
            {
                MessageBox.Show("None of the reservations were selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
