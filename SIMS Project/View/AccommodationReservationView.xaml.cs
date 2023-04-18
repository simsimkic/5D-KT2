using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AccommodationReservationView.xaml
    /// </summary>
    public partial class AccommodationReservationView : Window, INotifyPropertyChanged
    {
        public Accommodation SelectedAccommodation { get; set; }

        public AccommodationReservationController _controller;
        //public AccommodationReservation Reservation { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfDays { get; set; }
        public User Guest { get; set; }
        public AccommodationReservationView(Accommodation selectedAccommodation, User guest)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedAccommodation = selectedAccommodation;
            _controller = AccommodationReservationController.GetInstance();
            //Reservation = new AccommodationReservation();
            Guest = guest;
        }

        private void FindDates_Click(object sender, RoutedEventArgs e)
        {
            if (NumOfDays >= SelectedAccommodation.MinDays && StartDate >= DateTime.Now && StartDate < EndDate && int.TryParse(TextBoxNumOfDays.Text, out _) && (EndDate - StartDate).TotalDays >= NumOfDays)
            {
                DateOnly start = DateOnly.FromDateTime(StartDate);
                DateOnly end = DateOnly.FromDateTime(EndDate);
                List<AccommodationReservationDTO> accommodationReservationDTO = _controller.FindFreeTimeIntervalForAccommodation(SelectedAccommodation.Id, start, end, NumOfDays);

                if (!accommodationReservationDTO.Any())
                {
                    // If there are no avaliable accommodations, check for a month after the end date (for the same number of days)
                    DateOnly startDate = DateOnly.FromDateTime(EndDate);
                    DateOnly endDate = DateOnly.FromDateTime(EndDate.AddDays(30));
                    accommodationReservationDTO = _controller.FindFreeTimeIntervalForAccommodation(SelectedAccommodation.Id, startDate, endDate, NumOfDays);
                }
                AccommodationReservationSelectDates selectDates = new AccommodationReservationSelectDates(accommodationReservationDTO, SelectedAccommodation, Guest);
                selectDates.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid data!", "Accommodation Reservation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
