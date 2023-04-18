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
    /// Interaction logic for StartTourForm.xaml
    /// </summary>
    public partial class StartTourForm : Window
    {
        private readonly TourController _tourController;
        private readonly KeyPointController _keyPointController; 
        private readonly TourStartDateTimeController _tourStartDateTimeController;
        private readonly TourReservationController _tourReservationController; 

        private List<Tour> _tours;
        private GuideMain _owner; 
        public StartTourForm(GuideMain owner)
        {  
            InitializeComponent();
            DataContext = this;
            _owner = owner;

            _tourController = TourController.GetInstance();
            _keyPointController = KeyPointController.GetInstance();
            _tourStartDateTimeController = TourStartDateTimeController.GetInstance();
            _tourReservationController = TourReservationController.GetInstance(); 

          
            LoadIntoListBox(); 
            
        }

        private void LoadIntoListBox() {
            
             foreach (TourStartDateTime startDateTime in _tourStartDateTimeController.GetAll()) 
             {
                    if (startDateTime.StartDateTime.Date == DateTime.Now.Date && !startDateTime.TourDeleted) 
                    {
                        LstTours.Items.Add(startDateTime);
                    }
             }
            
        }
        private void BtnStart_Click(object sender, RoutedEventArgs e) {

            if (LstTours.SelectedIndex == -1) {
                MessageBox.Show("Must select one tour", "Tour starting error", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
            else
            {
                _owner.SelectedDate =(TourStartDateTime)LstTours.SelectedItem;
                _owner.SelectedDate.Started = true;

                LoadKeyPointsIntoListBox();
                LoadGuests(); 

                _owner.BtnStartTour.IsEnabled = false;
                _owner.BtnCheckKeyPoint.Visibility = Visibility.Visible;
                _owner.BtnMarkGuests.Visibility = Visibility.Visible;
 
               
                this.Close(); 
            }
        }
        private void LoadGuests() {
            foreach (TourReservation reservation in _tourReservationController.GetAll())
            {
                if (reservation.TourStartDateTimeId == _owner.SelectedDate.Id) 
                {
                    _owner.SignedGuests.Add(reservation.Guest2);
                }

            }
            
        }
        private void LoadKeyPointsIntoListBox() {
            foreach (KeyPoint keypoint in _owner.SelectedDate.Tour.KeyPoints)
            {
                _owner.LstKeyPoints.Items.Add(keypoint);

            }
            KeyPoint firstKeypoint = (KeyPoint)_owner.LstKeyPoints.Items[0];
            firstKeypoint.Checked = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            this.Close(); 
        }

    }
}
