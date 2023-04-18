using SIMS_Project.Commands;
using SIMS_Project.Commands.Owner;
using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Resources;
using SIMS_Project.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIMS_Project.Stores.Owner;
using SIMS_Project.Stores;

namespace SIMS_Project.ViewModel.Owner
{
    public class AccommodationRegistrationViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UserStore _userStore;
        private readonly AccommodationService _accommodationService;
        private readonly LocationService _locationService;
        private readonly FileManager _fileManager;
        public Accommodation NewAccommodation { get; set; }
        public User SignedInOwner => _userStore.SignedInUser;

        public ObservableCollection<string> AccommodationTypes { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public ObservableCollection<string> ImagePaths { get; set; }

        public ICommand AddImagesCommand { get; }
        //public ICommand RemoveImagesCommand { get; }
        public ICommand AccommodationRegisterCommand { get; }
        public ICommand CloseWindowCommand { get; }

        public AccommodationRegistrationViewModel(UserStore userStore, NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _userStore = userStore;
            _accommodationService = AccommodationService.GetInstance();
            _locationService = LocationService.GetInstance();
            _fileManager = new FileManager();

            InitAccommodationTypes();
            InitLocations();
            ImagePaths = new ObservableCollection<string>();

            NewAccommodation = new Accommodation();
            NewAccommodation.OwnerId = SignedInOwner.Id;
            NewAccommodation.Owner = SignedInOwner;

            AddImagesCommand = new AddImagesCommand(this);
            AccommodationRegisterCommand = new AccommodationRegistrationCommand(this, _navigationStore, _userStore, _modalNavigationStore);
            CloseWindowCommand = new CloseWindowCommand();
        }

        private void InitLocations()
        {
            Countries = new ObservableCollection<string>();
            Cities = new ObservableCollection<string>();

            List<Location> locations = _locationService.GetAll();
            foreach (Location location in locations)
            {
                Countries.Add(location.Country);
                Cities.Add(location.City);
            }
        }

        private void InitAccommodationTypes()
        {
            AccommodationTypes = new ObservableCollection<string>();
            AccommodationTypes.Add("APARTMENT");
            AccommodationTypes.Add("HUT");
            AccommodationTypes.Add("HOUSE");
        }

    }
}
