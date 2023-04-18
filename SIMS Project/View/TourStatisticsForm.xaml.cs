using SIMS_Project.Controller;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for TourStatisticsForm.xaml
    /// </summary>
    public partial class TourStatisticsForm : Window, INotifyPropertyChanged
    {
        private TourStartDateTimeController _tourStartDateTimeController;
        private TourReservationController _tourReservationController;
        private TourController _tourController;

        private List<Tour> _tours;
        private List<TourReservation> _reservations;
        private List<TourStartDateTime> _tourStartDateTimes;
        public Tour? mostVisitedTour { get; set; }
        public Tour selectedTour { get; set;  }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int? MaxSum { get; set; }

        public string BindingSum {
            get { return "(" + MaxSum.ToString() + ")"; }
            set {
                if (BindingSum != value)
                {
                    BindingSum = value;
                    OnPropertyChanged();

                }
            }
        }


        public TourStatisticsForm()
        {
            InitializeComponent();
            DataContext = this;

            mostVisitedTour = new Tour();
            _tourController = TourController.GetInstance();
            _tourReservationController = TourReservationController.GetInstance();
            _tourStartDateTimeController = TourStartDateTimeController.GetInstance();

            _tours = _tourController.GetAll();
            _reservations = _tourReservationController.GetAll();
            _tourStartDateTimes = _tourStartDateTimeController.GetAll();


            LoadComboBox();
            LoadTours();


        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        
        

        private void LoadComboBox() {
            CbYears.Items.Add("All");
            CbYears.SelectedItem = CbYears.Items[0];
           
            foreach (TourStartDateTime tourStartDateTime in _tourStartDateTimes)
            {
                string Year = tourStartDateTime.StartDateTime.Date.Year.ToString();
                if (!CbYears.Items.Contains(Year))
                {
                    CbYears.Items.Add(Year); 
                }
            }
        }
        

        private void FindMostVisitedTour(object sender, SelectionChangedEventArgs e) {

            MaxSum = 0;
            int? currentSum = 0;

            foreach (Tour tour in _tours)
            {
                foreach (TourStartDateTime tourStartDateTime in tour.TourStartDateAndTime)
                {

                    bool isAllSelected = CbYears.SelectedIndex == 0;
                    string currentYear = tourStartDateTime.StartDateTime.Date.Year.ToString();

                    bool Valid = isAllSelected ? tourStartDateTime.Finished : tourStartDateTime.Finished && currentYear.Equals(CbYears.SelectedItem);
                    if (Valid)
                    {
                        currentSum += CalculateGuests(tourStartDateTime);

                    }


                }
                if (currentSum > MaxSum)
                {
                    MaxSum = currentSum;
                    mostVisitedTour = tour;
                   

                }
               
                currentSum = 0;
            }

        }
        
        private int? CalculateGuests(TourStartDateTime tourStartDateTime) {
            int? sum = 0; 
            List<TourReservation> validReservations = _reservations.FindAll(r => r.TourStartDateTimeId == tourStartDateTime.Id);
            foreach (TourReservation reservation in validReservations) 
            {
                sum += reservation.GuestNumber; 
            }
            return sum; 
        }
        private void LoadTours()
        {

            foreach (Tour t in _tours)
            {
                LstTours.Items.Add(t);
            }

        }

        private void BtnShowCharts_Click(object sender, RoutedEventArgs e) {
            if (LstTours.SelectedIndex != -1)
            {
                selectedTour = (Tour)LstTours.SelectedItem;
                ChartsForm chartsForm = new ChartsForm();
                chartsForm.Owner = this; 
                chartsForm.ShowDialog();
            }
            else 
            {
                MessageBox.Show("You must select tour", "Error", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
        
        }
    }
}
