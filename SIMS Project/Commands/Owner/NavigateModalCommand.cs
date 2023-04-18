using SIMS_Project.Stores.Owner;
using SIMS_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Commands.Owner
{
    public class NavigateModalCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigateModalCommand(ModalNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _modalNavigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public override void Execute(object? parameter)
        {
            _modalNavigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
