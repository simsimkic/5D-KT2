using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Model.DAO;
using SIMS_Project.Model.DTO;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SIMS_Project.View;
using System.Text.RegularExpressions;

namespace SIMS_Project.View
{
    /// <summary>
    /// Interaction logic for AccommodationReservationSelectDates.xaml
    /// </summary>
    public partial class AccommodationReservationSelectDates : Window, INotifyPropertyChanged
    {
        public AccommodationReservationDTO SelectedDate { get; set; }
        //public ObservableCollection<AccommodationReservationDTO> AccommodationReservationDTO { get; set; }
        public List<AccommodationReservationDTO> AccommodationReservationDTO { get; set;}

        AccommodationReservationController _reservationController;

        public Accommodation accommodation;
        public AccommodationReservation accommodationReservation;
        public int NumberOfGuests { get; set; }
        public User Guest { get; set; }

        public AccommodationReservationSelectDates(List<AccommodationReservationDTO> accommodationReservationDTO,Accommodation acc, User guest)
        {
            InitializeComponent();
            this.DataContext = this;
            //AccommodationReservationDTO = new ObservableCollection<AccommodationReservationDTO>(accommodationReservationDTO);
            AccommodationReservationDTO = accommodationReservationDTO;
            accommodation = acc;
            _reservationController = AccommodationReservationController.GetInstance();
            Guest = guest;
        }

        private void ConfirmReservation_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedDate!=null && NumberOfGuests>0 && NumberOfGuests <= accommodation.MaxGuests  && int.TryParse(TextBoxNumberOfGuests.Text, out _))
            {
                accommodationReservation = new AccommodationReservation(_reservationController.NextId(),accommodation.Id, accommodation,SelectedDate.Start,SelectedDate.End, NumberOfGuests, false, Guest);
                _reservationController.Add(accommodationReservation);
                MessageBox.Show("Accommodation successfully booked!", "Reservation", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                this.Close();
                CloseWindows();
            }
            else
            {
                MessageBox.Show("Invalid data!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CloseWindows()
        {
            this.Close();
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(AccommodationReservationView))
                {
                    window.Close();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
