using SIMS_Project.Application.UseCases;
using SIMS_Project.Commands.Owner;
using SIMS_Project.Model;
using SIMS_Project.Stores;
using SIMS_Project.Stores.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMS_Project.ViewModel.Owner
{
    public class AccommodationOverviewViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UserStore _userStore;

        private AccommodationService _accommodationService;
        public User SignedInOwner => _userStore.SignedInUser;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public ICommand NavigateAccommodationRegistrationCommand { get; }
        public ICommand NavigateAccommodationDetailsCommand { get; }
        public AccommodationOverviewViewModel(UserStore userStore, NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _userStore = userStore;

            _accommodationService = AccommodationService.GetInstance();
            Accommodations = new ObservableCollection<Accommodation>();
            SelectedAccommodation = new Accommodation();
            InitAccommodationList();

            NavigateAccommodationRegistrationCommand = new NavigateCommand<AccommodationRegistrationViewModel>(navigationStore, () => new AccommodationRegistrationViewModel(_userStore, navigationStore, modalNavigationStore));
            NavigateAccommodationDetailsCommand = new NavigateCommand<AccommodationDetailsViewModel>(_navigationStore, () => new AccommodationDetailsViewModel(_navigationStore, _userStore, SelectedAccommodation, _modalNavigationStore));
        }

        private void InitAccommodationList()
        {
            List<Accommodation> accommodations = _accommodationService.GetByOwnerId(SignedInOwner.Id);
            foreach (Accommodation accommodation in accommodations)
            {
                Accommodations.Add(accommodation);
            }
        }
    }
}
