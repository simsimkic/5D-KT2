using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Model.DTO.Notifications;
using SIMS_Project.View.Guest2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for Guest2Main.xaml
    /// </summary>
    public partial class Guest2Main : Window
    {
        public User SignedInGuest2 { get; set; }
        private GuestCheckNotificationController _guestCheckNotificationController;
        private List<GuestCheckNotification> _guestCheckNotifications;
        private bool _isSigningOut;


        public Guest2Main(User guest2)
        {
            InitializeComponent();
            
            SignedInGuest2 = guest2;

            _guestCheckNotificationController = GuestCheckNotificationController.GetInstance();
            _guestCheckNotifications = _guestCheckNotificationController.GetAll();
            _isSigningOut = false;

            foreach (GuestCheckNotification guestCheckNotification in _guestCheckNotifications)
            {
                if (SignedInGuest2.Id == guestCheckNotification.GuestId)
                {
                    MessageBox.Show("You are marked on keypoint " + guestCheckNotification.Body, "Keypoint mark", MessageBoxButton.OK, MessageBoxImage.Information);
                    _guestCheckNotificationController.Remove(guestCheckNotification);
                    break; 
                }
                
            }
        }

        private void BtnShowTours_Click(object sender, RoutedEventArgs e)
        {
            TourSearch tourSearch = new(SignedInGuest2)
            {
                Owner = this
            };

            tourSearch.ShowDialog();
        }

        private void BtnShowBookedTours_Click(object sender, RoutedEventArgs e)
        {
            /*BookedTours bookedTours = new BookedTours(SignedInGuest2);
            bookedTours.Owner = this;

            bookedTours.ShowDialog();*/
        }

        private void BtnShowTourRequests_Click(object sender, RoutedEventArgs e)
        {
            TourRequests tourRequests = new TourRequests(SignedInGuest2);
            tourRequests.Owner = this;

            tourRequests.ShowDialog();
        }

        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LoggedInUserId = -1;
            Properties.Settings.Default.Save();

            _isSigningOut = true;
            Close();
        }

        private void Guest2Main_Closing(object sender, CancelEventArgs e)
        {
            if (!_isSigningOut)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
