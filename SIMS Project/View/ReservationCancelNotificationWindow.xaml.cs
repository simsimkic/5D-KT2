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
    /// Interaction logic for ReservationCancelNotificationWindow.xaml
    /// </summary>
    public partial class ReservationCancelNotificationWindow : Window
    {
        public ReservationCancelNotification ReservationCancelNotification { get; set; }
        public ObservableCollection<ReservationCancelNotification> Notifications { get; set; }

        public ReservationCancelNotificationController _controller { get; set; }  
        public ReservationCancelNotificationWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _controller = ReservationCancelNotificationController.GetInstance();
            Notifications = new ObservableCollection<ReservationCancelNotification>(_controller.GetAllNonDelivered(user.Id));


        }
    }
}
