using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Repository;
using SIMS_Project.Controller;
using SIMS_Project.Resources;
using SIMS_Project.Serializer;

namespace SIMS_Project.Model.DAO
{
    public class AccommodationDAO
    {
        private static AccommodationDAO _instance;
        private readonly AccommodationRepository _repository;
        private readonly FileManager _fileManager;
        private List<Accommodation> _accommodations;
        private List<Accommodation> _accommodationsSearch;

        private readonly LocationController _locationController;

        private AccommodationDAO()
        {
            _repository = new AccommodationRepository();
            _locationController = LocationController.GetInstance();
            _fileManager = new FileManager();
            _accommodations = _repository.Load();
            _accommodationsSearch = _accommodations;


            LoadLocations();
            LoadOwner();
        }

        private void LoadOwner()
        {
            UserController userController = UserController.GetInstance();
            foreach (Accommodation accommodation in _accommodations)
            {
                accommodation.Owner = userController.GetById(accommodation.OwnerId);
            }
        }

        private void LoadLocations()
        {
            foreach (Accommodation accommodation in _accommodations)
            {
                Location locationFromId = _locationController.GetById(accommodation.LocationId);
                if (locationFromId != null)
                {
                    accommodation.Location = locationFromId;
                }
            }
        }

        public static AccommodationDAO GetInstance()
        {
            if(_instance == null)
            {
                _instance = new AccommodationDAO();
            }

            return _instance;
        }

        public int NextId()
        {
            if (_accommodations.Count != 0)
                return _accommodations.Max(a => a.Id) + 1;
            else
                return 0;
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }

        public Accommodation GetById(int id)
        {
            return _accommodations.Find(a => a.Id == id);
        }

        public Accommodation Add(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            accommodation.Images = _fileManager.UploadImages(accommodation.Images, ResourcePath.AccommodationMediaPath, accommodation.Id);
            accommodation.Location = _locationController.Add(accommodation.Location);
            accommodation.LocationId = accommodation.Location.Id;
            _accommodations.Add(accommodation);
            _repository.Save(_accommodations);
            
            return accommodation;
        }

        private void SearchAccomodationsByName(string searchValue)
        {
            _accommodationsSearch = _accommodationsSearch.FindAll(acc => acc.Name.ToLower().Contains(searchValue.ToLower()));
        }

        private void SearchAccomodationsByLocation(string searchValue)
        {
            _accommodationsSearch = _accommodationsSearch.FindAll(acc => acc.Location.City.ToLower().Contains(searchValue.ToLower()) || acc.Location.Country.ToLower().Contains(searchValue.ToLower()));
        }

        private void SearchAccomodationsByType(string searchValue)
        {
            _accommodationsSearch = _accommodationsSearch.FindAll(acc => acc.Type.ToString().ToLower().Contains(searchValue.ToLower()));
        }
        private void SearchAccomodationsByNumOfGuests(int searchValue)
        {
            _accommodationsSearch = _accommodationsSearch.FindAll(acc => acc.MaxGuests >= searchValue);
        }

        private void SearchAccomodationsByNumOfDays(int searchValue)
        {
            _accommodationsSearch = _accommodationsSearch.FindAll(acc => acc.MinDays <= searchValue);
        }

        public IEnumerable<Accommodation> SearchAccomodations(string accName, string accLoc, string accType, int guests, int days)
        {
            try
            {
                _accommodationsSearch = _accommodations;
                if(accName != null)
                {
                    SearchAccomodationsByName(accName.Trim());
                }
                
                if(accLoc != null)
                {
                    SearchAccomodationsByLocation(accLoc.Trim());
                }
                
                if(accType != null)
                {
                    SearchAccomodationsByType(accType.Trim());
                }
               
                if(guests != 0)
                {
                    SearchAccomodationsByNumOfGuests(guests);
                }

                if (days != 0)
                {
                    SearchAccomodationsByNumOfDays(days);
                }
                
                return _accommodationsSearch;

            }catch(Exception ex)
            {
                return Enumerable.Empty<Accommodation>();
            }
        }

    }
}
