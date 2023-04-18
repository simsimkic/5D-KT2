using SIMS_Project.Controller;
using SIMS_Project.Repository;
using SIMS_Project.Resources;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DAO
{
    internal class TourDAO
    {
        private static TourDAO _instance;
        private readonly FileManager _fileManager;
        private List<Tour> _tours;
        private readonly TourRepository _tourRepository;

       
        private readonly LocationController _locationController;
        private TourDAO()
        { 
            _tourRepository = new TourRepository();
            _locationController = LocationController.GetInstance();
         
            _fileManager = new FileManager();
            _tours = _tourRepository.Load();

            LoadLocations();
            
        }
        private void LoadLocations()
        {
            foreach (Tour tour in _tours)
            {
                Location locationFromId = _locationController.GetById(tour.LocationId);
                if (locationFromId != null)
                {
                    tour.Location = locationFromId;
                }
            }
        }
   
        public static TourDAO GetInstance() {
            if (_instance == null) 
            { 
                _instance = new TourDAO(); 
            }
            return _instance; 
            
        }
        public int NextId()
        {
            if (_tours.Count != 0)
                return _tours.Max(t => t.Id) + 1;
            else
                return 0;
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public Tour GetById(int id)
        {
            return _tours.Find(t => t.Id == id);
        }

        public Tour Add(Tour tour)
        {
            tour.Id = NextId();
            tour.Images = _fileManager.UploadImages(tour.Images, ResourcePath.TourMediaPath, tour.Id);
            tour.Location = _locationController.Add(tour.Location);
            tour.LocationId = tour.Location.Id;
            _tours.Add(tour);
            _tourRepository.Save(_tours);

            return tour;
        }
        public void Save() 
        {
            _tourRepository.Save(_tours); 
        }
    }
}
