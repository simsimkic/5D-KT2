using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Resources;
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
    /// Interaction logic for AccommodationOwnerRate.xaml
    /// </summary>
    public partial class AccommodationOwnerRateForm : Window, INotifyPropertyChanged
    {
        private readonly FileManager _fileManager;
        public event PropertyChangedEventHandler? PropertyChanged;

        public AccommodationOwnerRate Rate { get; set; }
        public AccommodationOwnerRateController _controller { get; set; }
        public AccommodationReservation _accommodationReservation { get; set; }

        public AccommodationOwnerRateForm(AccommodationReservation reservation, User user)
        {
            InitializeComponent();
            this.DataContext = this;
            _fileManager = new FileManager();
            _accommodationReservation = reservation;
            _controller = AccommodationOwnerRateController.GetInstance();
            Rate = new AccommodationOwnerRate();
            Rate.AccommodationReservation = _accommodationReservation;
            Rate.AccommodationReservationId = _accommodationReservation.Id;
            Rate.UserId = user.Id;                                                       

        }

        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            List<string> images = _fileManager.BrowseImages();
            foreach (string imageName in images)
            {
                ListBoxImages.Items.Add(imageName);
            }
        }

        private void BtnRemoveImage_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxImages.SelectedIndex != -1)
            {
                for (int i = ListBoxImages.SelectedItems.Count - 1; i >= 0; i--)
                    ListBoxImages.Items.Remove(ListBoxImages.SelectedItems[i]);
            }
        }

        private List<string> GetImagesFromListBox()
        {
            List<string> images = new List<string>();
            foreach (string image in ListBoxImages.Items)
            {
                images.Add(image);
            }

            return images;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Rate.Images = GetImagesFromListBox();
            _controller.Add(Rate);
            MessageBox.Show("You have successfully rated accommodation and owner", "Rate accommodaton and owner", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseWindows();

        }

        public void CloseWindows()
        {
            this.Close();
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AccommodationOwnerRateWindow))
                {
                    window.Close();
                }
            }
        }
    }
}
