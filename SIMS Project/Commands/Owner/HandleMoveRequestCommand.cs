using SIMS_Project.Application.UseCases;
using SIMS_Project.Model;
using SIMS_Project.Stores;
using SIMS_Project.Stores.Owner;
using SIMS_Project.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Commands.Owner
{
    public class HandleMoveRequestCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UserStore _userStore;
        private readonly Accommodation _selectedAccommotaion;
        private ReservationMoveRequest _handledRequest;
        private readonly ReservationMoveRequestService _moveRequestService;

        public HandleMoveRequestCommand(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore, ReservationMoveRequest handledRequest, UserStore userStore, Accommodation selectedAccommotaion)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _handledRequest = handledRequest;
            _userStore = userStore;
            _selectedAccommotaion = selectedAccommotaion;
            
            _moveRequestService = ReservationMoveRequestService.GetInstance();
        }

        public override void Execute(object? parameter)
        {
            _moveRequestService.HandleRequest(_handledRequest, bool.Parse((string)parameter));
            _modalNavigationStore.Close();
            _navigationStore.CurrentViewModel = new AccommodationReservationsViewModel(_navigationStore, _userStore, _selectedAccommotaion, _modalNavigationStore);
        }
    }
}
