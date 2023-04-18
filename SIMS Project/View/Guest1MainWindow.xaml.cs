using SIMS_Project.Controller;
using SIMS_Project.Model;
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

namespace SIMS_Project.View
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainWindow : Window
    {
        public ReservationMoveRequestController _reservationMoveRequestController { get; set; }
        public User SignedInGuest { get; set; }
        public Guest1MainWindow(User signedInGuest)
        {
            InitializeComponent();
            this.DataContext = DataContext;
            SignedInGuest = signedInGuest;
            _reservationMoveRequestController = ReservationMoveRequestController.GetInstance();
        }

        private void BtnAccommodationView_Click(object sender, RoutedEventArgs e)
        {
            AccommodationView accommodationView = new AccommodationView(SignedInGuest);
            accommodationView.ShowDialog();
        }

        private void BtnRate_Click(object sender, RoutedEventArgs e)
        {
            AccommodationOwnerRateWindow accommodationOwnerRate = new AccommodationOwnerRateWindow(SignedInGuest);
            accommodationOwnerRate.ShowDialog();
        }

        private void BtnMoveAndCancel_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationMoveAndCancel accommodationReservationMoveAndCancel = new AccommodationReservationMoveAndCancel(SignedInGuest);
            accommodationReservationMoveAndCancel.ShowDialog();
        }

        private void BtnNotification_Click(object sender, RoutedEventArgs e)
        {
            MoveRequestsNotifications notifications = new MoveRequestsNotifications(SignedInGuest);
            notifications.ShowDialog();
            _reservationMoveRequestController.MakeAllNotChanged(SignedInGuest.Id);
        }
    }
}
