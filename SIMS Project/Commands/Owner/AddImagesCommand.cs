using SIMS_Project.Resources;
using SIMS_Project.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Commands.Owner
{
    public class AddImagesCommand : CommandBase
    {
        private AccommodationRegistrationViewModel _accommodationRegistrationViewModel;
        private FileManager _fileManager;

        public AddImagesCommand(AccommodationRegistrationViewModel accommodationRegistrationViewModel)
        {
            _accommodationRegistrationViewModel = accommodationRegistrationViewModel;
            _fileManager = new FileManager();
        }

        public override void Execute(object? parameter)
        {
            List<string> imageNames = _fileManager.BrowseImages();
            foreach (string imageName in imageNames)
            {
                _accommodationRegistrationViewModel.ImagePaths.Add(imageName);
            }
        }
    }
}
