using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.UseCases
{
    public class LocationService
    {
        private static LocationService _instance;
        private readonly ILocationRepository _locationRepository;

        private LocationService()
        {
            _locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();
        }

        public static LocationService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LocationService();
            }

            return _instance;
        }

        public List<Location> GetAll()
        {
            return _locationRepository.GetAllValid().ToList();
        }

        public Location GetById(int id)
        {
            return _locationRepository.GetById(id);
        }

        public Location GetByCountry(string country)
        {
            return _locationRepository.GetByCountry(country);
        }

        public Location GetByCountyAndCity(Location location)
        {
            return _locationRepository.GetByCountyAndCity(location);
        }
    }
}
