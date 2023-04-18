using SIMS_Project.Commands.Owner;
using SIMS_Project.Model;
using SIMS_Project.Stores;
using SIMS_Project.Stores.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMS_Project.ViewModel.Owner
{
    public class AccommodationDetailsViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UserStore _userStore;
        public Accommodation SelectedAccommodation { get; }

        public ICommand NavigateAccommodationReservationsCommand { get; }
        public ICommand NavigateAccommodationReviewsCommand { get; }

        public AccommodationDetailsViewModel(NavigationStore navigationStore, UserStore userStore, Accommodation selectedAccommodation, ModalNavigationStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _userStore = userStore;
            SelectedAccommodation = selectedAccommodation;

            NavigateAccommodationReservationsCommand = new NavigateCommand<AccommodationReservationsViewModel>(_navigationStore, () => new AccommodationReservationsViewModel(_navigationStore, _userStore, SelectedAccommodation, _modalNavigationStore));
            NavigateAccommodationReviewsCommand = new NavigateCommand<AccommodationReviewsViewModel>(_navigationStore, () => new AccommodationReviewsViewModel(_navigationStore, _userStore, SelectedAccommodation, _modalNavigationStore));
        }


    }
}
