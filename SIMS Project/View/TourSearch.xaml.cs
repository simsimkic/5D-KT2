using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for TourSearch.xaml
    /// </summary>
    public partial class TourSearch : Window
    {
        private readonly TourStartDateTimeController _tourStartDateTimeController;
        private readonly TourReservationController _tourReservationController;
        private List<TourStartDateTime> _tempTourStarts;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<TourStartDateTime> TourStarts { get; set; }
        public Location? SetLocation { get; set; }
        public int? SetDuration { get; set; }
        public string? SetLanguage { get; set; }
        public int? SetGuestNumber { get; set; }
        public User SignedInGuest2 { get; set; }

        public TourSearch(User signedInGuest2)
        {
            InitializeComponent();
            DataContext = this;

            _tourStartDateTimeController = TourStartDateTimeController.GetInstance();
            _tourReservationController = TourReservationController.GetInstance();
            TourStarts = new ObservableCollection<TourStartDateTime>();
            SignedInGuest2 = signedInGuest2;

            Search();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void BtnTourReservation_Click(object sender, RoutedEventArgs e)
        {
            TourReservationForm tourReservationForm = new(SignedInGuest2, (TourStartDateTime)ListBoxTours.SelectedItem);
            tourReservationForm.Owner = this;
            tourReservationForm.ShowDialog();
        }

        private void ListBoxTours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnTourReservation.IsEnabled = ListBoxTours.SelectedItem != null;
        }

        private void Search()
        {
            TourStarts.Clear();
            _tempTourStarts = _tourStartDateTimeController.GetAll();


            SearchByLocation();
            SearchByDuration();
            SearchByLanguage();
            SearchByGuestNumber();

            foreach(TourStartDateTime t in _tempTourStarts)
            {
                if (!t.TourDeleted) 
                {
                    TourStarts.Add(t);
                }

            }
        }

        private void SearchByLocation()
        {
            if (SetLocation != null)
            {
                _tempTourStarts = _tempTourStarts
                    .FindAll(x => (x.Tour.Location.City, x.Tour.Location.Country).Equals((SetLocation.City, SetLocation.Country)));
            }
        }

        private void SearchByDuration()
        {
            Tour t = (Tour)ListBoxTours.SelectedItem;
            if (SetDuration != null)
            {
                _tempTourStarts = _tempTourStarts.FindAll(x => x.Tour.TourDuration.Equals(SetDuration));
            }
        }

        private void SearchByLanguage()
        {
            if (!string.IsNullOrEmpty(SetLanguage))
            {
                _tempTourStarts = _tempTourStarts.FindAll(x => x.Tour.Language.ToLower().Equals(SetLanguage.ToLower()));
            }
        }

        private void SearchByGuestNumber()
        {
            if (SetGuestNumber != null)
            {
                _tempTourStarts = _tempTourStarts
                    .FindAll(x => _tourReservationController.GetAvailableGuestNumber(x) >= SetGuestNumber);
            }
        }
    }
}
