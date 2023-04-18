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
    /// Interaction logic for TourCreationForm.xaml
    /// </summary>
    public partial class TourCreationForm : Window,INotifyPropertyChanged
    {
        private readonly TourController _tourController;
        private readonly TourStartDateTimeController _tourStartDateTimeController;
        private readonly KeyPointController _keyPointController; 
        
        public Tour NewTour { get; set; }
        private readonly FileManager _fileManager;
        public event PropertyChangedEventHandler? PropertyChanged;
        public List<string> Images { get; set; }
    
    
        public TourCreationForm()
        {
            InitializeComponent();
            DataContext = this;

            _tourController = TourController.GetInstance();
            _tourStartDateTimeController = TourStartDateTimeController.GetInstance();
            _keyPointController = KeyPointController.GetInstance();

            _fileManager = new FileManager();
            NewTour = new Tour();
            Images = new List<string>();

            InitCbTypes(); 
            MyDatePicker.SelectedDate= DateTime.Now; 


        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = NewTour.IsValid && LstImagePaths.Items.Count > 0 && LstKeyPoints.Items.Count >= 2 && LstDates.Items.Count > 0 && DistinctImages() && ValidDates(); 
            if (isValid)
            {
                
                NewTour.Images = GetItemsFromImageListBox();
                NewTour.KeyPoints = GetItemsFromKeyPointListBox();
                NewTour.TourStartDateAndTime = GetItemsFromDatesListBox(); 
                _tourController.Add(NewTour);
                AddTourToKeyPoints();
                AddTourToTourStarts();
      
                MessageBox.Show("You have successfully created new tour", "Tour creation", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid input", "Tour creation error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool ValidDates() {
            foreach (TourStartDateTime date in LstDates.Items) 
            {
                if(DateTime.Compare(date.StartDateTime,DateTime.Now) < 0)
                    return false;
                    
            }
            return true; 
        }
        private bool DistinctImages() {
           
            for (int i = 0; i < LstImagePaths.Items.Count; ++i) 
            {
                for (int j = 0; j < LstImagePaths.Items.Count; ++j)
                {
                    if (i != j && LstImagePaths.Items[i].Equals(LstImagePaths.Items[j]))
                        return false; 

                }

            }
            return true; 
        
        }
        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            List<string> images = _fileManager.BrowseImages();

            foreach (string image in images)
            {
                LstImagePaths.Items.Add(image);
            }
        }
        private void BtnRemoveImage_Click(object sender, RoutedEventArgs e)
        {
            if (LstImagePaths.SelectedIndex != -1)
            {
                for (int i = LstImagePaths.SelectedItems.Count - 1; i >= 0; i--)
                    LstImagePaths.Items.Remove(LstImagePaths.SelectedItems[i]);
            }
        }

       
        private void BtnAddDate_Click(object sender, RoutedEventArgs e)
        {
            int hours = (int)CbHours.SelectedValue;
            int minutes = (int)CbMinutes.SelectedValue;
            int year = MyDatePicker.SelectedDate.Value.Year;
            int month = MyDatePicker.SelectedDate.Value.Month;
            int day = MyDatePicker.SelectedDate.Value.Day; 

            DateTime dateTime = new DateTime(year,month,day,hours,minutes,0);

            TourStartDateTime tourStart = new TourStartDateTime();
            tourStart.StartDateTime = dateTime;
            LstDates.Items.Add(tourStart); 

        }
        private void BtnRemoveDate_Click(object sender, RoutedEventArgs e)
        {
            if (LstDates.SelectedIndex != -1)
            {
                for (int i = LstDates.SelectedItems.Count - 1; i >= 0; i--)
                    LstDates.Items.Remove(LstDates.SelectedItems[i]);
            }
        }
        private void BtnAddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            AddKeyPoint addKeyPoint = new AddKeyPoint(this);
            addKeyPoint.ShowDialog();
            addKeyPoint.Owner = this; 

        }
        private void BtnRemoveKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            if (LstKeyPoints.SelectedIndex != -1)
            {
                for (int i = LstKeyPoints.SelectedItems.Count - 1; i >= 0; i--)
                        LstKeyPoints.Items.Remove(LstKeyPoints.SelectedItems[i]);
            }
        }

        private List<string> GetItemsFromImageListBox()
        {
            
                List<string> items = new List<string>();
                foreach (string image in LstImagePaths.Items)
                {
                      items.Add(image);
                }

                return items;
            
        }
        private List<KeyPoint> GetItemsFromKeyPointListBox()
        {
            List<KeyPoint> items = new List<KeyPoint>();
            foreach (KeyPoint keypoint in LstKeyPoints.Items)
            {
                items.Add(keypoint);
            }

            return items;
        }
        private List<TourStartDateTime> GetItemsFromDatesListBox()
        {
            List<TourStartDateTime> items = new List<TourStartDateTime>();
            foreach (TourStartDateTime date in LstDates.Items)
            {
                items.Add(date);
            }

            return items;
        }
        private void AddTourToKeyPoints() {
            foreach (KeyPoint keypoint in LstKeyPoints.Items) 
            {
                keypoint.Tour = NewTour;
                keypoint.TourID = NewTour.Id;
                _keyPointController.Add(keypoint); 
            }
        }
        private void AddTourToTourStarts()
        {
            foreach (TourStartDateTime tourStart in LstDates.Items)
            {
                tourStart.Tour = NewTour;
                tourStart.TourId = NewTour.Id;
                _tourStartDateTimeController.Add(tourStart);
            }
        }
        private void InitCbTypes() {
            for (int i = 0; i <= 24; ++i) {
                CbHours.Items.Add(i);
            }
            CbHours.SelectedIndex = 0;
            for (int i = 0; i <= 60; i+=5)
            {
                CbMinutes.Items.Add(i);
            }
            CbMinutes.SelectedIndex = 0;

        }
    }
}
