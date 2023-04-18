using SIMS_Project.Commands.Owner;
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
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateBackCommand { get; }
        public ICommand NavigateAccommodationOverviewCommand { get; }
        public ICommand NavigateGuestForumsCommand { get; }
        public ICommand NavigateHelpCommand { get; }
        public ICommand NavigateUserProfileCommand { get; }
        public ICommand SignOutCommand { get; }

        public NavigationBarViewModel(NavigationStore navigationStore, UserStore userStore, ModalNavigationStore modalNavigationStore)
        {
            NavigateAccommodationOverviewCommand = new NavigateCommand<AccommodationOverviewViewModel>(
                navigationStore, () => new AccommodationOverviewViewModel(userStore, navigationStore, modalNavigationStore));
        }
    }
}
