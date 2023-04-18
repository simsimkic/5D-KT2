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
    /// <summary>
    /// Interaction logic for MoveRequestsNotifications.xaml
    /// </summary>
    public partial class MoveRequestsNotifications : Window
    {
        public ReservationMoveRequest SelectedRequest { get; set; }
        public ObservableCollection<ReservationMoveRequest> ReservationMoveRequests { get; set; }
        public ReservationMoveRequestController _requestsController { get; set; }

        public MoveRequestsNotifications(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _requestsController = ReservationMoveRequestController.GetInstance();
            ReservationMoveRequests = new ObservableCollection<ReservationMoveRequest>(_requestsController.GetAllMoveRequestsForUserChanged(user.Id));
        }
    }
}
