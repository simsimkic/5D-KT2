using SIMS_Project.Application.UseCases;
using SIMS_Project.Commands;
using SIMS_Project.Commands.Owner;
using SIMS_Project.Model;
using SIMS_Project.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.ViewModel.Guest2
{
    public class TourRequestFormViewModel : ViewModelBase
    {
        private User _signedInGuest2;
        private TourRequestService _tourRequestService;
        private readonly ObservableCollection<TourRequest> _tourRequests;
        public TourRequest NewTourRequest { get; set; }
        public RelayCommand AddTourRequestCommand { get; }
        public Action CloseAction { get; set; }

        public TourRequestFormViewModel(User signedInGuest2, ObservableCollection<TourRequest> tourRequests, Action closeAction) 
        {
            _signedInGuest2 = signedInGuest2;
            _tourRequests = tourRequests;
            CloseAction = closeAction;
            _tourRequestService = TourRequestService.GetInstance();
            NewTourRequest = new TourRequest
            {
                Guest2Id = _signedInGuest2.Id,
                Guest2 = _signedInGuest2,
                Status = TourRequestStatus.ON_WAIT
            };
            AddTourRequestCommand = new RelayCommand(x => AddTourRequest(), x => NewTourRequest.IsValid);
        }

        private void AddTourRequest()
        {
            _tourRequestService.AddRequest(NewTourRequest);
            _tourRequests.Add(NewTourRequest);
            CloseAction();
        }
    }
}
