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
    /// Interaction logic for CancelTourForm.xaml
    /// </summary>
    public partial class CancelTourForm : Window
    {
        public TourStartDateTime SelectedTourDateTime { get; set; }
        private readonly TourStartDateTimeController _tourStartDateTimeController;
        private readonly TourReservationController _tourReservationController;
        private readonly VoucherController _voucherController;
       
        private List<TourStartDateTime> _toursStartDateTimes;
        private List<TourReservation> _tourReservations;
        private List<Voucher> _vouchers; 
      
        public CancelTourForm()
        {
            InitializeComponent();
            DataContext = this;

            SelectedTourDateTime = new TourStartDateTime(); 

            _tourStartDateTimeController = TourStartDateTimeController.GetInstance();
            _toursStartDateTimes = _tourStartDateTimeController.GetAll();

            _tourReservationController = TourReservationController.GetInstance();
            _tourReservations = _tourReservationController.GetAll(); 

            _voucherController = VoucherController.GetInstance();
            _vouchers = _voucherController.GetAll(); 


            LoadToursIntoListBox(); 
        }

        private void LoadToursIntoListBox()
        {

            foreach (TourStartDateTime t in _toursStartDateTimes) 
            {
                int dateDifference = (t.StartDateTime.Year - DateTime.Now.Year)*12 * 30 * 24 + 
                                    (t.StartDateTime.Month  - DateTime.Now.Month)*30*24 + 
                                    (t.StartDateTime.Day - DateTime.Now.Day)*24 +
                                    (t.StartDateTime.Hour - DateTime.Now.Hour); 
                
                if (!t.TourDeleted && dateDifference > 48) 
                {
                    LstTours.Items.Add(t); 
                } 
            }

        }

        private void BtnCancelTour_Click(object sender, RoutedEventArgs e) {

            if (LstTours.SelectedItem != null)
            {

                SelectedTourDateTime = (TourStartDateTime)LstTours.SelectedItem;

                LogicallyDeleteReservations();
                LogicallyDeleteTourStartDate();
                GiftVoucher();

            }
            else
            {
                MessageBox.Show("Must selected at least one tour!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        private void LogicallyDeleteTourStartDate()
        {
            

            TourStartDateTime t = _toursStartDateTimes.Find(t => t.Id == SelectedTourDateTime.Id);

            t.TourDeleted = true;
            LstTours.Items.Remove(SelectedTourDateTime);
            _tourStartDateTimeController.Save();

        }
        private void LogicallyDeleteReservations()
        {
            List<TourReservation> reservations = _tourReservations.FindAll(t => t.TourStartDateTimeId == SelectedTourDateTime.Id);

            if (reservations.Count > 0) {
                foreach (TourReservation t in reservations) {
                     t.TourDeleted = true; 
                       
                }
            }
            _tourReservationController.Save(); 
            
        }
        private void GiftVoucher() 
        {
            foreach (TourReservation tr in _tourReservations)
            {
                if (tr.TourStartDateTimeId == SelectedTourDateTime.Id) 
                {
                    Voucher v = new Voucher(-1,tr.Guest2Id,tr.Guest2, DateTime.Now);
                    _voucherController.Add(v); 
                }
            }
        }
    }
  
}
