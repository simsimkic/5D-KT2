using SIMS_Project.Controller;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddKeyPoint.xaml
    /// </summary>
    public partial class AddKeyPoint : Window,INotifyPropertyChanged
    {
   
        public event PropertyChangedEventHandler? PropertyChanged;
        public KeyPoint NewKeyPoint { get; set; }
        TourCreationForm _owner;
        public AddKeyPoint(TourCreationForm owner)
        {
            InitializeComponent();
            DataContext = this;

            _owner = owner;
            NewKeyPoint = new KeyPoint(); 

        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            this.Close(); 
        }
        private void BtnConfirm_Click(object sender, RoutedEventArgs e) {

            if (NewKeyPoint.IsValid)
            {
                
                _owner.LstKeyPoints.Items.Add(NewKeyPoint);
              
                this.Close();
            }
            else
            {
               
                MessageBox.Show("Invalid input", "Adding keypoint error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
           

        }
    }
}
