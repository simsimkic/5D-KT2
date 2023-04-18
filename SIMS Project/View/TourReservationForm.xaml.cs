using SIMS_Project.Controller;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interaction logic for TourReservationForm.xaml
    /// </summary>
    public partial class TourReservationForm: Window, INotifyPropertyChanged
    {
        private readonly TourStartDateTimeController _tourStartController;
        private readonly TourReservationController _tourReservationController;
        private TourStartDateTime _tourStart;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TourStartDateTime TourStart 
        {
            get => _tourStart;
            set
            {
                if (_tourStart != value) 
                { 
                    _tourStart = value;
                    OnPropertyChanged();
                }
            }
        }
        public TourReservation TourReservation { get; set; }
        public ObservableCollection<TourStartDateTime> TourStartSuggestions { get; set; }

        public TourReservationForm(User signedInGuest2, TourStartDateTime tourStart)
        {
            InitializeComponent();
            DataContext = this;

            TourStart = tourStart;
            TourReservation = new TourReservation();
            TourReservation.Guest2Id = signedInGuest2.Id;
            TourReservation.Guest2 = signedInGuest2;
            _tourStartController = TourStartDateTimeController.GetInstance();
            _tourReservationController = TourReservationController.GetInstance();

            (string, string) tourStartLocationPair = (TourStart.Tour.Location.City, TourStart.Tour.Location.Country);
            TourStartSuggestions = new ObservableCollection<TourStartDateTime>
                (
                _tourStartController.GetAll().FindAll(x => (x.Tour.Location.City, x.Tour.Location.Country) == tourStartLocationPair)
                );
        }

        private void AddReservation()
        {
            TourReservation.TourStartDateTimeId = TourStart.Id;
            TourReservation.TourStartDateTime = TourStart;

            _tourReservationController.Add(TourReservation);
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            TryToReserve();
        }

        private void BtnSelectTourStartSuggestion_Click(object sender, RoutedEventArgs e)
        {
            TourStart = (TourStartDateTime)ListBoxTourStartSuggestions.SelectedItem;

            GridTourStartSuggestions.Visibility = Visibility.Hidden;
            StackPanelTourStartSelection.IsEnabled = true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TryToReserve()
        {
            int availableGuestNumber = _tourReservationController.GetAvailableGuestNumber(TourStart);
            int remainingGuestNumber = availableGuestNumber - (int)TourReservation.GuestNumber;

            if (remainingGuestNumber >= 0)
            {
                AddReservation();

                string personOrPeople = TourReservation.GuestNumber == 1 ? "person" : "people";
                MessageBox.Show(string.Format("Successfully booked tour for {0} {1}", TourReservation.GuestNumber, personOrPeople));

                this.Close();
            }
            else if (availableGuestNumber > 0)
            {
                MessageBox.Show(string.Format("Booking tour for {0} people not possible, only {1} available spots remaining",
                    TourReservation.GuestNumber, availableGuestNumber));
            }
            else
            {
                MessageBox.Show("Tour is fully booked, showing other tours at the same location instead");

                TourStartSuggestions.Remove(TourStart);
                GridTourStartSuggestions.Visibility = Visibility.Visible;
                StackPanelTourStartSelection.IsEnabled = false;
            }
        }

        private void ListBoxTourStartSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnSelectTourStartSuggestion.IsEnabled = ListBoxTourStartSuggestions.SelectedItem != null;
        }
    }
}
