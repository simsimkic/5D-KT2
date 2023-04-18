using SIMS_Project.Application.UseCases;
using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Stores;
using SIMS_Project.Stores.Owner;
using SIMS_Project.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Project.Commands.Owner
{
    public class AccommodationRegistrationCommand : CommandBase
    {
        private AccommodationRegistrationViewModel _accommodationRegistrationViewModel;
        private AccommodationService _accommodationService;
        private NavigationStore  _navigationStore;
        private ModalNavigationStore  _modalNavigationStore;
        private UserStore _userStore;

        public AccommodationRegistrationCommand(AccommodationRegistrationViewModel accommodationRegistrationViewModel, NavigationStore navigationStore, UserStore userStore, ModalNavigationStore modalNavigationStore)
        {
            _accommodationRegistrationViewModel = accommodationRegistrationViewModel;
            _accommodationService = AccommodationService.GetInstance();
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _userStore = userStore;
        }

        public override void Execute(object? parameter)
        {
            if (_accommodationRegistrationViewModel.NewAccommodation.IsValid)
            {
                _accommodationRegistrationViewModel.NewAccommodation.Images = _accommodationRegistrationViewModel.ImagePaths.ToList<string>();
                _accommodationService.RegisterNew(_accommodationRegistrationViewModel.NewAccommodation);
                MessageBox.Show("You have successfully registered new accommodation", "Accommodation registration", MessageBoxButton.OK, MessageBoxImage.Information);
                _navigationStore.CurrentViewModel = new AccommodationOverviewViewModel(_userStore, _navigationStore, _modalNavigationStore);
            }
            else
            {
                MessageBox.Show("Invalid input", "Accommodation registration error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
