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
using SIMS_Project.Model.DTO.Notifications;
using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.ViewModel.Owner;

namespace SIMS_Project.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerMain.xaml
    /// </summary>
    public partial class OwnerMain : Window
    {
        public OwnerMain(User owner)
        {
            InitializeComponent();
            //DataContext = new MainViewModel(owner);
        }

        //private void BtnRegisterAccomodation_Click(object sender, RoutedEventArgs e)
        //{
        //    AccommodationRegistrationForm accommodationRegistrationForm = new AccommodationRegistrationForm(SignedInOwner);
        //    accommodationRegistrationForm.Owner = this;
        //    accommodationRegistrationForm.ShowDialog();
        //}

        //private void BtnNotificationAction_Click(object sender, RoutedEventArgs e)
        //{
        //    if(SelectedNotification.TriggerAction())
        //    {
        //        Notifications.Remove(SelectedNotification);
        //    }
        //}      
    }
}
