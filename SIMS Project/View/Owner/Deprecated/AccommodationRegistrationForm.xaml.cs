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
using System.ComponentModel;
using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Resources;
using SIMS_Project.ViewModel.Owner;

namespace SIMS_Project.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationForm.xaml
    /// </summary>
    public partial class AccommodationRegistrationForm : Window
    {
        public AccommodationRegistrationForm(User signedInOwner)
        {
            InitializeComponent();
            //DataContext = new AccommodationRegistrationViewModel(signedInOwner);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRemoveImage_Click(object sender, RoutedEventArgs e)
        {
            if (LstImageNames.SelectedIndex != -1)
            {
                for (int i = LstImageNames.SelectedItems.Count - 1; i >= 0; i--)
                    LstImageNames.Items.Remove(LstImageNames.SelectedItems[i]);
            }
        }
    }
}