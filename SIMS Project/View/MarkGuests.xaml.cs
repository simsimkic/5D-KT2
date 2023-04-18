using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Model.DTO.Notifications;
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
    /// Interaction logic for MarkGuests.xaml
    /// </summary>
    public partial class MarkGuests : Window
    {
        private GuideMain _owner; 

        private readonly GuestCheckNotificationController _guestCheckNotificationController;
        private GuestCheckNotification _guestCheckNotification;
        private TourReservationController _tourReservationController; 
        public MarkGuests(GuideMain owner)
        {

            InitializeComponent();
            _owner = owner;
            _guestCheckNotificationController = GuestCheckNotificationController.GetInstance();
            _tourReservationController = TourReservationController.GetInstance();

            LoadGuestsIntoListBox();
          
        }
        
        private void BtnConfirm_Click(object sender, RoutedEventArgs e) 
        {
            if (LstSignedGuests.SelectedIndex != -1) {

                User selectedUser = (User)LstSignedGuests.SelectedItem;

                SendNotificationToGuest(selectedUser);
                
                TourReservation reservation = _tourReservationController.GetAll().Find(t => t.TourStartDateTimeId == _owner.SelectedDate.Id &&
                    t.Guest2Id == selectedUser.Id 
                );
                reservation.KeyPointMarked = _owner.SelectedKeyPoint.Id; 
                _owner.SignedGuests.Remove(selectedUser);
               
                MessageBox.Show("Guest marked to keypoint","Guest mark",MessageBoxButton.OK,MessageBoxImage.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("Must select guest", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
        private void LoadGuestsIntoListBox() {

            foreach (User user in _owner.SignedGuests) 
            {
                LstSignedGuests.Items.Add(user); 
            }
        }
        private void SendNotificationToGuest(User selectedUser) {

            _guestCheckNotification = new GuestCheckNotification();
            _guestCheckNotification.GuestId = selectedUser.Id;
            _guestCheckNotification.Body = _owner.SelectedKeyPoint.ToString();
            _guestCheckNotificationController.Add(_guestCheckNotification);

        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
