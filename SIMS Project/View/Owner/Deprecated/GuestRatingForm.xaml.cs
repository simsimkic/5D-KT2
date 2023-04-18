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
using System.Collections.ObjectModel;
using SIMS_Project.Model;
using SIMS_Project.Controller;

namespace SIMS_Project.View.Owner
{
    /// <summary>
    /// Interaction logic for GuestRatingForm.xaml
    /// </summary>
    public partial class GuestRatingForm : Window
    {
        private RatingQuestionController _ratingQuestionController { get; set; }
        private GuestRatingController _guestRatingController { get; set; }
        
        public GuestRating NewGuestRating { get; set; }
        public ObservableCollection<GuestRatingParameter> RatingParameters { get; set; }

        public GuestRatingForm(AccommodationReservation reservation)
        {
            InitializeComponent();
            DataContext = this;
            NewGuestRating = new GuestRating();
            NewGuestRating.Reservation = reservation;

            _guestRatingController = GuestRatingController.GetInstance();
            _ratingQuestionController = RatingQuestionController.GetInstance();
            RatingParameters = new ObservableCollection<GuestRatingParameter>();

            LoadQuestions();
        }

        private void LoadQuestions()
        {
            List<RatingQuestion> questions = _ratingQuestionController.GetAll();
            foreach (RatingQuestion question in questions)
            {
                RatingParameters.Add(new GuestRatingParameter(0, 0, question));
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            NewGuestRating.Parameters = RatingParameters.ToList();
            _guestRatingController.Add(NewGuestRating);
            MessageBox.Show("You have successfuly rated your guest!", "Guest Rating", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }
    }
}
