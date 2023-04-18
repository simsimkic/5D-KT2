using SIMS_Project.Controller;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    
    public partial class AccommodationView : Window
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private List<Accommodation> _accommodations { get; set; }
        public AccommodationController _accommodationController { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public User Guest { get; set; }
        public string AccName { get; set; }
        public string Location { get; set; }  
        public int Guests { get; set; }
        public int Days { get; set; }

        public string SearchValue { get; set; }
        public AccommodationView(User guest)
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = AccommodationController.GetInstance();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
            ComboBoxType.Items.Add("HUT");
            ComboBoxType.Items.Add("HOUSE");;
            ComboBoxType.Items.Add("APARTMENT");
            Guest = guest;
        }

        private void UpdateAccommodationView(IEnumerable<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach(Accommodation accommodation in accommodations)
            {
                Accommodations.Add(accommodation);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Accommodation> accommodations = _accommodationController.SearchAccomodations(AccName, Location, ComboBoxType.Text, Guests,Days);
            UpdateAccommodationView(accommodations);
        }

        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedAccommodation != null)
            {
                AccommodationReservationView accommodationReservation = new AccommodationReservationView(SelectedAccommodation, Guest);
                accommodationReservation.ShowDialog();
            }
            else
            {
                MessageBox.Show("None of the accommodations were selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnCancelSearch_Click(object sender, RoutedEventArgs e)
        {
            UpdateAccommodationView(_accommodationController.GetAll());
            TextBoxName.Text = null;
            TextBoxLocation.Text = null;
            ComboBoxType.Text = null;
            TextBoxGuests.Text = "0";
            TextBoxDays.Text = "0";
        }
    }
}
