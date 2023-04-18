using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Model.Enums;
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
    /// Interaction logic for AccommodationMoveReservationForm.xaml
    /// </summary>
    public partial class AccommodationMoveReservationForm : Window
    {
        public AccommodationReservation SelectedReservation { get; set; }

        public ReservationMoveRequestController _controller { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
 
        public AccommodationMoveReservationForm(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedReservation = selectedReservation;
            _controller = ReservationMoveRequestController.GetInstance();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            ReservationMoveRequest request = new ReservationMoveRequest(_controller.NextId(), SelectedReservation.Id, ReservationMoveStatus.ON_WAIT, false, "", DateOnly.FromDateTime(Start), DateOnly.FromDateTime(End), false);
            _controller.Add(request);
            MessageBox.Show("Request successfully made! Now wait for owner to approve it.", "Reservation", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseWindows();
        }

        public void CloseWindows()
        {
            this.Close();
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AccommodationReservationMoveAndCancel))
                {
                    window.Close();
                }
            }
        }
    }
}
