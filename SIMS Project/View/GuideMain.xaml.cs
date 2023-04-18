using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for GuideMain.xaml
    /// </summary>


    public partial class GuideMain : Window,INotifyPropertyChanged
    {
        
        private int _count = 1;
        private bool _ended = false; 
        public KeyPoint SelectedKeyPoint { get; set; }


        public TourReservationController _tourReservationController { get; set; }

        private readonly TourStartDateTimeController _tourStartDateTimeController;
        private readonly KeyPointController _keyPointController; 
        private readonly TourController _tourController;
        

        public TourStartDateTime SelectedDate { get; set; } 

        public List<TourStartDateTime> _tourStartDateTimes { get; set; } 

        public List<User> SignedGuests { get; set; }
        
      
        public event PropertyChangedEventHandler? PropertyChanged;
        public GuideMain()
        {

            InitializeComponent();
            DataContext = this;

            SignedGuests = new List<User>();
            _tourReservationController = TourReservationController.GetInstance();
            _tourController = TourController.GetInstance();
            _keyPointController = KeyPointController.GetInstance();
            _tourStartDateTimeController = TourStartDateTimeController.GetInstance(); 

            _tourStartDateTimes = _tourStartDateTimeController.GetAll(); 
            
            CheckForStartedTours();    
            
           
        }
        private void CheckForStartedTours()
        {

            foreach (TourStartDateTime t in _tourStartDateTimes)
            {
                if (t.Started) 
                {
                    SelectedDate = t;

                    LoadKeyPointsIntoListBox();
                    LoadGuests(); 


                    BtnStartTour.IsEnabled = false;
                    BtnCheckKeyPoint.Visibility = Visibility.Visible;
                    BtnMarkGuests.Visibility = Visibility.Visible;

                    if (_count == LstKeyPoints.Items.Count)
                    {
                        BtnEndTour.Visibility = Visibility.Visible;
                        BtnCheckKeyPoint.Visibility = Visibility.Hidden;
                    }


                    break; 
                }
            }

        }
        private void LoadKeyPointsIntoListBox() 
        {
            int k = 0;
         
            foreach (KeyPoint keyPnt in SelectedDate.Tour.KeyPoints)
            {

                    LstKeyPoints.Items.Add(keyPnt);

                    if (keyPnt.Checked && k != 0)
                        ++_count;
                    ++k;

             }

        }
        private void LoadGuests() 
        {
            foreach (TourReservation tourReservation in _tourReservationController.GetAll()) 
            {
                if (tourReservation.TourStartDateTimeId == SelectedDate.Id && tourReservation.KeyPointMarked == -1) {
                    SignedGuests.Add(tourReservation.Guest2);
                }
            }
        }
        
        private void BtnCreateTour_Click(object sender, RoutedEventArgs e)
        {
            TourCreationForm tourCreationForm = new TourCreationForm();
            tourCreationForm.Owner = this;
            tourCreationForm.ShowDialog();
        }
        private void BtnStartTour_Click(object sender, RoutedEventArgs e) 
        {
            StartTourForm startTourForm = new StartTourForm(this);
            startTourForm.Owner = this;
            startTourForm.ShowDialog(); 
        }
       

        private void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDate != null && !_ended)
            {

                BtnCheckKeyPoint.Visibility = Visibility.Hidden;
                BtnEndTour.Visibility = Visibility.Visible;
                foreach (KeyPoint keypoint in LstKeyPoints.Items) {
                    keypoint.Checked = true; 
                    ++_count; 
                }
                --_count;
                _ended = true; 
                MessageBox.Show("Tour finished earlier", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);

            }else if (_ended)
            {
                MessageBox.Show("Already ended", "Error", MessageBoxButton.OK, MessageBoxImage.Error); 

            }
            else if(SelectedDate == null)
            {

                MessageBox.Show("No tour to terminate", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void BtnCheckKeyPoint_Click(object sender, RoutedEventArgs e) 
        {

            if (LstKeyPoints.SelectedIndex != _count)
            {
                MessageBox.Show("Must check keypoints in order", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SelectedKeyPoint = (KeyPoint)LstKeyPoints.SelectedItem;
                SelectedKeyPoint.Checked = true;
                MessageBox.Show("Keypoint checked", "Keypoint checking", MessageBoxButton.OK, MessageBoxImage.Information);

                ++_count; 
                if(_count == LstKeyPoints.Items.Count)
                {
                    BtnCheckKeyPoint.Visibility = Visibility.Hidden;
                    BtnEndTour.Visibility = Visibility.Visible;
                    _ended = true; 
                }
            }
        
        }
        private void BtnEndTour_Click(object sender, RoutedEventArgs e)
        {
            SelectedDate.Finished = true;
            ResetWindowToDefault(); 
            MessageBox.Show("Tour ended", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            _ended = false; 
        }

        private void BtnDenounceTour_Click(object sender, RoutedEventArgs e) { 

            CancelTourForm cancelTourForm = new CancelTourForm();
            cancelTourForm.Owner = this; 
            cancelTourForm.ShowDialog();
        
        }
        private void BtnMarkGuests_Click(object sender, RoutedEventArgs e) 
        {
            if (LstKeyPoints.SelectedIndex == _count-1)
            {
                SelectedKeyPoint = (KeyPoint)LstKeyPoints.SelectedItem;
                MarkGuests markGuestsForm = new MarkGuests(this);
                markGuestsForm.Owner = this;
                markGuestsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Must mark guests on active keypoint", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnStatistics_Click(object sende, RoutedEventArgs e) 
        {
            TourStatisticsForm tourStatisticsForm = new TourStatisticsForm();
            tourStatisticsForm.Owner = this; 
            tourStatisticsForm.ShowDialog();
        
        }
        private void ResetWindowToDefault() {
            SelectedDate.Started = false;
            SelectedDate = null;

            ResetKeyPoints();
            ResetReservations(); 

            BtnStartTour.IsEnabled = true;
            BtnEndTour.Visibility = Visibility.Hidden;
            BtnMarkGuests.Visibility = Visibility.Hidden;

            SignedGuests.Clear();
            LstKeyPoints.Items.Clear();

            _count = 1;

        }
        
        private void ResetKeyPoints() 
        {
            foreach (KeyPoint keypoint in LstKeyPoints.Items)
            {
                keypoint.Checked = false;
            }
        }
        private void ResetReservations() 
        {
            foreach (TourReservation reservation in _tourReservationController.GetAll()) 
            {
                if (reservation.TourStartDateTimeId == SelectedDate.Id)
                    reservation.KeyPointMarked = -1; 
            }
        }
      
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _keyPointController.Save();
            _tourStartDateTimeController.Save();  
            _tourReservationController.Save();  
           
        }

     


    }
}

