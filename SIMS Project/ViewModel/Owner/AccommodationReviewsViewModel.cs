using SIMS_Project.Application.UseCases;
using SIMS_Project.Commands.Owner;
using SIMS_Project.Model;
using SIMS_Project.Model.DTO.Owner;
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
    public class AccommodationReviewsViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UserStore _userStore;

        private readonly AccommodationReviewService _accommodationReviewService;
        public Accommodation SelectedAccommodation { get; }

        public ObservableCollection<ShortGuestReview> AccommodationReviews { get; set; }
        public ICommand NavigateAccommodationDetailsCommand { get; }
        public ICommand NavigateAccommodationReservationsCommand { get; }

        public AccommodationReviewsViewModel(NavigationStore navigationStore, UserStore userStore, Accommodation selectedAccommodation, ModalNavigationStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _userStore = userStore;
            SelectedAccommodation = selectedAccommodation;

            _accommodationReviewService = AccommodationReviewService.GetInstance();
            AccommodationReviews = new ObservableCollection<ShortGuestReview>();
            LoadAccommodationReviews();

            NavigateAccommodationDetailsCommand = new NavigateCommand<AccommodationDetailsViewModel>(_navigationStore, () => new AccommodationDetailsViewModel(_navigationStore, _userStore, SelectedAccommodation, _modalNavigationStore));
            NavigateAccommodationReservationsCommand = new NavigateCommand<AccommodationReservationsViewModel>(_navigationStore, () => new AccommodationReservationsViewModel(_navigationStore, _userStore, SelectedAccommodation, _modalNavigationStore));
        }

        private void LoadAccommodationReviews()
        {
            List<AccommodationReview> accommodationReviews = _accommodationReviewService.GetReviewsByAccommodationId(SelectedAccommodation.Id);
            foreach (AccommodationReview accommodationReview in accommodationReviews)
            {
                AccommodationReviews.Add(new ShortGuestReview(accommodationReview));
            }
        }
    }
}
