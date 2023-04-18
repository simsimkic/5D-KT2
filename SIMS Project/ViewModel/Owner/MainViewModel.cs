using SIMS_Project.Controller;
using SIMS_Project.Model;
using SIMS_Project.Model.DTO.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIMS_Project.Commands;
using SIMS_Project.View.Owner;
using SIMS_Project.Stores.Owner;
using SIMS_Project.Stores;

namespace SIMS_Project.ViewModel.Owner
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UserStore _userStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public NavigationBarViewModel NavigationBarViewModel { get; }
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public ObservableCollection<Notification> Notifications { get; set; }
        public Notification SelectedNotification { get; set; }
        public User SignedInOwner => _userStore.SignedInUser;

        public ICommand ShowAccommodationRegistrationWindowCommand { get; }

        public MainViewModel(NavigationStore navigationStore, NavigationBarViewModel navigationBarViewModel, UserStore userStore, ModalNavigationStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _userStore = userStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
            _navigationStore.CurrentViewModel = new AccommodationOverviewViewModel(_userStore, _navigationStore, _modalNavigationStore);
            NavigationBarViewModel = navigationBarViewModel;

            Notifications = new ObservableCollection<Notification>();
            SelectedNotification = null;
            //GetNotifications();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }

        private void GetNotifications()
        {
            List<Notification> notifications = NotificationManager.GetGuestRatingNotifications(SignedInOwner.Id);
            foreach (Notification notification in notifications)
            {
                Notifications.Add(notification);
            }
        }
    }
}
