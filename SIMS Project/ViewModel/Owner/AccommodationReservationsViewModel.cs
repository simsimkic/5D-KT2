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
    public class AccommodationReservationsViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UserStore _userStore;

        private readonly AccommodationReservationService _reservationService;
        private readonly ReservationMoveRequestService _moveRequestService;
        public Accommodation SelectedAccommodation { get; }
        public ReservationMoveRequest SelectedMoveRequest { get; set; }
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public ObservableCollection<ReservationMoveRequest> MoveRequests { get; set; }

        public ICommand NavigateAccommodationDetailsCommand { get; }
        public ICommand NavigateAccommodationReviewsCommand { get; }
        public ICommand ShowMoveRequestModalCommand { get; }

        public AccommodationReservationsViewModel(NavigationStore navigationStore, UserStore userStore, Accommodation selectedAccommodation, ModalNavigationStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _userStore = userStore;
            _reservationService = AccommodationReservationService.GetInstance();
            _moveRequestService = ReservationMoveRequestService.GetInstance();

            SelectedAccommodation = selectedAccommodation;
            SelectedMoveRequest = new ReservationMoveRequest();
            Reservations = new ObservableCollection<AccommodationReservation>();
            MoveRequests = new ObservableCollection<ReservationMoveRequest>();
            LoadReservations();
            LoadMoveRequests();

            NavigateAccommodationDetailsCommand = new NavigateCommand<AccommodationDetailsViewModel>(_navigationStore, () => new AccommodationDetailsViewModel(_navigationStore, _userStore, SelectedAccommodation, _modalNavigationStore));
            NavigateAccommodationReviewsCommand = new NavigateCommand<AccommodationReviewsViewModel>(_navigationStore, () => new AccommodationReviewsViewModel(_navigationStore, _userStore, SelectedAccommodation, _modalNavigationStore));
            ShowMoveRequestModalCommand = new NavigateModalCommand<MoveRequestDetailsViewModel>(_modalNavigationStore, () => new MoveRequestDetailsViewModel(_navigationStore, _modalNavigationStore, _userStore, SelectedMoveRequest));
        }

        private void LoadMoveRequests()
        {
            List<ReservationMoveRequest> moveRequests = _moveRequestService.GetNotHandled();
            foreach (ReservationMoveRequest moveRequest in moveRequests)
            {
                MoveRequests.Add(moveRequest);
            }
        }

        private void LoadReservations()
        {
            List<AccommodationReservation> reservations = _reservationService.GetAllValid();
            foreach (AccommodationReservation reservation in reservations)
            {
                Reservations.Add(reservation);
            }
        }
    }
}
