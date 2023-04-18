using SIMS_Project.Application.UseCases;
using SIMS_Project.Commands;
using SIMS_Project.Commands.Owner;
using SIMS_Project.Model;
using SIMS_Project.Stores.Owner;
using SIMS_Project.View.Guest2;
using SIMS_Project.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SIMS_Project.ViewModel.Guest2
{
    public class TourRequestsViewModel : ViewModelBase
    {
        private readonly User _signedInGuest2;
        private readonly TourRequestService _tourRequestService;
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public ICommand OpenTourRequestFormCommand { get; }

        public TourRequestsViewModel(User signedInGuest2) 
        {
            _signedInGuest2 = signedInGuest2;
            _tourRequestService = TourRequestService.GetInstance();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetAll());
            OpenTourRequestFormCommand = new OpenWindowCommand(typeof(TourRequestForm), _signedInGuest2, TourRequests);
        }
    }
}
