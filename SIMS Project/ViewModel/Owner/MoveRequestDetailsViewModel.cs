using SIMS_Project.Application.UseCases;
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
    public class MoveRequestDetailsViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UserStore _userStore;

        private readonly AccommodationReservationService _reservationService;

        public ReservationMoveRequest SelectedMoveRequest { get; set; }
        public bool Intertwines { get; }

        public ICommand CloseModalCommand { get; }
        public ICommand HandleMoveRequestCommand { get; }

        public MoveRequestDetailsViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore, UserStore userStore, ReservationMoveRequest selectedMoveRequest)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _userStore = userStore;
            _reservationService = AccommodationReservationService.GetInstance();

            SelectedMoveRequest = selectedMoveRequest;
            CloseModalCommand = new CloseModalCommand(_modalNavigationStore);
            HandleMoveRequestCommand = new HandleMoveRequestCommand(_navigationStore, _modalNavigationStore, SelectedMoveRequest, _userStore, SelectedMoveRequest.AccommodationReservation.Accommodation);
            Intertwines = _reservationService.DoReservationsIntertwine(SelectedMoveRequest.Start, SelectedMoveRequest.End, SelectedMoveRequest.AccommodationReservation.AccomodationId);
        }
    }
}
